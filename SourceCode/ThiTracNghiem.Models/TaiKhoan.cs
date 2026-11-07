using System.Text.RegularExpressions;

namespace ThiTracNghiem.Models;

/// <summary>Tài khoản đăng nhập, dùng chung cho giảng viên và sinh viên.</summary>
public class TaiKhoan
{
    private static readonly Regex MauEmail = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
    private static readonly Regex MauTenDangNhap = new(@"^[A-Za-z0-9_.]{4,30}$", RegexOptions.Compiled);

    private string _tenDangNhap = string.Empty;
    private string _hoTen = string.Empty;
    private string? _lop;
    private string _email = string.Empty;

    public int MaTK { get; set; }

    /// <summary>Sinh viên dùng MSSV làm tên đăng nhập.</summary>
    public string TenDangNhap
    {
        get => _tenDangNhap;
        set => _tenDangNhap = value?.Trim() ?? string.Empty;
    }

    /// <summary>Mật khẩu đã băm SHA-256 (không bao giờ lưu mật khẩu gốc).</summary>
    public string MatKhauHash { get; set; } = string.Empty;

    public string HoTen
    {
        get => _hoTen;
        set => _hoTen = value?.Trim() ?? string.Empty;
    }

    public VaiTro VaiTro { get; set; } = VaiTro.SinhVien;

    public string? Lop
    {
        get => _lop;
        set => _lop = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }

    public string Email
    {
        get => _email;
        set => _email = value?.Trim().ToLowerInvariant() ?? string.Empty;
    }

    /// <summary>false = tài khoản bị khóa, không được đăng nhập.</summary>
    public bool DangHoatDong { get; set; } = true;

    public DateTime NgayTao { get; set; } = DateTime.Now;

    public bool LaGiangVien => VaiTro == VaiTro.GiangVien;
    public bool LaSinhVien => VaiTro == VaiTro.SinhVien;

    public List<string> KiemTraHopLe()
    {
        var loi = new List<string>();

        if (!MauTenDangNhap.IsMatch(TenDangNhap))
            loi.Add("Tên đăng nhập dài 4-30 ký tự, chỉ gồm chữ không dấu, số, dấu chấm hoặc gạch dưới.");

        if (string.IsNullOrEmpty(HoTen))
            loi.Add("Họ tên không được để trống.");
        else if (HoTen.Length > 100)
            loi.Add("Họ tên tối đa 100 ký tự.");

        if (!MauEmail.IsMatch(Email) || Email.Length > 100)
            loi.Add("Email không đúng định dạng.");

        if (LaSinhVien && string.IsNullOrEmpty(Lop))
            loi.Add("Sinh viên phải có lớp.");
        else if (Lop?.Length > 20)
            loi.Add("Tên lớp tối đa 20 ký tự.");

        return loi;
    }

    public override string ToString() => $"{HoTen} ({TenDangNhap})";
}
