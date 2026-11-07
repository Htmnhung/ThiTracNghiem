namespace ThiTracNghiem.Models;

/// <summary>Câu hỏi có 4 phương án và đúng 1 đáp án đúng.</summary>
public class CauHoiMotDapAn : CauHoi
{
    public override LoaiCauHoi Loai => LoaiCauHoi.MotDapAn;

    public override bool ChoPhepChonNhieu => false;

    /// <summary>Đúng khi sinh viên chọn đúng một phương án và trùng đáp án.</summary>
    public override bool KiemTraDapAn(string? dapAnChon)
    {
        var daChon = DapAnHelper.ChuanHoaHoacNull(dapAnChon);
        return daChon is { Length: 1 } && daChon == DapAnDung;
    }

    public override List<string> KiemTraHopLe()
    {
        var loi = base.KiemTraHopLe();

        if (!CoDuBonPhuongAn)
            loi.Add("Câu một đáp án phải có đủ 4 phương án A, B, C, D.");

        if (DapAnDung.Length > 1)
            loi.Add("Câu một đáp án chỉ được có đúng 1 đáp án đúng.");

        return loi;
    }
}
