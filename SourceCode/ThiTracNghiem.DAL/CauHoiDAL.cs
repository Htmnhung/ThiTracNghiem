using System.Data;
using Microsoft.Data.SqlClient;
using ThiTracNghiem.Models;
using static ThiTracNghiem.DAL.DbHelper;

namespace ThiTracNghiem.DAL;

public class CauHoiDAL : RepositoryBase<CauHoi>
{
    // Danh sách cột dùng chung, cũng được LuotThiDAL dùng khi đọc chi tiết bài thi
    internal const string CacCotCauHoi =
        "ch.MaCH, ch.MaMH, mh.TenMH, ch.NoiDung, ch.PhuongAnA, ch.PhuongAnB, ch.PhuongAnC, ch.PhuongAnD, " +
        "ch.DapAnDung, ch.LoaiCauHoi, ch.DoKho, ch.TrangThai, ch.NguoiTao, ch.NgayTao";

    protected override string CauLenhChon =>
        $"SELECT {CacCotCauHoi} FROM dbo.CauHoi ch JOIN dbo.MonHoc mh ON mh.MaMH = ch.MaMH";

    protected override string CotKhoa => "ch.MaCH";

    protected override CauHoi AnhXa(IDataRecord dong) => AnhXaCauHoi(dong);

    /// <summary>
    /// Đọc một câu hỏi: dựa vào cột LoaiCauHoi để tạo đúng lớp con
    /// (CauHoiMotDapAn / CauHoiNhieuDapAn / CauHoiDungSai) thông qua factory.
    /// </summary>
    internal static CauHoi AnhXaCauHoi(IDataRecord dong)
    {
        var cauHoi = CauHoi.TaoTheoLoai(dong.LayEnum<LoaiCauHoi>("LoaiCauHoi"));

        cauHoi.MaCH = dong.LayInt("MaCH");
        cauHoi.MaMH = dong.LayInt("MaMH");
        cauHoi.TenMonHoc = dong.LayChuoiNull("TenMH");
        cauHoi.NoiDung = dong.LayChuoi("NoiDung");
        cauHoi.PhuongAnA = dong.LayChuoi("PhuongAnA");
        cauHoi.PhuongAnB = dong.LayChuoi("PhuongAnB");
        cauHoi.PhuongAnC = dong.LayChuoiNull("PhuongAnC");
        cauHoi.PhuongAnD = dong.LayChuoiNull("PhuongAnD");
        cauHoi.DapAnDung = dong.LayChuoi("DapAnDung");
        cauHoi.DoKho = dong.LayEnum<DoKho>("DoKho");
        cauHoi.DangSuDung = dong.LayBool("TrangThai");
        cauHoi.NguoiTao = dong.LayInt("NguoiTao");
        cauHoi.NgayTao = dong.LayNgay("NgayTao");

        return cauHoi;
    }

    /// <summary>Lấy tất cả câu hỏi, kể cả câu đã ẩn.</summary>
    public override List<CauHoi> LayTatCa() => TimKiem(null, null, null, null, hienCauDaAn: true);

    /// <summary>Tìm kiếm nhiều tiêu chí bằng stored procedure sp_TimKiemCauHoi. Tham số null = bỏ qua.</summary>
    public List<CauHoi> TimKiem(int? maMH, DoKho? doKho, LoaiCauHoi? loai, string? tuKhoa, bool hienCauDaAn) =>
        ExecuteReader("dbo.sp_TimKiemCauHoi", CommandType.StoredProcedure, AnhXaCauHoi,
            ThamSo("@MaMH", maMH),
            ThamSo("@DoKho", doKho?.ToString()),
            ThamSo("@LoaiCauHoi", loai?.ToString()),
            ThamSo("@TuKhoa", string.IsNullOrWhiteSpace(tuKhoa) ? null : ThoatKyTuLike(tuKhoa.Trim())),
            ThamSo("@HienCauDaAn", hienCauDaAn));

