using ThiTracNghiem.BLL;
using ThiTracNghiem.Models;

namespace ThiTracNghiem.GUI;

/// <summary>
/// Form làm bài thi, dùng chung cho 2 chế độ:
/// - Làm bài: đồng hồ đếm ngược, chọn đáp án (tự động lưu), nộp bài / tự nộp khi hết giờ.
/// - Xem lại: hiển thị đáp án đã chọn, đáp án đúng và điểm.
/// </summary>
public partial class frmLamBai : FormCoSo
{
    private static readonly Color MauDaTraLoi = Color.FromArgb(187, 222, 251);
    private static readonly Color MauDung = Color.FromArgb(200, 230, 201);
    private static readonly Color MauSai = Color.FromArgb(255, 205, 210);
    private static readonly Color MauChuDung = Color.FromArgb(27, 94, 32);
    private static readonly Color MauChuSai = Color.FromArgb(183, 28, 28);

    private readonly ThiService _thiService = new();
    private readonly BaiThi _baiThi;
    private bool _cheDoXemLai;
    private bool _dangNopBai;
    private bool _dangHienThiCau;
    private int _viTriCau;

    public frmLamBai(BaiThi baiThi, bool cheDoXemLai)
    {
        InitializeComponent();
        _baiThi = baiThi;
        _cheDoXemLai = cheDoXemLai || baiThi.LuotThi.DaNop;
    }

    private LuotThi LuotThi => _baiThi.LuotThi;

    private ChiTietLuotThi CauHienTai => LuotThi.ChiTiet[_viTriCau];

    private void frmLamBai_Load(object sender, EventArgs e)
    {
        var deThi = _baiThi.DeThi;
        lblTenDe.Text = deThi.TenDe;
        lblThongTin.Text = $"{deThi.TenMonHoc}  •  {LuotThi.TongSoCau} câu  •  {deThi.ThoiGianLam} phút  •  " +
                           $"Bắt đầu lúc {LuotThi.ThoiGianBatDau:HH:mm dd/MM/yyyy}";

        TaoDanhSachCau();

        if (_cheDoXemLai)
        {
            ChuyenSangXemLai();
        }
        else
        {
            CapNhatDongHo();
            timerDongHo.Start();
            if (_baiThi.LaBaiLamTiep)
                HienThongBao("Tiếp tục bài đang làm dở. Các đáp án đã chọn trước đó vẫn được giữ nguyên.");
        }

        HienThiCau(0);
    }

    // ===================== Danh sách câu bên trái =====================

    private void TaoDanhSachCau()
    {
        flpDanhSachCau.SuspendLayout();
        for (int i = 0; i < LuotThi.TongSoCau; i++)
        {
            var nut = new Button
            {
                Text = (i + 1).ToString(),
                Tag = i,
                Size = new Size(48, 38),
                Margin = new Padding(3),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                Cursor = Cursors.Hand
            };
            nut.Click += (s, _) => HienThiCau((int)((Button)s!).Tag!);
            flpDanhSachCau.Controls.Add(nut);
        }
        flpDanhSachCau.ResumeLayout();
    }

    private void CapNhatDanhSachCau()
    {
        foreach (Button nut in flpDanhSachCau.Controls)
        {
            var chiTiet = LuotThi.ChiTiet[(int)nut.Tag!];

            nut.BackColor = _cheDoXemLai
                ? (chiTiet.LaDung == true ? MauDung : MauSai)
                : (chiTiet.DaTraLoi ? MauDaTraLoi : Color.White);

            bool laCauHienTai = (int)nut.Tag! == _viTriCau;
            nut.FlatAppearance.BorderSize = laCauHienTai ? 3 : 1;
            nut.FlatAppearance.BorderColor = laCauHienTai ? Color.FromArgb(31, 78, 121) : Color.Silver;
            nut.Font = new Font(Font, laCauHienTai ? FontStyle.Bold : FontStyle.Regular);
        }

        lblDaTraLoi.Text = _cheDoXemLai
            ? $"Đúng {LuotThi.SoCauDung}/{LuotThi.TongSoCau} câu"
            : $"Đã trả lời: {LuotThi.SoCauDaTraLoi}/{LuotThi.TongSoCau} câu";
    }

    // ===================== Hiển thị một câu =====================

