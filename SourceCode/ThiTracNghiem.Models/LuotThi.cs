namespace ThiTracNghiem.Models;

/// <summary>
/// Một lượt làm bài của sinh viên.
/// Quan hệ composition với <see cref="ChiTietLuotThi"/>: danh sách chi tiết do lượt thi quản lý,
/// bên ngoài chỉ được đọc, không được thêm/xóa trực tiếp.
/// </summary>
public class LuotThi
{
    private readonly List<ChiTietLuotThi> _chiTiet = new();

    public int MaLT { get; set; }
    public int MaDe { get; set; }
    public int MaTK { get; set; }

    public DateTime ThoiGianBatDau { get; set; } = DateTime.Now;
    public DateTime? ThoiGianNop { get; set; }
    public int? SoCauDung { get; set; }
    public decimal? Diem { get; set; }
    public TrangThaiLuotThi TrangThai { get; set; } = TrangThaiLuotThi.DangLam;

    public IReadOnlyList<ChiTietLuotThi> ChiTiet => _chiTiet;

    public int TongSoCau => _chiTiet.Count;
    public int SoCauDaTraLoi => _chiTiet.Count(ct => ct.DaTraLoi);
    public bool DaNop => TrangThai == TrangThaiLuotThi.DaNop;

    /// <summary>Thêm một câu vào lượt thi (dùng khi tạo lượt thi hoặc đọc từ CSDL).</summary>
    public void ThemChiTiet(ChiTietLuotThi chiTiet)
    {
        ArgumentNullException.ThrowIfNull(chiTiet);

        if (_chiTiet.Any(ct => ct.MaCH == chiTiet.MaCH))
            throw new InvalidOperationException($"Câu hỏi {chiTiet.MaCH} đã có trong lượt thi.");
        if (_chiTiet.Any(ct => ct.ThuTu == chiTiet.ThuTu))
            throw new InvalidOperationException($"Thứ tự câu {chiTiet.ThuTu} bị trùng.");

        chiTiet.MaLT = MaLT;
        _chiTiet.Add(chiTiet);
        _chiTiet.Sort((a, b) => a.ThuTu.CompareTo(b.ThuTu));
    }

    /// <summary>Ghi nhận đáp án sinh viên chọn cho một câu.</summary>
    public void ChonDapAn(int maCH, string? dapAn)
    {
        if (DaNop)
            throw new InvalidOperationException("Bài thi đã nộp, không thể thay đổi đáp án.");

        var chiTiet = _chiTiet.FirstOrDefault(ct => ct.MaCH == maCH)
            ?? throw new KeyNotFoundException($"Câu hỏi {maCH} không thuộc lượt thi này.");

        chiTiet.DapAnChon = dapAn;
    }

    /// <summary>
    /// Nộp bài và chấm điểm tự động.
    /// Mỗi câu được chấm bằng phương thức đa hình KiemTraDapAn của đúng loại câu hỏi,
    /// nên khi thêm loại câu hỏi mới không cần sửa phương thức này.
    /// </summary>
    public decimal NopBai(DateTime thoiGianNop)
    {
        if (DaNop)
            throw new InvalidOperationException("Bài thi đã được nộp trước đó.");
        if (_chiTiet.Count == 0)
            throw new InvalidOperationException("Lượt thi không có câu hỏi nào.");

        foreach (var chiTiet in _chiTiet)
            chiTiet.Cham();

        SoCauDung = _chiTiet.Count(ct => ct.LaDung == true);
        Diem = TinhDiem(SoCauDung.Value, _chiTiet.Count);
        ThoiGianNop = thoiGianNop < ThoiGianBatDau ? ThoiGianBatDau : thoiGianNop;
        TrangThai = TrangThaiLuotThi.DaNop;

        return Diem.Value;
    }

    /// <summary>Điểm = Số câu đúng / Tổng số câu × 10, làm tròn 2 chữ số (giống hàm ROUND của SQL Server).</summary>
    public static decimal TinhDiem(int soCauDung, int tongSoCau)
    {
        if (tongSoCau <= 0)
            throw new ArgumentOutOfRangeException(nameof(tongSoCau), "Tổng số câu phải lớn hơn 0.");
        if (soCauDung < 0 || soCauDung > tongSoCau)
            throw new ArgumentOutOfRangeException(nameof(soCauDung), "Số câu đúng không hợp lệ.");

        return Math.Round(10m * soCauDung / tongSoCau, 2, MidpointRounding.AwayFromZero);
    }

    /// <summary>Thời gian còn lại của bài thi (không âm).</summary>
    public TimeSpan ThoiGianConLai(int thoiGianLamPhut, DateTime thoiDiem)
    {
        var conLai = ThoiGianBatDau.AddMinutes(thoiGianLamPhut) - thoiDiem;
        return conLai > TimeSpan.Zero ? conLai : TimeSpan.Zero;
    }

    public bool DaHetGio(int thoiGianLamPhut, DateTime thoiDiem) =>
        ThoiGianConLai(thoiGianLamPhut, thoiDiem) == TimeSpan.Zero;
}
