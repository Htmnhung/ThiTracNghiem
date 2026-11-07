using System.Data;
using static ThiTracNghiem.DAL.DbHelper;

namespace ThiTracNghiem.DAL;

/// <summary>
/// Các truy vấn thống kê / báo cáo. Kết quả trả về dạng DataTable để gắn trực tiếp vào DataGridView
/// hoặc biểu đồ, vì đây là dữ liệu tổng hợp chứ không phải thực thể nghiệp vụ.
/// </summary>
public class ThongKeDAL
{
    /// <summary>Số lượt thi, điểm trung bình / cao nhất / thấp nhất, tỉ lệ đạt theo đề (null = tất cả đề).</summary>
    public DataTable ThongKeDiemTheoDe(int? maDe = null) =>
        ExecuteProcedure("dbo.sp_ThongKeDiemTheoDe", ThamSo("@MaDe", maDe));

    /// <summary>Số lượt thi theo từng khoảng điểm của một đề (dùng vẽ biểu đồ).</summary>
    public DataTable PhoDiem(int maDe) =>
        ExecuteProcedure("dbo.sp_PhoDiem", ThamSo("@MaDe", maDe));

    /// <summary>Các câu hỏi bị làm sai nhiều nhất (null = mọi môn).</summary>
    public DataTable TopCauHoiSaiNhieu(int? maMH = null, int soLuong = 10) =>
        ExecuteProcedure("dbo.sp_TopCauHoiSaiNhieu", ThamSo("@MaMH", maMH), ThamSo("@SoLuong", soLuong));

    /// <summary>Sinh viên (đang hoạt động) chưa nộp bài lần nào cho đề thi, có thể lọc theo lớp.</summary>
    public DataTable SinhVienChuaThi(int maDe, string? lop = null) =>
        ExecuteQuery(@"
            SELECT tk.MaTK, tk.TenDangNhap AS MaSV, tk.HoTen, tk.Lop, tk.Email
            FROM dbo.TaiKhoan tk
            WHERE tk.VaiTro = 'SinhVien' AND tk.TrangThai = 1
              AND (@Lop IS NULL OR tk.Lop = @Lop)
              AND NOT EXISTS (SELECT 1 FROM dbo.LuotThi lt
                              WHERE lt.MaTK = tk.MaTK AND lt.MaDe = @MaDe AND lt.TrangThai = 'DaNop')
            ORDER BY tk.Lop, tk.TenDangNhap",
            ThamSo("@MaDe", maDe),
            ThamSo("@Lop", lop));
}