    private void HienThiCau(int viTri)
    {
        _viTriCau = viTri;
        var chiTiet = CauHienTai;
        var cauHoi = chiTiet.CauHoi;

        lblSoCau.Text = $"Câu {viTri + 1}/{LuotThi.TongSoCau}";
        lblLoaiCau.Text = $"{cauHoi.Loai.HienThi()}  •  Độ khó: {cauHoi.DoKho.HienThi()}  •  " +
                          (cauHoi.ChoPhepChonNhieu ? "Chọn TẤT CẢ phương án đúng" : "Chọn 1 phương án");
        txtNoiDung.Text = cauHoi.NoiDung;

        _dangHienThiCau = true;
        flpPhuongAn.SuspendLayout();
        foreach (Control cu in flpPhuongAn.Controls.Cast<Control>().ToList())
            cu.Dispose();
        flpPhuongAn.Controls.Clear();

        // Đa hình: số phương án (LayCacPhuongAn) và kiểu chọn (ChoPhepChonNhieu) do từng loại câu hỏi quyết định
        foreach (var phuongAn in cauHoi.LayCacPhuongAn())
        {
            ButtonBase oChon = cauHoi.ChoPhepChonNhieu ? new CheckBox() : new RadioButton();
            oChon.Text = $"{phuongAn.KyHieu}.  {phuongAn.NoiDung}";
            oChon.Tag = phuongAn.KyHieu;
            oChon.AutoSize = true;
            oChon.MaximumSize = new Size(flpPhuongAn.Width - 20, 0);
            oChon.Font = new Font("Segoe UI", 11F);
            oChon.Margin = new Padding(6, 8, 6, 8);
            DatDaChon(oChon, chiTiet.DapAnChon?.Contains(phuongAn.KyHieu) == true);

            if (_cheDoXemLai)
            {
                ToMauKetQua(oChon, cauHoi.DapAnDung.Contains(phuongAn.KyHieu), LaDaChon(oChon));
            }
            else
            {
                if (oChon is CheckBox oVuong)
                    oVuong.CheckedChanged += PhuongAn_CheckedChanged;
                else if (oChon is RadioButton oTron)
                    oTron.CheckedChanged += PhuongAn_CheckedChanged;
            }

            flpPhuongAn.Controls.Add(oChon);
        }
        flpPhuongAn.ResumeLayout();
        _dangHienThiCau = false;

        HienThiKetQuaCau(chiTiet);
        btnCauTruoc.Enabled = viTri > 0;
        btnCauSau.Enabled = viTri < LuotThi.TongSoCau - 1;
        CapNhatDanhSachCau();
    }

    private void HienThiKetQuaCau(ChiTietLuotThi chiTiet)
    {
        if (!_cheDoXemLai)
        {
            lblKetQuaCau.Text = string.Empty;
            return;
        }

        if (chiTiet.LaDung == true)
        {
            lblKetQuaCau.ForeColor = MauChuDung;
            lblKetQuaCau.Text = "✔ Trả lời đúng";
        }
        else
        {
            lblKetQuaCau.ForeColor = MauChuSai;
            lblKetQuaCau.Text = (chiTiet.DapAnChon == null ? "✘ Bỏ trống" : $"✘ Trả lời sai (đã chọn {chiTiet.DapAnChon})")
                                + $"  -  Đáp án đúng: {chiTiet.CauHoi.DapAnDung}";
        }
    }

    private static void ToMauKetQua(ButtonBase oChon, bool laDapAnDung, bool daChon)
    {
        // Chế độ xem lại: không cho đổi lựa chọn nhưng vẫn giữ màu chữ dễ đọc
        if (oChon is CheckBox oVuong)
            oVuong.AutoCheck = false;
        else if (oChon is RadioButton oTron)
            oTron.AutoCheck = false;

        if (laDapAnDung)
        {
            oChon.ForeColor = MauChuDung;
            oChon.Font = new Font(oChon.Font, FontStyle.Bold);
            oChon.Text += "   ✔";
        }
        else if (daChon)
        {
            oChon.ForeColor = MauChuSai;
            oChon.Text += "   ✘";
        }
    }

    private static bool LaDaChon(Control oChon) => oChon switch
    {
        CheckBox oVuong => oVuong.Checked,
        RadioButton oTron => oTron.Checked,
        _ => false
    };

    private static void DatDaChon(Control oChon, bool daChon)
    {
        if (oChon is CheckBox oVuong)
            oVuong.Checked = daChon;
        else if (oChon is RadioButton oTron)
            oTron.Checked = daChon;
    }

    // ===================== Chọn đáp án =====================

