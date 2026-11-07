using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace ThiTracNghiem.GUI;

/// <summary>
/// Biểu đồ cột đơn giản tự vẽ bằng GDI+ (WinForms trên .NET không còn sẵn control Chart).
/// Dùng để hiển thị phổ điểm của đề thi.
/// </summary>
public class BieuDoCot : Control
{
    private static readonly Color MauCot = Color.FromArgb(70, 130, 180);
    private static readonly Color MauLuoi = Color.FromArgb(225, 225, 225);

    private List<(string Nhan, double GiaTri)> _duLieu = new();
    private string _tieuDe = string.Empty;

    public BieuDoCot()
    {
        DoubleBuffered = true;
        ResizeRedraw = true;
        BackColor = Color.White;
    }

    [Category("Appearance")]
    [DefaultValue("")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public string TieuDe
    {
        get => _tieuDe;
        set
        {
            _tieuDe = value ?? string.Empty;
            Invalidate();
        }
    }

    public void DatDuLieu(IEnumerable<(string Nhan, double GiaTri)> duLieu)
    {
        _duLieu = duLieu.ToList();
        Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias;
        g.Clear(BackColor);

        using var fontTieuDe = new Font(Font.FontFamily, Font.Size + 2, FontStyle.Bold);
        using var butChu = new SolidBrush(ForeColor);
        using var dinhDangGiua = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

        g.DrawString(TieuDe, fontTieuDe, butChu, new RectangleF(0, 4, Width, 26), dinhDangGiua);

        var vung = new Rectangle(50, 40, Width - 70, Height - 80);
        if (vung.Width < 50 || vung.Height < 30)
            return;

        if (_duLieu.Count == 0)
        {
            g.DrawString("Chưa có dữ liệu", Font, Brushes.Gray, vung, dinhDangGiua);
            return;
        }

        // Trục tung: chia 5 vạch, giá trị lớn nhất làm tròn lên số nguyên
        double lonNhat = Math.Max(1, Math.Ceiling(_duLieu.Max(d => d.GiaTri)));
        const int soVach = 5;
        using var butLuoi = new Pen(MauLuoi);
        using var dinhDangPhai = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };

        for (int i = 0; i <= soVach; i++)
        {
            float y = vung.Bottom - vung.Height * i / (float)soVach;
            g.DrawLine(butLuoi, vung.Left, y, vung.Right, y);
            g.DrawString((lonNhat * i / soVach).ToString("0.#"), Font, Brushes.DimGray,
                new RectangleF(0, y - 10, vung.Left - 6, 20), dinhDangPhai);
        }

        // Các cột
        float doRongO = vung.Width / (float)_duLieu.Count;
        float doRongCot = doRongO * 0.6f;
        using var coCot = new SolidBrush(MauCot);

        for (int i = 0; i < _duLieu.Count; i++)
        {
            var (nhan, giaTri) = _duLieu[i];
            float chieuCao = (float)(vung.Height * giaTri / lonNhat);
            float x = vung.Left + doRongO * i + (doRongO - doRongCot) / 2;
            var cot = new RectangleF(x, vung.Bottom - chieuCao, doRongCot, chieuCao);

            g.FillRectangle(coCot, cot);
            g.DrawString(giaTri.ToString("0.##"), Font, butChu,
                new RectangleF(vung.Left + doRongO * i, cot.Top - 20, doRongO, 18), dinhDangGiua);
            g.DrawString(nhan, Font, butChu,
                new RectangleF(vung.Left + doRongO * i, vung.Bottom + 4, doRongO, 34), dinhDangGiua);
        }

        g.DrawLine(Pens.Gray, vung.Left, vung.Bottom, vung.Right, vung.Bottom);
    }
}
