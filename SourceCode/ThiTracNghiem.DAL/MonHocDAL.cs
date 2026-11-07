using System.Data;
using ThiTracNghiem.Models;
using static ThiTracNghiem.DAL.DbHelper;

namespace ThiTracNghiem.DAL;

public class MonHocDAL : RepositoryBase<MonHoc>
{
    protected override string CauLenhChon => "SELECT MaMH, TenMH, SoTinChi, MoTa FROM dbo.MonHoc";

    protected override string CotKhoa => "MaMH";

    protected override MonHoc AnhXa(IDataRecord dong) => new()
    {
        MaMH = dong.LayInt("MaMH"),
        TenMH = dong.LayChuoi("TenMH"),
        SoTinChi = dong.LayInt("SoTinChi"),
        MoTa = dong.LayChuoiNull("MoTa")
    };

    public override List<MonHoc> LayTatCa() => DocDanhSach($"{CauLenhChon} ORDER BY TenMH");

    public List<MonHoc> TimKiem(string? tuKhoa) =>
        DocDanhSach($"{CauLenhChon} WHERE @TuKhoa IS NULL OR TenMH LIKE N'%' + @TuKhoa + N'%' ORDER BY TenMH",
            ThamSo("@TuKhoa", string.IsNullOrWhiteSpace(tuKhoa) ? null : ThoatKyTuLike(tuKhoa.Trim())));

    public bool TenDaTonTai(string tenMH, int boQuaMaMH = 0) =>
        TonTai("SELECT COUNT(*) FROM dbo.MonHoc WHERE TenMH = @TenMH AND MaMH <> @BoQua",
            ThamSo("@TenMH", tenMH), ThamSo("@BoQua", boQuaMaMH));

    public int DemCauHoi(int maMH) =>
        Convert.ToInt32(ExecuteScalar("SELECT COUNT(*) FROM dbo.CauHoi WHERE MaMH = @MaMH", ThamSo("@MaMH", maMH)));

    public int DemDeThi(int maMH) =>
        Convert.ToInt32(ExecuteScalar("SELECT COUNT(*) FROM dbo.DeThi WHERE MaMH = @MaMH", ThamSo("@MaMH", maMH)));

    public override int Them(MonHoc mh) =>
        Convert.ToInt32(ExecuteScalar(@"
            INSERT INTO dbo.MonHoc (TenMH, SoTinChi, MoTa)
            OUTPUT INSERTED.MaMH
            VALUES (@TenMH, @SoTinChi, @MoTa)",
            ThamSo("@TenMH", mh.TenMH),
            ThamSo("@SoTinChi", mh.SoTinChi),
            ThamSo("@MoTa", mh.MoTa)));

    public override bool CapNhat(MonHoc mh) =>
        ExecuteNonQuery(@"
            UPDATE dbo.MonHoc SET TenMH = @TenMH, SoTinChi = @SoTinChi, MoTa = @MoTa
            WHERE MaMH = @MaMH",
            ThamSo("@TenMH", mh.TenMH),
            ThamSo("@SoTinChi", mh.SoTinChi),
            ThamSo("@MoTa", mh.MoTa),
            ThamSo("@MaMH", mh.MaMH)) > 0;

    public override bool Xoa(int maMH) =>
        ExecuteNonQuery("DELETE FROM dbo.MonHoc WHERE MaMH = @MaMH", ThamSo("@MaMH", maMH)) > 0;
}
