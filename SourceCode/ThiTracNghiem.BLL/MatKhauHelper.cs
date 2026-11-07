using System.Security.Cryptography;
using System.Text;

namespace ThiTracNghiem.BLL;

/// <summary>Băm và kiểm tra mật khẩu. CSDL chỉ lưu chuỗi băm, không lưu mật khẩu gốc.</summary>
public static class MatKhauHelper
{
    public const int DoDaiToiThieu = 6;
    public const int DoDaiToiDa = 50;

    /// <summary>SHA-256 của chuỗi UTF-8, dạng HEX in hoa (64 ký tự) - khớp với HASHBYTES('SHA2_256', ...) trong script SQL.</summary>
    public static string BamMatKhau(string matKhau) =>
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(matKhau)));

    public static bool KiemTra(string matKhau, string matKhauHash) =>
        string.Equals(BamMatKhau(matKhau), matKhauHash.Trim(), StringComparison.OrdinalIgnoreCase);

    /// <summary>Trả về thông báo lỗi nếu mật khẩu không đạt yêu cầu, null nếu hợp lệ.</summary>
    public static string? KiemTraDoManh(string? matKhau)
    {
        if (string.IsNullOrEmpty(matKhau))
            return "Mật khẩu không được để trống.";
        if (matKhau.Length < DoDaiToiThieu || matKhau.Length > DoDaiToiDa)
            return $"Mật khẩu phải dài từ {DoDaiToiThieu} đến {DoDaiToiDa} ký tự.";
        if (matKhau.Contains(' '))
            return "Mật khẩu không được chứa khoảng trắng.";
        return null;
    }
}