    /// <summary>Số câu đang sử dụng của một môn theo từng độ khó (dùng khi kiểm tra cấu trúc đề).</summary>
    public Dictionary<DoKho, int> DemTheoDoKho(int maMH)
    {
        var ketQua = Enum.GetValues<DoKho>().ToDictionary(d => d, _ => 0);

        var danhSach = ExecuteReader(
            "SELECT DoKho, COUNT(*) AS SoCau FROM dbo.CauHoi WHERE MaMH = @MaMH AND TrangThai = 1 GROUP BY DoKho",
            CommandType.Text,
            dong => (DoKho: dong.LayEnum<DoKho>("DoKho"), SoCau: dong.LayInt("SoCau")),
            ThamSo("@MaMH", maMH));

        foreach (var (doKho, soCau) in danhSach)
            ketQua[doKho] = soCau;

        return ketQua;
    }

    /// <summary>Câu hỏi đã từng xuất hiện trong một lượt thi (không được xóa hẳn, không được đổi đáp án).</summary>
    public bool DaDuocSuDung(int maCH) =>
        TonTai("SELECT COUNT(*) FROM dbo.ChiTietLuotThi WHERE MaCH = @MaCH", ThamSo("@MaCH", maCH));

    public override int Them(CauHoi ch) =>
        Convert.ToInt32(ExecuteScalar(@"
            INSERT INTO dbo.CauHoi (MaMH, NoiDung, PhuongAnA, PhuongAnB, PhuongAnC, PhuongAnD,
                                    DapAnDung, LoaiCauHoi, DoKho, TrangThai, NguoiTao)
            OUTPUT INSERTED.MaCH
            VALUES (@MaMH, @NoiDung, @PhuongAnA, @PhuongAnB, @PhuongAnC, @PhuongAnD,
                    @DapAnDung, @LoaiCauHoi, @DoKho, @TrangThai, @NguoiTao)",
            TaoThamSo(ch)));

    public override bool CapNhat(CauHoi ch) =>
        ExecuteNonQuery(@"
            UPDATE dbo.CauHoi
            SET MaMH = @MaMH, NoiDung = @NoiDung,
                PhuongAnA = @PhuongAnA, PhuongAnB = @PhuongAnB, PhuongAnC = @PhuongAnC, PhuongAnD = @PhuongAnD,
                DapAnDung = @DapAnDung, LoaiCauHoi = @LoaiCauHoi, DoKho = @DoKho, TrangThai = @TrangThai
            WHERE MaCH = @MaCH",
            [.. TaoThamSo(ch), ThamSo("@MaCH", ch.MaCH)]) > 0;

    /// <summary>Ẩn (false) hoặc hiện lại (true) câu hỏi - xóa mềm.</summary>
    public bool DatTrangThai(int maCH, bool dangSuDung) =>
        ExecuteNonQuery("UPDATE dbo.CauHoi SET TrangThai = @TrangThai WHERE MaCH = @MaCH",
            ThamSo("@TrangThai", dangSuDung), ThamSo("@MaCH", maCH)) > 0;

    /// <summary>Xóa hẳn. CSDL sẽ chặn (FK_ChiTietLuotThi_CauHoi) nếu câu hỏi đã được dùng trong bài thi.</summary>
    public override bool Xoa(int maCH) =>
        ExecuteNonQuery("DELETE FROM dbo.CauHoi WHERE MaCH = @MaCH", ThamSo("@MaCH", maCH)) > 0;

    private static SqlParameter[] TaoThamSo(CauHoi ch) =>
    [
        ThamSo("@MaMH", ch.MaMH),
        ThamSo("@NoiDung", ch.NoiDung),
        ThamSo("@PhuongAnA", ch.PhuongAnA),
        ThamSo("@PhuongAnB", ch.PhuongAnB),
        ThamSo("@PhuongAnC", ch.PhuongAnC),
        ThamSo("@PhuongAnD", ch.PhuongAnD),
        ThamSo("@DapAnDung", ch.DapAnDung),
        ThamSo("@LoaiCauHoi", ch.Loai.ToString()),
        ThamSo("@DoKho", ch.DoKho.ToString()),
        ThamSo("@TrangThai", ch.DangSuDung),
        ThamSo("@NguoiTao", ch.NguoiTao)
    ];
}
