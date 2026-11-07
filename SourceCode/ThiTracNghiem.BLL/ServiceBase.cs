using ThiTracNghiem.DAL;
using ThiTracNghiem.Models;

namespace ThiTracNghiem.BLL;

/// <summary>
/// Lớp cơ sở của các service: kiểm tra quyền, kiểm tra dữ liệu,
/// và chuyển lỗi của tầng DAL thành <see cref="NghiepVuException"/> với thông báo dễ hiểu.
/// </summary>
public abstract class ServiceBase
{
    /// <summary>Thông báo thân thiện theo tên ràng buộc CSDL bị vi phạm.</summary>
    private static readonly Dictionary<string, string> ThongBaoRangBuoc = new(StringComparer.OrdinalIgnoreCase)
    {
        ["UQ_TaiKhoan_TenDangNhap"] = "Tên đăng nhập đã tồn tại.",
        ["UQ_TaiKhoan_Email"] = "Email đã được sử dụng cho tài khoản khác.",
        ["UQ_MonHoc_TenMH"] = "Tên môn học đã tồn tại.",
        ["FK_CauHoi_MonHoc"] = "Môn học đang có câu hỏi hoặc môn học không tồn tại.",
        ["FK_DeThi_MonHoc"] = "Môn học đang có đề thi hoặc môn học không tồn tại.",
        ["FK_ChiTietLuotThi_CauHoi"] = "Câu hỏi đã được dùng trong bài thi, không thể xóa.",
        ["FK_LuotThi_DeThi"] = "Đề thi đã có sinh viên làm bài, không thể xóa.",
        ["FK_LuotThi_TaiKhoan"] = "Tài khoản đã có dữ liệu liên quan. Hãy khóa tài khoản thay vì xóa.",
        ["FK_CauHoi_TaiKhoan"] = "Tài khoản đã có dữ liệu liên quan. Hãy khóa tài khoản thay vì xóa.",
        ["FK_DeThi_TaiKhoan"] = "Tài khoản đã có dữ liệu liên quan. Hãy khóa tài khoản thay vì xóa."
    };

    /// <summary>Nguồn thời gian hiện tại. Có thể thay đổi khi kiểm thử (ví dụ giả lập hết giờ làm bài).</summary>
    public static Func<DateTime> DongHo { get; set; } = () => DateTime.Now;

    protected static DateTime BayGio => DongHo();

    // ===================== Quyền =====================

    protected static TaiKhoan YeuCauDangNhap() =>
        PhienDangNhap.NguoiDung ?? throw new NghiepVuException("Bạn chưa đăng nhập.");

    protected static TaiKhoan YeuCauGiangVien()
    {
        var nguoiDung = YeuCauDangNhap();
        if (!nguoiDung.LaGiangVien)
            throw new NghiepVuException("Chức năng này chỉ dành cho giảng viên.");
        return nguoiDung;
    }

    protected static TaiKhoan YeuCauSinhVien()
    {
        var nguoiDung = YeuCauDangNhap();
        if (!nguoiDung.LaSinhVien)
            throw new NghiepVuException("Chức năng này chỉ dành cho sinh viên.");
        return nguoiDung;
    }

    // ===================== Kiểm tra dữ liệu =====================

    /// <summary>Có lỗi thì ném NghiepVuException chứa toàn bộ danh sách lỗi.</summary>
    protected static void BaoLoiNeuCo(List<string> danhSachLoi)
    {
        if (danhSachLoi.Count > 0)
            throw new NghiepVuException(danhSachLoi);
    }

    protected static T BatBuocTonTai<T>(T? doiTuong, string thongBao) where T : class =>
        doiTuong ?? throw new NghiepVuException(thongBao);

    // ===================== Xử lý lỗi DAL =====================

    /// <summary>Chạy một nghiệp vụ; mọi DataAccessException được chuyển thành NghiepVuException.</summary>
    protected static T ThucHien<T>(Func<T> nghiepVu)
    {
        try
        {
            return nghiepVu();
        }
        catch (DataAccessException ex)
        {
            throw ChuyenDoiLoi(ex);
        }
    }

    protected static void ThucHien(Action nghiepVu) =>
        ThucHien(() =>
        {
            nghiepVu();
            return true;
        });

    private static NghiepVuException ChuyenDoiLoi(DataAccessException ex)
    {
        if (ex.Loai == LoaiLoiDuLieu.KhongKetNoiDuoc)
            return new NghiepVuException(ex.Message, laLoiKetNoi: true, ex);

        if (ex.TenRangBuoc != null && ThongBaoRangBuoc.TryGetValue(ex.TenRangBuoc, out var thongBao))
            return new NghiepVuException(thongBao, innerException: ex);

        return new NghiepVuException(ex.Message, innerException: ex);
    }
}
