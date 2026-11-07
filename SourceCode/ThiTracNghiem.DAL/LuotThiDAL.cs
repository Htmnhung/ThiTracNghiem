using System.Data;
using Microsoft.Data.SqlClient;
using ThiTracNghiem.Models;
using static ThiTracNghiem.DAL.DbHelper;

namespace ThiTracNghiem.DAL;

/// <summary>
/// Truy cập dữ liệu lượt thi. Không kế thừa RepositoryBase vì lượt thi không có CRUD thông thường:
/// lượt thi được tạo bằng stored procedure, cập nhật đáp án từng câu và nộp bài trong transaction.
/// </summary>
public class LuotThiDAL
{
    /// <summary>
    /// Tạo lượt thi mới bằng sp_TaoLuotThi: bốc ngẫu nhiên câu hỏi theo cấu trúc đề, chạy trong transaction.
    /// Trả về mã lượt thi vừa tạo.
    /// </summary>
    /// <param name="thoiGianBatDau">Giờ bắt đầu theo đồng hồ của ứng dụng; null = giờ của máy chủ CSDL.</param>
    public int TaoLuotThi(int maDe, int maTK, DateTime? thoiGianBatDau = null)
    {
        var maLT = new SqlParameter("@MaLT", SqlDbType.Int) { Direction = ParameterDirection.Output };

        ExecuteProcedureNonQuery("dbo.sp_TaoLuotThi",
            ThamSo("@MaDe", maDe),
            ThamSo("@MaTK", maTK),
            ThamSo("@ThoiGianBatDau", thoiGianBatDau),
            maLT);

        return (int)maLT.Value;
    }

    /// <summary>Đọc lượt thi cùng danh sách câu hỏi (đúng lớp con) và đáp án đã chọn.</summary>
    public LuotThi? LayTheoMa(int maLT) =>
        ThucThi(() =>
        {
            const string sql = @"
                SELECT MaLT, MaDe, MaTK, ThoiGianBatDau, ThoiGianNop, SoCauDung, Diem, TrangThai
                FROM dbo.LuotThi
                WHERE MaLT = @MaLT;

                SELECT ct.ThuTu, ct.DapAnChon, ct.LaDung, " + CauHoiDAL.CacCotCauHoi + @"
                FROM dbo.ChiTietLuotThi ct
                JOIN dbo.CauHoi ch ON ch.MaCH = ct.MaCH
                JOIN dbo.MonHoc mh ON mh.MaMH = ch.MaMH
                WHERE ct.MaLT = @MaLT
                ORDER BY ct.ThuTu;";

            using var ketNoi = TaoKetNoi();
            using var lenh = TaoLenh(ketNoi, sql, CommandType.Text, [ThamSo("@MaLT", maLT)]);
            MoKetNoi(ketNoi);
            using var doc = lenh.ExecuteReader();

            if (!doc.Read())
                return null;

            var luotThi = new LuotThi
            {
                MaLT = doc.LayInt("MaLT"),
                MaDe = doc.LayInt("MaDe"),
                MaTK = doc.LayInt("MaTK"),
                ThoiGianBatDau = doc.LayNgay("ThoiGianBatDau"),
                ThoiGianNop = doc.LayNgayNull("ThoiGianNop"),
                SoCauDung = doc.LayIntNull("SoCauDung"),
                Diem = doc.LayDecimalNull("Diem"),
                TrangThai = doc.LayEnum<TrangThaiLuotThi>("TrangThai")
            };

            doc.NextResult();
            while (doc.Read())
            {
                var chiTiet = new ChiTietLuotThi(CauHoiDAL.AnhXaCauHoi(doc), doc.LayInt("ThuTu"))
                {
                    DapAnChon = doc.LayChuoiNull("DapAnChon"),
                    LaDung = doc.LayBoolNull("LaDung")
                };
                luotThi.ThemChiTiet(chiTiet);
            }

            return luotThi;
        });

    /// <summary>Số lần sinh viên đã thi một đề (tính cả lượt đang làm).</summary>
    public int DemSoLanThi(int maDe, int maTK) =>
        Convert.ToInt32(ExecuteScalar("SELECT COUNT(*) FROM dbo.LuotThi WHERE MaDe = @MaDe AND MaTK = @MaTK",
            ThamSo("@MaDe", maDe), ThamSo("@MaTK", maTK)));

    /// <summary>Mã lượt thi đang làm dở của sinh viên với đề này (ví dụ máy bị tắt giữa chừng), nếu có.</summary>
    public int? LayMaLuotDangLam(int maDe, int maTK)
    {
        var ketQua = ExecuteScalar(@"
            SELECT TOP 1 MaLT FROM dbo.LuotThi
            WHERE MaDe = @MaDe AND MaTK = @MaTK AND TrangThai = 'DangLam'
            ORDER BY ThoiGianBatDau DESC",
            ThamSo("@MaDe", maDe), ThamSo("@MaTK", maTK));

        return ketQua == null ? null : Convert.ToInt32(ketQua);
    }

