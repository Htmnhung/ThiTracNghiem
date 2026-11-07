using ThiTracNghiem.BLL;
using ThiTracNghiem.Models;

namespace ThiTracNghiem.GUI;

public partial class frmDeThi : FormCoSo
{
    private readonly DeThiService _deThiService = new();
    private readonly CauHoiService _cauHoiService = new();
    private readonly MonHocService _monHocService = new();
    private List<DeThi> _danhSach = new();

    /// <summary>Mã đề đang sửa; 0 = đang thêm mới.</summary>
    private int _maDangChon;
    private bool _dangNapLuoi;

    public frmDeThi()
    {
        InitializeComponent();
    }

    private void frmDeThi_Load(object sender, EventArgs e)
    {
        CaiDatLuoi(dgvDeThi);

        cboTrangThaiLoc.NapDuLieu(new[]
        {
            new MucChon(LocTrangThaiDe.TatCa, MucChon.TatCa),
            new MucChon(LocTrangThaiDe.DangMo, "Đang mở"),
            new MucChon(LocTrangThaiDe.ChuaMo, "Chưa mở"),
            new MucChon(LocTrangThaiDe.DaDong, "Đã đóng")
        });

        List<MonHoc> dsMon = new();
        if (ThucHien(() => dsMon = _monHocService.LayTatCa()))
        {
            cboMonHocLoc.NapDuLieu(MucChon.TuDanhSach(dsMon, m => m.MaMH, m => m.TenMH, coMucTatCa: true));
            cboMonHoc.NapDuLieu(MucChon.TuDanhSach(dsMon, m => m.MaMH, m => m.TenMH));
        }
    }

    private void frmDeThi_Shown(object sender, EventArgs e) => TaiDuLieu();

    private void TaiDuLieu(int maCanChon = 0)
    {
        bool thanhCong = ThucHien(() => _danhSach = _deThiService.TimKiem(
            cboMonHocLoc.LayGiaTri<int>(),
            txtTuKhoa.Text,
            cboTrangThaiLoc.LayGiaTri<LocTrangThaiDe>() ?? LocTrangThaiDe.TatCa));

        if (!thanhCong)
            return;

        var bayGio = DateTime.Now;
        _dangNapLuoi = true;
        dgvDeThi.DataSource = _danhSach.Select(de => new
        {
            de.MaDe,
            de.TenDe,
            de.TenMonHoc,
            de.ThoiGianLam,
            de.TongSoCau,
            de.ThoiGianMo,
            de.ThoiGianDong,
            de.SoLanThiToiDa,
            TrangThai = de.TrangThaiHienThi(bayGio)
        }).ToList();

        DatCot(dgvDeThi, "MaDe", "Mã", 12);
        DatCot(dgvDeThi, "TenDe", "Tên đề", 90);
        DatCot(dgvDeThi, "TenMonHoc", "Môn học", 55);
        DatCot(dgvDeThi, "ThoiGianLam", "Phút", 15);
        DatCot(dgvDeThi, "TongSoCau", "Số câu", 18);
        DatCot(dgvDeThi, "ThoiGianMo", "Mở", 40, "dd/MM/yyyy HH:mm");
        DatCot(dgvDeThi, "ThoiGianDong", "Đóng", 40, "dd/MM/yyyy HH:mm");
        DatCot(dgvDeThi, "SoLanThiToiDa", "Số lần", 18);
        DatCot(dgvDeThi, "TrangThai", "Trạng thái", 25);
        ChonDong(dgvDeThi, "MaDe", maCanChon);
        _dangNapLuoi = false;

        if (maCanChon == 0)
            LamMoiForm();
    }

    private void dgvDeThi_SelectionChanged(object sender, EventArgs e)
    {
        if (_dangNapLuoi || LayMaDongChon(dgvDeThi, "MaDe") is not int ma)
            return;

        var deThi = _danhSach.First(de => de.MaDe == ma);
        _maDangChon = ma;

        txtTenDe.Text = deThi.TenDe;
        cboMonHoc.ChonGiaTri(deThi.MaMH);
        nudThoiGianLam.Value = deThi.ThoiGianLam;
        nudSoCauDe.Value = deThi.SoCauDe;
        nudSoCauTB.Value = deThi.SoCauTB;
        nudSoCauKho.Value = deThi.SoCauKho;
        dtpThoiGianMo.Value = deThi.ThoiGianMo;
        dtpThoiGianDong.Value = deThi.ThoiGianDong;
        nudSoLanThi.Value = deThi.SoLanThiToiDa;

        bool daCoNguoiThi = false;
        ThucHien(() => daCoNguoiThi = _deThiService.DaCoNguoiThi(ma));
        KhoaCauTrucDe(daCoNguoiThi);
        lblCheDo.Text = $"Đang sửa đề thi mã {ma}";
    }

