namespace ThiTracNghiem.BLL;

/// <summary>
/// Ngoại lệ duy nhất tầng BLL ném ra cho giao diện: thông báo đã thân thiện, hiển thị thẳng cho người dùng.
/// </summary>
public class NghiepVuException : Exception
{
    public NghiepVuException(string thongBao, bool laLoiKetNoi = false, Exception? innerException = null)
        : base(thongBao, innerException)
    {
        DanhSachLoi = new[] { thongBao };
        LaLoiKetNoi = laLoiKetNoi;
    }

    /// <summary>Dùng khi kiểm tra dữ liệu phát hiện nhiều lỗi cùng lúc.</summary>
    public NghiepVuException(IEnumerable<string> danhSachLoi)
        : this(danhSachLoi.ToList())
    {
    }

    private NghiepVuException(List<string> danhSachLoi)
        : base(string.Join(Environment.NewLine, danhSachLoi.Select(loi => "• " + loi)))
    {
        DanhSachLoi = danhSachLoi;
    }

    public IReadOnlyList<string> DanhSachLoi { get; }

    /// <summary>true: lỗi do không kết nối được CSDL (giao diện có thể hiển thị biểu tượng lỗi khác).</summary>
    public bool LaLoiKetNoi { get; }
}
