namespace ThiTracNghiem.Models;

/// <summary>
/// Đề thi lưu "cấu trúc đề": số câu dễ / trung bình / khó, thời gian làm bài,
/// khoảng thời gian mở đề và số lần thi tối đa. Câu hỏi cụ thể được bốc ngẫu nhiên cho từng lượt thi.
/// </summary>
public class DeThi
{
    public const int ThoiGianLamToiThieu = 5;
    public const int ThoiGianLamToiDa = 180;
    public const int TongSoCauToiDa = 100;
    public const int GioiHanSoLanThi = 10;

    private string _tenDe = string.Empty;

    public int MaDe { get; set; }
    public int MaMH { get; set; }

    /// <summary>Tên môn học (chỉ dùng để hiển thị).</summary>
    public string? TenMonHoc { get; set; }

    public string TenDe
    {
        get => _tenDe;
        set => _tenDe = value?.Trim() ?? string.Empty;
    }

    /// <summary>Thời gian làm bài, tính bằng phút.</summary>
    public int ThoiGianLam { get; set; } = 15;

    public int SoCauDe { get; set; }
    public int SoCauTB { get; set; }
    public int SoCauKho { get; set; }

    public DateTime ThoiGianMo { get; set; } = DateTime.Today;
    public DateTime ThoiGianDong { get; set; } = DateTime.Today.AddDays(7);

    public int SoLanThiToiDa { get; set; } = 1;

    public int NguoiTao { get; set; }
    public DateTime NgayTao { get; set; } = DateTime.Now;

    public int TongSoCau => SoCauDe + SoCauTB + SoCauKho;

    /// <summary>Số câu cần bốc theo độ khó.</summary>
    public int SoCauTheoDoKho(DoKho doKho) => doKho switch
    {
        DoKho.De => SoCauDe,
        DoKho.TrungBinh => SoCauTB,
        DoKho.Kho => SoCauKho,
        _ => 0
    };

    public bool DangMo(DateTime thoiDiem) => thoiDiem >= ThoiGianMo && thoiDiem <= ThoiGianDong;

    public string TrangThaiHienThi(DateTime thoiDiem) =>
        thoiDiem < ThoiGianMo ? "Chưa mở"
        : thoiDiem > ThoiGianDong ? "Đã đóng"
        : "Đang mở";

    public List<string> KiemTraHopLe()
    {
        var loi = new List<string>();

        if (MaMH <= 0)
            loi.Add("Chưa chọn môn học.");

        if (string.IsNullOrEmpty(TenDe))
            loi.Add("Tên đề thi không được để trống.");
        else if (TenDe.Length > 200)
            loi.Add("Tên đề thi tối đa 200 ký tự.");

        if (ThoiGianLam < ThoiGianLamToiThieu || ThoiGianLam > ThoiGianLamToiDa)
            loi.Add($"Thời gian làm bài phải từ {ThoiGianLamToiThieu} đến {ThoiGianLamToiDa} phút.");

        if (SoCauDe < 0 || SoCauTB < 0 || SoCauKho < 0)
            loi.Add("Số câu theo từng độ khó không được âm.");
        else if (TongSoCau < 1 || TongSoCau > TongSoCauToiDa)
            loi.Add($"Tổng số câu phải từ 1 đến {TongSoCauToiDa}.");

        if (ThoiGianDong <= ThoiGianMo)
            loi.Add("Thời gian đóng đề phải sau thời gian mở đề.");

        if (SoLanThiToiDa < 1 || SoLanThiToiDa > GioiHanSoLanThi)
            loi.Add($"Số lần thi tối đa phải từ 1 đến {GioiHanSoLanThi}.");

        return loi;
    }

    public override string ToString() => TenDe;
}