    /// <summary>Đề đã có người thi: khóa các trường thuộc cấu trúc đề.</summary>
    private void KhoaCauTrucDe(bool khoa)
    {
        cboMonHoc.Enabled = nudThoiGianLam.Enabled = !khoa;
        nudSoCauDe.Enabled = nudSoCauTB.Enabled = nudSoCauKho.Enabled = !khoa;
        lblCanhBao.Text = khoa
            ? "Đề đã có sinh viên làm bài: chỉ được sửa tên đề, thời gian mở/đóng và số lần thi tối đa."
            : string.Empty;
    }

    private void LamMoiForm()
    {
        _maDangChon = 0;
        var gioHienTai = DateTime.Today.AddHours(DateTime.Now.Hour + 1);

        txtTenDe.Clear();
        nudThoiGianLam.Value = 15;
        nudSoCauDe.Value = nudSoCauTB.Value = nudSoCauKho.Value = 0;
        dtpThoiGianMo.Value = gioHienTai;
        dtpThoiGianDong.Value = gioHienTai.AddDays(7);
        nudSoLanThi.Value = 1;
        KhoaCauTrucDe(false);
        lblCheDo.Text = "Đang thêm đề thi mới";
        dgvDeThi.ClearSelection();
        txtTenDe.Focus();
    }

    private void cboMonHoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (cboMonHoc.LayGiaTri<int>() is not int maMH)
            return;

        Dictionary<DoKho, int> soCau = new();
        if (ThucHien(() => soCau = _cauHoiService.DemTheoDoKho(maMH)))
        {
            lblNganHang.Text = $"Ngân hàng câu hỏi của môn: {soCau[DoKho.De]} dễ · " +
                               $"{soCau[DoKho.TrungBinh]} trung bình · {soCau[DoKho.Kho]} khó";
        }
    }

    private void nudSoCau_ValueChanged(object sender, EventArgs e)
    {
        lblTongSoCau.Text = $"Tổng: {nudSoCauDe.Value + nudSoCauTB.Value + nudSoCauKho.Value} câu";
    }

    private void btnTim_Click(object sender, EventArgs e) => TaiDuLieu();

    private void btnThemMoi_Click(object sender, EventArgs e) => LamMoiForm();

    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        cboMonHocLoc.SelectedIndex = 0;
        cboTrangThaiLoc.SelectedIndex = 0;
        txtTuKhoa.Clear();
        TaiDuLieu();
    }

    private void btnLuu_Click(object sender, EventArgs e)
    {
        var deThi = new DeThi
        {
            MaDe = _maDangChon,
            MaMH = cboMonHoc.LayGiaTri<int>() ?? 0,
            TenDe = txtTenDe.Text,
            ThoiGianLam = (int)nudThoiGianLam.Value,
            SoCauDe = (int)nudSoCauDe.Value,
            SoCauTB = (int)nudSoCauTB.Value,
            SoCauKho = (int)nudSoCauKho.Value,
            ThoiGianMo = dtpThoiGianMo.Value,
            ThoiGianDong = dtpThoiGianDong.Value,
            SoLanThiToiDa = (int)nudSoLanThi.Value
        };

        bool laThemMoi = _maDangChon == 0;
        bool thanhCong = ThucHien(() =>
        {
            if (laThemMoi)
                _deThiService.Them(deThi);
            else
                _deThiService.CapNhat(deThi);
        });

        if (!thanhCong)
            return;

        HienThongBao(laThemMoi ? "Đã thêm đề thi." : "Đã cập nhật đề thi.");
        TaiDuLieu(deThi.MaDe);
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        if (_maDangChon == 0)
        {
            HienCanhBao("Hãy chọn đề thi cần xóa trong danh sách.");
            return;
        }

        if (!XacNhan($"Xóa đề thi \"{txtTenDe.Text}\"?"))
            return;

        if (ThucHien(() => _deThiService.Xoa(_maDangChon)))
        {
            HienThongBao("Đã xóa đề thi.");
            TaiDuLieu();
        }
    }
}
