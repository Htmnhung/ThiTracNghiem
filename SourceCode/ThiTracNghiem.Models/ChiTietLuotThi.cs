namespace ThiTracNghiem.Models;

/// <summary>Một câu hỏi trong một lượt thi, kèm đáp án sinh viên đã chọn.</summary>
public class ChiTietLuotThi
{
    private string? _dapAnChon;

    public ChiTietLuotThi(CauHoi cauHoi, int thuTu)
    {
        ArgumentNullException.ThrowIfNull(cauHoi);
        if (thuTu < 1)
            throw new ArgumentOutOfRangeException(nameof(thuTu), "Thứ tự câu phải từ 1 trở lên.");

        CauHoi = cauHoi;
        ThuTu = thuTu;
    }

    public int MaLT { get; set; }

    /// <summary>Câu hỏi (association): kiểu khai báo là lớp cha, đối tượng thực tế là một lớp con.</summary>
    public CauHoi CauHoi { get; }

    public int MaCH => CauHoi.MaCH;

    /// <summary>Thứ tự hiển thị câu trong bài thi (1, 2, 3...).</summary>
    public int ThuTu { get; }

    /// <summary>Đáp án đã chọn (dạng chuẩn hóa) hoặc null nếu bỏ trống.</summary>
    public string? DapAnChon
    {
        get => _dapAnChon;
        set => _dapAnChon = DapAnHelper.ChuanHoa(value);
    }

    /// <summary>Kết quả chấm: null = chưa chấm.</summary>
    public bool? LaDung { get; set; }

    public bool DaTraLoi => DapAnChon != null;

    /// <summary>
    /// Chấm câu này. Lời gọi CauHoi.KiemTraDapAn là đa hình:
    /// tùy đối tượng thực tế là CauHoiMotDapAn, CauHoiNhieuDapAn hay CauHoiDungSai
    /// mà phương thức tương ứng được thực thi.
    /// </summary>
    public bool Cham()
    {
        LaDung = CauHoi.KiemTraDapAn(DapAnChon);
        return LaDung.Value;
    }
}
