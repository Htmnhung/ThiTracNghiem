namespace ThiTracNghiem.DAL;

/// <summary>Phân loại lỗi CSDL để tầng BLL đưa ra thông báo phù hợp.</summary>
public enum LoaiLoiDuLieu
{
    /// <summary>Không kết nối được SQL Server, sai tên CSDL, sai chuỗi kết nối...</summary>
    KhongKetNoiDuoc,
    /// <summary>Vi phạm PRIMARY KEY / UNIQUE.</summary>
    TrungDuLieu,
    /// <summary>Vi phạm FOREIGN KEY (xóa bản ghi đang được tham chiếu, tham chiếu tới bản ghi không tồn tại).</summary>
    ViPhamKhoaNgoai,
    /// <summary>Vi phạm CHECK constraint.</summary>
    ViPhamRangBuoc,
    /// <summary>Lỗi nghiệp vụ do stored procedure chủ động THROW (mã lỗi từ 50000).</summary>
    LoiNghiepVu,
    Khac
}

/// <summary>
/// Ngoại lệ của tầng DAL. Bọc SqlException để tầng BLL không phụ thuộc vào thư viện SqlClient.
/// </summary>
public class DataAccessException : Exception
{
    public DataAccessException(LoaiLoiDuLieu loai, string message, string? tenRangBuoc = null, Exception? innerException = null)
        : base(message, innerException)
    {
        Loai = loai;
        TenRangBuoc = tenRangBuoc;
    }

    public LoaiLoiDuLieu Loai { get; }

    /// <summary>Tên ràng buộc bị vi phạm (ví dụ "UQ_MonHoc_TenMH"), nếu có.</summary>
    public string? TenRangBuoc { get; }
}
