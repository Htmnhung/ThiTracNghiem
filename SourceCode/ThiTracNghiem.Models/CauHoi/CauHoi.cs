namespace ThiTracNghiem.Models;

/// <summary>Một phương án trả lời: ký hiệu (A-D) và nội dung.</summary>
public readonly record struct PhuongAn(char KyHieu, string NoiDung);

/// <summary>
/// Lớp trừu tượng cho mọi loại câu hỏi.
/// <para>
/// Chọn abstract class (thay vì interface) vì các loại câu hỏi có chung dữ liệu
/// (nội dung, phương án, độ khó...) và chung một phần kiểm tra hợp lệ;
/// chỉ cách kiểm tra đáp án và một số quy tắc là khác nhau → để lớp con override.
/// </para>
/// </summary>
public abstract class CauHoi
{
    public const int DoDaiNoiDungToiDa = 1000;
    public const int DoDaiPhuongAnToiDa = 300;

    private string _noiDung = string.Empty;
    private string _phuongAnA = string.Empty;
    private string _phuongAnB = string.Empty;
    private string? _phuongAnC;
    private string? _phuongAnD;
    private string _dapAnDung = string.Empty;

    public int MaCH { get; set; }
    public int MaMH { get; set; }

    /// <summary>Tên môn học (chỉ dùng để hiển thị, lấy từ phép join).</summary>
    public string? TenMonHoc { get; set; }

    public string NoiDung
    {
        get => _noiDung;
        set => _noiDung = value?.Trim() ?? string.Empty;
    }

    public string PhuongAnA
    {
        get => _phuongAnA;
        set => _phuongAnA = ChuanHoaChuoi(value) ?? string.Empty;
    }

    public string PhuongAnB
    {
        get => _phuongAnB;
        set => _phuongAnB = ChuanHoaChuoi(value) ?? string.Empty;
    }

    public string? PhuongAnC
    {
        get => _phuongAnC;
        set => _phuongAnC = ChuanHoaChuoi(value);
    }

    public string? PhuongAnD
    {
        get => _phuongAnD;
        set => _phuongAnD = ChuanHoaChuoi(value);
    }

    /// <summary>Đáp án đúng, luôn ở dạng chuẩn hóa (ví dụ "A", "AC").</summary>
    public string DapAnDung
    {
        get => _dapAnDung;
        set => _dapAnDung = DapAnHelper.ChuanHoa(value) ?? string.Empty;
    }

    public DoKho DoKho { get; set; } = DoKho.De;

    /// <summary>false = câu hỏi đã bị ẩn (xóa mềm), không được bốc vào đề.</summary>
    public bool DangSuDung { get; set; } = true;

    public int NguoiTao { get; set; }
    public DateTime NgayTao { get; set; } = DateTime.Now;

    // ----- Phần mỗi lớp con phải tự định nghĩa -----

    public abstract LoaiCauHoi Loai { get; }

    /// <summary>true: giao diện hiển thị CheckBox (chọn nhiều); false: RadioButton (chọn một).</summary>
    public abstract bool ChoPhepChonNhieu { get; }

    /// <summary>Kiểm tra đáp án sinh viên chọn có đúng hay không.</summary>
    public abstract bool KiemTraDapAn(string? dapAnChon);

    // ----- Phần dùng chung, lớp con có thể mở rộng -----

    /// <summary>Danh sách phương án hiển thị cho sinh viên.</summary>
    public virtual IReadOnlyList<PhuongAn> LayCacPhuongAn() => new List<PhuongAn>
    {
        new('A', PhuongAnA),
        new('B', PhuongAnB),
        new('C', PhuongAnC ?? string.Empty),
        new('D', PhuongAnD ?? string.Empty)
    };

    /// <summary>
    /// Kiểm tra dữ liệu câu hỏi. Trả về danh sách lỗi (rỗng nếu hợp lệ).
    /// Lớp con gọi base.KiemTraHopLe() rồi bổ sung quy tắc riêng.
    /// </summary>
    public virtual List<string> KiemTraHopLe()
    {
        var loi = new List<string>();

        if (MaMH <= 0)
            loi.Add("Chưa chọn môn học.");

        if (string.IsNullOrEmpty(NoiDung))
            loi.Add("Nội dung câu hỏi không được để trống.");
        else if (NoiDung.Length > DoDaiNoiDungToiDa)
            loi.Add($"Nội dung câu hỏi tối đa {DoDaiNoiDungToiDa} ký tự.");

        if (string.IsNullOrEmpty(PhuongAnA) || string.IsNullOrEmpty(PhuongAnB))
            loi.Add("Phương án A và B không được để trống.");

        foreach (var pa in new[] { PhuongAnA, PhuongAnB, PhuongAnC, PhuongAnD })
        {
            if (pa != null && pa.Length > DoDaiPhuongAnToiDa)
            {
                loi.Add($"Mỗi phương án tối đa {DoDaiPhuongAnToiDa} ký tự.");
                break;
            }
        }

        if (string.IsNullOrEmpty(DapAnDung))
            loi.Add("Chưa chọn đáp án đúng.");

        return loi;
    }

    /// <summary>Factory: tạo đúng lớp con theo loại câu hỏi (dùng khi đọc dữ liệu từ CSDL).</summary>
    public static CauHoi TaoTheoLoai(LoaiCauHoi loai) => loai switch
    {
        LoaiCauHoi.MotDapAn => new CauHoiMotDapAn(),
        LoaiCauHoi.NhieuDapAn => new CauHoiNhieuDapAn(),
        LoaiCauHoi.DungSai => new CauHoiDungSai(),
        _ => throw new ArgumentOutOfRangeException(nameof(loai), $"Loại câu hỏi không hợp lệ: {loai}")
    };

    protected bool CoDuBonPhuongAn =>
        !string.IsNullOrEmpty(PhuongAnC) && !string.IsNullOrEmpty(PhuongAnD);

    private static string? ChuanHoaChuoi(string? giaTri) =>
        string.IsNullOrWhiteSpace(giaTri) ? null : giaTri.Trim();

    public override string ToString() => NoiDung;
}