    private void PhuongAn_CheckedChanged(object? sender, EventArgs e)
    {
        // RadioButton phát sự kiện 2 lần (bỏ chọn ô cũ, chọn ô mới): chỉ xử lý lần chọn
        if (_dangHienThiCau || (sender is RadioButton oTron && !oTron.Checked))
            return;

        string dapAn = string.Concat(flpPhuongAn.Controls.Cast<Control>()
            .Where(LaDaChon)
            .Select(o => (char)o.Tag!));

        var chiTiet = CauHienTai;
        bool daLuu = ThucHien(() =>
            _thiService.ChonDapAn(_baiThi, chiTiet.MaCH, dapAn.Length == 0 ? null : dapAn));

        if (!daLuu)
        {
            if (_thiService.ThoiGianConLai(_baiThi) == TimeSpan.Zero)
                NopBai(tuDong: true);
            else
                HienThiCau(_viTriCau);   // khôi phục lựa chọn đã lưu trước đó
            return;
        }

        CapNhatDanhSachCau();
    }

    private void btnCauTruoc_Click(object sender, EventArgs e)
    {
        if (_viTriCau > 0)
            HienThiCau(_viTriCau - 1);
    }

    private void btnCauSau_Click(object sender, EventArgs e)
    {
        if (_viTriCau < LuotThi.TongSoCau - 1)
            HienThiCau(_viTriCau + 1);
    }

    // ===================== Đồng hồ và nộp bài =====================

    private void timerDongHo_Tick(object sender, EventArgs e)
    {
        if (CapNhatDongHo() == TimeSpan.Zero)
            NopBai(tuDong: true);
    }

    private TimeSpan CapNhatDongHo()
    {
        var conLai = _thiService.ThoiGianConLai(_baiThi);
        lblDongHo.Text = $"{(int)conLai.TotalMinutes:00}:{conLai.Seconds:00}";
        lblDongHo.ForeColor = conLai.TotalMinutes < 1 ? Color.FromArgb(255, 138, 128)
            : conLai.TotalMinutes < 5 ? Color.Gold
            : Color.White;
        return conLai;
    }

    private void btnNopBai_Click(object sender, EventArgs e)
    {
        int chuaTraLoi = LuotThi.TongSoCau - LuotThi.SoCauDaTraLoi;
        string cauHoi = chuaTraLoi > 0
            ? $"Bạn còn {chuaTraLoi} câu chưa trả lời.\nVẫn nộp bài?"
            : "Bạn có chắc muốn nộp bài?";

        if (XacNhan(cauHoi))
            NopBai(tuDong: false);
    }

    private void NopBai(bool tuDong)
    {
        if (_dangNopBai || _cheDoXemLai)
            return;

        _dangNopBai = true;
        timerDongHo.Stop();

        decimal diem = 0;
        if (ThucHien(() => diem = _thiService.NopBai(_baiThi)))
        {
            HienThongBao((tuDong ? "Đã hết giờ làm bài. Bài thi đã được nộp tự động.\n\n" : string.Empty) +
                         $"Điểm của bạn: {diem:0.00}/10\nSố câu đúng: {LuotThi.SoCauDung}/{LuotThi.TongSoCau}");
            ChuyenSangXemLai();
            HienThiCau(0);
        }
        else if (!tuDong)
        {
            // Nộp thất bại (ví dụ mất kết nối): cho phép thử lại
            timerDongHo.Start();
        }

        _dangNopBai = false;
    }

    private void ChuyenSangXemLai()
    {
        _cheDoXemLai = true;
        timerDongHo.Stop();
        btnNopBai.Visible = false;
        Text = "Xem lại bài thi";
        lblChuThich.Text = "Ô xanh lá: trả lời đúng\r\nÔ đỏ: trả lời sai / bỏ trống";
        lblDongHo.Text = $"{LuotThi.Diem:0.00} điểm";
        lblDongHo.ForeColor = LuotThi.Diem >= 5 ? Color.LightGreen : Color.FromArgb(255, 138, 128);
    }

    private void frmLamBai_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (_cheDoXemLai || e.CloseReason != CloseReason.UserClosing)
            return;

        bool dongForm = XacNhan("Bạn chưa nộp bài.\n\n" +
                                "Nếu thoát, các đáp án đã chọn vẫn được lưu nhưng thời gian làm bài vẫn tiếp tục tính. " +
                                "Bạn có thể vào lại để làm tiếp trước khi hết giờ.\n\nVẫn thoát?");
        if (dongForm)
            timerDongHo.Stop();
        else
            e.Cancel = true;
    }
}
