namespace ThiTracNghiem.Models;

/// <summary>Môn học. Mỗi môn có một ngân hàng câu hỏi và nhiều đề thi.</summary>
public class MonHoc
{
    public const int SoTinChiToiThieu = 1;
    public const int SoTinChiToiDa = 10;

    private string _tenMH = string.Empty;
    private string? _moTa;

    public int MaMH { get; set; }

    public string TenMH
    {
        get => _tenMH;
        set => _tenMH = value?.Trim() ?? string.Empty;
    }

    public int SoTinChi { get; set; } = 3;

    public string? MoTa
    {
        get => _moTa;
        set => _moTa = string.IsNullOrWhiteSpace(value) ? null : value.Trim();
    }

    public List<string> KiemTraHopLe()
    {
        var loi = new List<string>();

        if (string.IsNullOrEmpty(TenMH))
            loi.Add("Tên môn học không được để trống.");
        else if (TenMH.Length > 100)
            loi.Add("Tên môn học tối đa 100 ký tự.");

        if (SoTinChi < SoTinChiToiThieu || SoTinChi > SoTinChiToiDa)
            loi.Add($"Số tín chỉ phải từ {SoTinChiToiThieu} đến {SoTinChiToiDa}.");

        if (MoTa?.Length > 500)
            loi.Add("Mô tả tối đa 500 ký tự.");

        return loi;
    }

    /// <summary>Hiển thị tên môn khi gắn vào ComboBox.</summary>
    public override string ToString() => TenMH;
}
