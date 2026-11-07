using ThiTracNghiem.BLL;
using ThiTracNghiem.Models;

namespace ThiTracNghiem.GUI;

public partial class frmMonHoc : FormCoSo
{
    private readonly MonHocService _monHocService = new();
    private List<MonHoc> _danhSach = new();

    /// <summary>Mã môn học đang sửa; 0 = đang thêm mới.</summary>
    private int _maDangChon;
    private bool _dangNapLuoi;

    public frmMonHoc()
    {
        InitializeComponent();
    }

    private void frmMonHoc_Load(object sender, EventArgs e)
    {
        CaiDatLuoi(dgvMonHoc);
    }

    private void frmMonHoc_Shown(object sender, EventArgs e)
    {
        TaiDuLieu();
        LamMoiForm();
    }

    private void TaiDuLieu(int maCanChon = 0)
    {
        if (!ThucHien(() => _danhSach = _monHocService.TimKiem(txtTimKiem.Text)))
            return;

        _dangNapLuoi = true;
        dgvMonHoc.DataSource = _danhSach
            .Select(mh => new { mh.MaMH, mh.TenMH, mh.SoTinChi, mh.MoTa })
            .ToList();
        DatCot(dgvMonHoc, "MaMH", "Mã", 15);
        DatCot(dgvMonHoc, "TenMH", "Tên môn học", 60);
        DatCot(dgvMonHoc, "SoTinChi", "Số tín chỉ", 20);
        DatCot(dgvMonHoc, "MoTa", "Mô tả", 80);
        ChonDong(dgvMonHoc, "MaMH", maCanChon);
        _dangNapLuoi = false;

        if (maCanChon == 0)
            LamMoiForm();
    }

    private void dgvMonHoc_SelectionChanged(object sender, EventArgs e)
    {
        if (_dangNapLuoi || LayMaDongChon(dgvMonHoc, "MaMH") is not int ma)
            return;

        var monHoc = _danhSach.First(mh => mh.MaMH == ma);
        _maDangChon = ma;
        txtTenMH.Text = monHoc.TenMH;
        nudSoTinChi.Value = monHoc.SoTinChi;
        txtMoTa.Text = monHoc.MoTa;
        lblCheDo.Text = $"Đang sửa môn học mã {ma}";
    }

    private void LamMoiForm()
    {
        _maDangChon = 0;
        txtTenMH.Clear();
        nudSoTinChi.Value = 3;
        txtMoTa.Clear();
        lblCheDo.Text = "Đang thêm môn học mới";
        dgvMonHoc.ClearSelection();
        txtTenMH.Focus();
    }

    private void btnTim_Click(object sender, EventArgs e) => TaiDuLieu();

    private void btnThemMoi_Click(object sender, EventArgs e) => LamMoiForm();

    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        txtTimKiem.Clear();
        TaiDuLieu();
    }

    private void btnLuu_Click(object sender, EventArgs e)
    {
        var monHoc = new MonHoc
        {
            MaMH = _maDangChon,
            TenMH = txtTenMH.Text,
            SoTinChi = (int)nudSoTinChi.Value,
            MoTa = txtMoTa.Text
        };

        bool laThemMoi = _maDangChon == 0;
        bool thanhCong = ThucHien(() =>
        {
            if (laThemMoi)
                _monHocService.Them(monHoc);
            else
                _monHocService.CapNhat(monHoc);
        });

        if (!thanhCong)
            return;

        HienThongBao(laThemMoi ? "Đã thêm môn học." : "Đã cập nhật môn học.");
        TaiDuLieu(monHoc.MaMH);
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        if (_maDangChon == 0)
        {
            HienCanhBao("Hãy chọn môn học cần xóa trong danh sách.");
            return;
        }

        if (!XacNhan($"Xóa môn học \"{txtTenMH.Text}\"?"))
            return;

        if (ThucHien(() => _monHocService.Xoa(_maDangChon)))
        {
            HienThongBao("Đã xóa môn học.");
            TaiDuLieu();
        }
    }
}
