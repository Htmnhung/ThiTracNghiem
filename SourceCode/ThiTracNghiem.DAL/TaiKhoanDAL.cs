using System.Data;
using ThiTracNghiem.Models;
using static ThiTracNghiem.DAL.DbHelper;

namespace ThiTracNghiem.DAL;

public class TaiKhoanDAL : RepositoryBase<TaiKhoan>
{
    protected override string CauLenhChon =>
        "SELECT MaTK, TenDangNhap, MatKhau, HoTen, VaiTro, Lop, Email, TrangThai, NgayTao FROM dbo.TaiKhoan";

    protected override string CotKhoa => "MaTK";

    protected override TaiKhoan AnhXa(IDataRecord dong) => new()
    {
        MaTK = dong.LayInt("MaTK"),
        TenDangNhap = dong.LayChuoi("TenDangNhap"),
        MatKhauHash = dong.LayChuoi("MatKhau"),
        HoTen = dong.LayChuoi("HoTen"),
        VaiTro = dong.LayEnum<VaiTro>("VaiTro"),
        Lop = dong.LayChuoiNull("Lop"),
        Email = dong.LayChuoi("Email"),
        DangHoatDong = dong.LayBool("TrangThai"),
        NgayTao = dong.LayNgay("NgayTao")
    };

    public TaiKhoan? LayTheoTenDangNhap(string tenDangNhap) =>
        DocDanhSach($"{CauLenhChon} WHERE TenDangNhap = @TenDangNhap",
            ThamSo("@TenDangNhap", tenDangNhap)).FirstOrDefault();

    /// <summary>Tìm tài khoản theo vai trò, lớp và từ khóa (họ tên hoặc tên đăng nhập). Tham số null = bỏ qua.</summary>
    public List<TaiKhoan> TimKiem(VaiTro? vaiTro, string? lop, string? tuKhoa) =>
        DocDanhSach($@"{CauLenhChon}
            WHERE (@VaiTro IS NULL OR VaiTro = @VaiTro)
              AND (@Lop IS NULL OR Lop = @Lop)
              AND (@TuKhoa IS NULL OR HoTen LIKE N'%' + @TuKhoa + N'%' OR TenDangNhap LIKE '%' + @TuKhoa + '%')
            ORDER BY VaiTro, TenDangNhap",
            ThamSo("@VaiTro", vaiTro?.ToString()),
            ThamSo("@Lop", lop),
            ThamSo("@TuKhoa", string.IsNullOrWhiteSpace(tuKhoa) ? null : ThoatKyTuLike(tuKhoa.Trim())));

    public List<string> LayDanhSachLop() =>
        ExecuteReader("SELECT DISTINCT Lop FROM dbo.TaiKhoan WHERE Lop IS NOT NULL ORDER BY Lop",
            CommandType.Text, dong => dong.LayChuoi("Lop"));

    public bool TenDangNhapDaTonTai(string tenDangNhap, int boQuaMaTK = 0) =>
        TonTai("SELECT COUNT(*) FROM dbo.TaiKhoan WHERE TenDangNhap = @Ten AND MaTK <> @BoQua",
            ThamSo("@Ten", tenDangNhap), ThamSo("@BoQua", boQuaMaTK));

    public bool EmailDaTonTai(string email, int boQuaMaTK = 0) =>
        TonTai("SELECT COUNT(*) FROM dbo.TaiKhoan WHERE Email = @Email AND MaTK <> @BoQua",
            ThamSo("@Email", email), ThamSo("@BoQua", boQuaMaTK));

    /// <summary>Tài khoản đã có dữ liệu liên quan (câu hỏi, đề thi đã tạo hoặc lượt thi) thì không xóa được.</summary>
    public bool CoDuLieuLienQuan(int maTK) =>
        TonTai(@"SELECT (SELECT COUNT(*) FROM dbo.LuotThi WHERE MaTK = @Ma)
                      + (SELECT COUNT(*) FROM dbo.CauHoi  WHERE NguoiTao = @Ma)
                      + (SELECT COUNT(*) FROM dbo.DeThi   WHERE NguoiTao = @Ma)",
            ThamSo("@Ma", maTK));

    public override int Them(TaiKhoan tk) =>
        Convert.ToInt32(ExecuteScalar(@"
            INSERT INTO dbo.TaiKhoan (TenDangNhap, MatKhau, HoTen, VaiTro, Lop, Email, TrangThai)
            OUTPUT INSERTED.MaTK
            VALUES (@TenDangNhap, @MatKhau, @HoTen, @VaiTro, @Lop, @Email, @TrangThai)",
            ThamSo("@TenDangNhap", tk.TenDangNhap),
            ThamSo("@MatKhau", tk.MatKhauHash),
            ThamSo("@HoTen", tk.HoTen),
            ThamSo("@VaiTro", tk.VaiTro.ToString()),
            ThamSo("@Lop", tk.Lop),
            ThamSo("@Email", tk.Email),
            ThamSo("@TrangThai", tk.DangHoatDong)));

    /// <summary>Cập nhật thông tin tài khoản (không đổi mật khẩu; đổi mật khẩu dùng <see cref="DoiMatKhau"/>).</summary>
    public override bool CapNhat(TaiKhoan tk) =>
        ExecuteNonQuery(@"
            UPDATE dbo.TaiKhoan
            SET TenDangNhap = @TenDangNhap, HoTen = @HoTen, VaiTro = @VaiTro,
                Lop = @Lop, Email = @Email, TrangThai = @TrangThai
            WHERE MaTK = @MaTK",
            ThamSo("@TenDangNhap", tk.TenDangNhap),
            ThamSo("@HoTen", tk.HoTen),
            ThamSo("@VaiTro", tk.VaiTro.ToString()),
            ThamSo("@Lop", tk.Lop),
            ThamSo("@Email", tk.Email),
            ThamSo("@TrangThai", tk.DangHoatDong),
            ThamSo("@MaTK", tk.MaTK)) > 0;

    public bool DoiMatKhau(int maTK, string matKhauHash) =>
        ExecuteNonQuery("UPDATE dbo.TaiKhoan SET MatKhau = @MatKhau WHERE MaTK = @MaTK",
            ThamSo("@MatKhau", matKhauHash), ThamSo("@MaTK", maTK)) > 0;

    /// <summary>Khóa (false) hoặc mở khóa (true) tài khoản.</summary>
    public bool DatTrangThai(int maTK, bool dangHoatDong) =>
        ExecuteNonQuery("UPDATE dbo.TaiKhoan SET TrangThai = @TrangThai WHERE MaTK = @MaTK",
            ThamSo("@TrangThai", dangHoatDong), ThamSo("@MaTK", maTK)) > 0;

    public override bool Xoa(int maTK) =>
        ExecuteNonQuery("DELETE FROM dbo.TaiKhoan WHERE MaTK = @MaTK", ThamSo("@MaTK", maTK)) > 0;
}
