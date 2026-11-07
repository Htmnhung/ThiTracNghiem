using ThiTracNghiem.Models;

namespace ThiTracNghiem.BLL;

/// <summary>Lưu tài khoản đang đăng nhập trong suốt thời gian chạy ứng dụng.</summary>
public static class PhienDangNhap
{
    public static TaiKhoan? NguoiDung { get; private set; }

    public static bool DaDangNhap => NguoiDung != null;

    internal static void BatDau(TaiKhoan taiKhoan) => NguoiDung = taiKhoan;

    public static void KetThuc() => NguoiDung = null;
}
