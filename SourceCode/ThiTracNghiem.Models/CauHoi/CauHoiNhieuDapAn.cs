namespace ThiTracNghiem.Models;

/// <summary>Câu hỏi có 4 phương án và từ 2 đáp án đúng trở lên.</summary>
public class CauHoiNhieuDapAn : CauHoi
{
    public override LoaiCauHoi Loai => LoaiCauHoi.NhieuDapAn;

    public override bool ChoPhepChonNhieu => true;

    /// <summary>
    /// Đúng khi tập phương án sinh viên chọn trùng khớp hoàn toàn với tập đáp án đúng
    /// (không phụ thuộc thứ tự chọn: "CA" và "AC" là như nhau).
    /// </summary>
    public override bool KiemTraDapAn(string? dapAnChon)
    {
        var daChon = DapAnHelper.ChuanHoaHoacNull(dapAnChon);
        if (daChon == null)
            return false;

        return new HashSet<char>(daChon).SetEquals(DapAnDung);
    }

    public override List<string> KiemTraHopLe()
    {
        var loi = base.KiemTraHopLe();

        if (!CoDuBonPhuongAn)
            loi.Add("Câu nhiều đáp án phải có đủ 4 phương án A, B, C, D.");

        if (DapAnDung.Length is > 0 and < 2)
            loi.Add("Câu nhiều đáp án phải có ít nhất 2 đáp án đúng.");

        return loi;
    }
}