    /// <summary>Lưu ngay đáp án của một câu (tự động lưu khi sinh viên chọn). Chỉ lưu được khi lượt thi đang làm.</summary>
    public bool LuuDapAn(int maLT, int maCH, string? dapAnChon) =>
        ExecuteNonQuery(@"
            UPDATE ct SET DapAnChon = @DapAnChon
            FROM dbo.ChiTietLuotThi ct
            JOIN dbo.LuotThi lt ON lt.MaLT = ct.MaLT
            WHERE ct.MaLT = @MaLT AND ct.MaCH = @MaCH AND lt.TrangThai = 'DangLam'",
            ThamSo("@DapAnChon", dapAnChon),
            ThamSo("@MaLT", maLT),
            ThamSo("@MaCH", maCH)) > 0;

    /// <summary>
    /// Lưu kết quả bài thi đã chấm (sau khi gọi LuotThi.NopBai) trong một transaction:
    /// cập nhật đáp án + đúng/sai của từng câu và điểm của lượt thi.
    /// Nếu lượt thi đã được nộp trước đó thì hủy toàn bộ.
    /// </summary>
    public void LuuKetQuaNopBai(LuotThi luotThi)
    {
        if (!luotThi.DaNop)
            throw new InvalidOperationException("Lượt thi chưa được chấm điểm.");

        ThucThiGiaoTac((ketNoi, giaoTac) =>
        {
            foreach (var chiTiet in luotThi.ChiTiet)
            {
                using var lenhChiTiet = TaoLenh(ketNoi, @"
                    UPDATE dbo.ChiTietLuotThi SET DapAnChon = @DapAnChon, LaDung = @LaDung
                    WHERE MaLT = @MaLT AND MaCH = @MaCH",
                    CommandType.Text,
                    [
                        ThamSo("@DapAnChon", chiTiet.DapAnChon),
                        ThamSo("@LaDung", chiTiet.LaDung),
                        ThamSo("@MaLT", luotThi.MaLT),
                        ThamSo("@MaCH", chiTiet.MaCH)
                    ],
                    giaoTac);
                lenhChiTiet.ExecuteNonQuery();
            }

            using var lenhLuotThi = TaoLenh(ketNoi, @"
                UPDATE dbo.LuotThi
                SET ThoiGianNop = @ThoiGianNop, SoCauDung = @SoCauDung, Diem = @Diem, TrangThai = 'DaNop'
                WHERE MaLT = @MaLT AND TrangThai = 'DangLam'",
                CommandType.Text,
                [
                    ThamSo("@ThoiGianNop", luotThi.ThoiGianNop),
                    ThamSo("@SoCauDung", luotThi.SoCauDung),
                    ThamSo("@Diem", luotThi.Diem),
                    ThamSo("@MaLT", luotThi.MaLT)
                ],
                giaoTac);

            if (lenhLuotThi.ExecuteNonQuery() == 0)
                throw new DataAccessException(LoaiLoiDuLieu.LoiNghiepVu,
                    "Lượt thi không tồn tại hoặc đã được nộp trước đó.");
        });
    }

    /// <summary>
    /// Tra cứu kết quả thi (từ view vw_KetQuaThi) theo nhiều tiêu chí, chỉ lấy lượt đã nộp.
    /// Tham số null = bỏ qua tiêu chí đó.
    /// </summary>
    public DataTable TimKiemKetQua(int? maDe = null, int? maTK = null, string? lop = null,
        decimal? diemTu = null, decimal? diemDen = null, DateTime? tuNgay = null, DateTime? denNgay = null) =>
        ExecuteQuery(@"
            SELECT MaLT, MaDe, TenDe, TenMH, MaTK, MaSV, HoTen, Lop,
                   ThoiGianBatDau, ThoiGianNop, SoPhutLam, SoCauDung, TongSoCau, Diem, KetQua
            FROM dbo.vw_KetQuaThi
            WHERE TrangThai = 'DaNop'
              AND (@MaDe IS NULL OR MaDe = @MaDe)
              AND (@MaTK IS NULL OR MaTK = @MaTK)
              AND (@Lop IS NULL OR Lop = @Lop)
              AND (@DiemTu IS NULL OR Diem >= @DiemTu)
              AND (@DiemDen IS NULL OR Diem <= @DiemDen)
              AND (@TuNgay IS NULL OR ThoiGianNop >= @TuNgay)
              AND (@DenNgay IS NULL OR ThoiGianNop < DATEADD(DAY, 1, @DenNgay))
            ORDER BY ThoiGianNop DESC",
            ThamSo("@MaDe", maDe),
            ThamSo("@MaTK", maTK),
            ThamSo("@Lop", lop),
            ThamSo("@DiemTu", diemTu),
            ThamSo("@DiemDen", diemDen),
            ThamSo("@TuNgay", tuNgay?.Date),
            ThamSo("@DenNgay", denNgay?.Date));
}
