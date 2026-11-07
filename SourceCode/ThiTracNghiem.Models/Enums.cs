namespace ThiTracNghiem.Models;

// Tên các giá trị enum trùng với giá trị lưu trong CSDL (cột VARCHAR có ràng buộc CHECK),
// nên tầng DAL có thể chuyển đổi trực tiếp bằng Enum.Parse / ToString().

/// <summary>Vai trò của tài khoản (TaiKhoan.VaiTro).</summary>
public enum VaiTro
{
    GiangVien,
    SinhVien
}

/// <summary>Độ khó của câu hỏi (CauHoi.DoKho).</summary>
public enum DoKho
{
    De,
    TrungBinh,
    Kho
}

/// <summary>Loại câu hỏi (CauHoi.LoaiCauHoi). Mỗi loại tương ứng một lớp con của <see cref="CauHoi"/>.</summary>
public enum LoaiCauHoi
{
    MotDapAn,
    NhieuDapAn,
    DungSai
}

/// <summary>Trạng thái của lượt thi (LuotThi.TrangThai).</summary>
public enum TrangThaiLuotThi
{
    DangLam,
    DaNop
}

/// <summary>Tên hiển thị tiếng Việt cho các enum, dùng trên giao diện.</summary>
public static class TenHienThi
{
    public static string HienThi(this VaiTro vaiTro) => vaiTro switch
    {
        VaiTro.GiangVien => "Giảng viên",
        VaiTro.SinhVien => "Sinh viên",
        _ => vaiTro.ToString()
    };

    public static string HienThi(this DoKho doKho) => doKho switch
    {
        DoKho.De => "Dễ",
        DoKho.TrungBinh => "Trung bình",
        DoKho.Kho => "Khó",
        _ => doKho.ToString()
    };

    public static string HienThi(this LoaiCauHoi loai) => loai switch
    {
        LoaiCauHoi.MotDapAn => "Một đáp án",
        LoaiCauHoi.NhieuDapAn => "Nhiều đáp án",
        LoaiCauHoi.DungSai => "Đúng/Sai",
        _ => loai.ToString()
    };

    public static string HienThi(this TrangThaiLuotThi trangThai) => trangThai switch
    {
        TrangThaiLuotThi.DangLam => "Đang làm",
        TrangThaiLuotThi.DaNop => "Đã nộp",
        _ => trangThai.ToString()
    };
}
