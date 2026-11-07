namespace ThiTracNghiem.Models;

/// <summary>Câu hỏi Đúng/Sai: chỉ có 2 phương án, A = Đúng, B = Sai.</summary>
public class CauHoiDungSai : CauHoi
{
    public CauHoiDungSai()
    {
        PhuongAnA = "Đúng";
        PhuongAnB = "Sai";
    }

    public override LoaiCauHoi Loai => LoaiCauHoi.DungSai;

    public override bool ChoPhepChonNhieu => false;

    /// <summary>Phát biểu trong câu hỏi là đúng hay sai.</summary>
    public bool PhatBieuLaDung => DapAnDung == "A";

    public override bool KiemTraDapAn(string? dapAnChon)
    {
        var daChon = DapAnHelper.ChuanHoaHoacNull(dapAnChon);
        return daChon is "A" or "B" && daChon == DapAnDung;
    }

    /// <summary>Chỉ hiển thị 2 phương án.</summary>
    public override IReadOnlyList<PhuongAn> LayCacPhuongAn() => new List<PhuongAn>
    {
        new('A', PhuongAnA),
        new('B', PhuongAnB)
    };

    public override List<string> KiemTraHopLe()
    {
        var loi = base.KiemTraHopLe();

        if (PhuongAnC != null || PhuongAnD != null)
            loi.Add("Câu Đúng/Sai không được có phương án C, D.");

        if (DapAnDung.Length > 0 && DapAnDung is not ("A" or "B"))
            loi.Add("Đáp án câu Đúng/Sai chỉ được là A (Đúng) hoặc B (Sai).");

        return loi;
    }
}
