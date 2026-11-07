using System.Data;
using Microsoft.Data.SqlClient;
using ThiTracNghiem.Models;
using static ThiTracNghiem.DAL.DbHelper;

namespace ThiTracNghiem.DAL;

public class DeThiDAL : RepositoryBase<DeThi>
{
    protected override string CauLenhChon => @"
        SELECT dt.MaDe, dt.MaMH, mh.TenMH, dt.TenDe, dt.ThoiGianLam, dt.SoCauDe, dt.SoCauTB, dt.SoCauKho,
               dt.ThoiGianMo, dt.ThoiGianDong, dt.SoLanThiToiDa, dt.NguoiTao, dt.NgayTao
        FROM dbo.DeThi dt
        JOIN dbo.MonHoc mh ON mh.MaMH = dt.MaMH";

    protected override string CotKhoa => "dt.MaDe";

    protected override DeThi AnhXa(IDataRecord dong) => new()
    {
        MaDe = dong.LayInt("MaDe"),
        MaMH = dong.LayInt("MaMH"),
        TenMonHoc = dong.LayChuoiNull("TenMH"),
        TenDe = dong.LayChuoi("TenDe"),
        ThoiGianLam = dong.LayInt("ThoiGianLam"),
        SoCauDe = dong.LayInt("SoCauDe"),
        SoCauTB = dong.LayInt("SoCauTB"),
        SoCauKho = dong.LayInt("SoCauKho"),
        ThoiGianMo = dong.LayNgay("ThoiGianMo"),
        ThoiGianDong = dong.LayNgay("ThoiGianDong"),
        SoLanThiToiDa = dong.LayInt("SoLanThiToiDa"),
        NguoiTao = dong.LayInt("NguoiTao"),
        NgayTao = dong.LayNgay("NgayTao")
    };

    public override List<DeThi> LayTatCa() => TimKiem(null, null);

    /// <summary>Tìm đề theo môn học và từ khóa trong tên đề. Tham số null = bỏ qua.</summary>
    public List<DeThi> TimKiem(int? maMH, string? tuKhoa) =>
        DocDanhSach($@"{CauLenhChon}
            WHERE (@MaMH IS NULL OR dt.MaMH = @MaMH)
              AND (@TuKhoa IS NULL OR dt.TenDe LIKE N'%' + @TuKhoa + N'%')
            ORDER BY dt.ThoiGianMo DESC, dt.MaDe DESC",
            ThamSo("@MaMH", maMH),
            ThamSo("@TuKhoa", string.IsNullOrWhiteSpace(tuKhoa) ? null : ThoatKyTuLike(tuKhoa.Trim())));

    /// <summary>Đề đã có người thi thì không được sửa cấu trúc / xóa.</summary>
    public bool CoLuotThi(int maDe) =>
        TonTai("SELECT COUNT(*) FROM dbo.LuotThi WHERE MaDe = @MaDe", ThamSo("@MaDe", maDe));

    public override int Them(DeThi de) =>
        Convert.ToInt32(ExecuteScalar(@"
            INSERT INTO dbo.DeThi (MaMH, TenDe, ThoiGianLam, SoCauDe, SoCauTB, SoCauKho,
                                   ThoiGianMo, ThoiGianDong, SoLanThiToiDa, NguoiTao)
            OUTPUT INSERTED.MaDe
            VALUES (@MaMH, @TenDe, @ThoiGianLam, @SoCauDe, @SoCauTB, @SoCauKho,
                    @ThoiGianMo, @ThoiGianDong, @SoLanThiToiDa, @NguoiTao)",
            TaoThamSo(de)));

    public override bool CapNhat(DeThi de) =>
        ExecuteNonQuery(@"
            UPDATE dbo.DeThi
            SET MaMH = @MaMH, TenDe = @TenDe, ThoiGianLam = @ThoiGianLam,
                SoCauDe = @SoCauDe, SoCauTB = @SoCauTB, SoCauKho = @SoCauKho,
                ThoiGianMo = @ThoiGianMo, ThoiGianDong = @ThoiGianDong, SoLanThiToiDa = @SoLanThiToiDa
            WHERE MaDe = @MaDe",
            [.. TaoThamSo(de), ThamSo("@MaDe", de.MaDe)]) > 0;

    public override bool Xoa(int maDe) =>
        ExecuteNonQuery("DELETE FROM dbo.DeThi WHERE MaDe = @MaDe", ThamSo("@MaDe", maDe)) > 0;

    private static SqlParameter[] TaoThamSo(DeThi de) =>
    [
        ThamSo("@MaMH", de.MaMH),
        ThamSo("@TenDe", de.TenDe),
        ThamSo("@ThoiGianLam", de.ThoiGianLam),
        ThamSo("@SoCauDe", de.SoCauDe),
        ThamSo("@SoCauTB", de.SoCauTB),
        ThamSo("@SoCauKho", de.SoCauKho),
        ThamSo("@ThoiGianMo", de.ThoiGianMo),
        ThamSo("@ThoiGianDong", de.ThoiGianDong),
        ThamSo("@SoLanThiToiDa", de.SoLanThiToiDa),
        ThamSo("@NguoiTao", de.NguoiTao)
    ];
}
