namespace ThiTracNghiem.Models;

/// <summary>
/// Chuẩn hóa chuỗi đáp án theo quy ước của hệ thống:
/// các chữ cái A-D, viết hoa, đã sắp xếp, không lặp, không dấu phân cách.
/// Ví dụ: "c, a" → "AC"; "b" → "B"; "" → null (bỏ trống).
/// </summary>
public static class DapAnHelper
{
    public const string CacKyHieuHopLe = "ABCD";

    /// <summary>Chuẩn hóa đáp án. Ném <see cref="ArgumentException"/> nếu có ký tự ngoài A-D.</summary>
    public static string? ChuanHoa(string? dapAn)
    {
        if (string.IsNullOrWhiteSpace(dapAn))
            return null;

        var cacKyTu = dapAn
            .ToUpperInvariant()
            .Where(c => !char.IsWhiteSpace(c) && c != ',' && c != ';')
            .ToList();

        if (cacKyTu.Any(c => !CacKyHieuHopLe.Contains(c)))
            throw new ArgumentException($"Đáp án \"{dapAn}\" chỉ được chứa các chữ cái A, B, C, D.");

        var ketQua = new string(cacKyTu.Distinct().OrderBy(c => c).ToArray());
        return ketQua.Length == 0 ? null : ketQua;
    }

    /// <summary>Giống <see cref="ChuanHoa"/> nhưng trả về null thay vì ném lỗi khi dữ liệu sai.</summary>
    public static string? ChuanHoaHoacNull(string? dapAn)
    {
        try
        {
            return ChuanHoa(dapAn);
        }
        catch (ArgumentException)
        {
            return null;
        }
    }
}
