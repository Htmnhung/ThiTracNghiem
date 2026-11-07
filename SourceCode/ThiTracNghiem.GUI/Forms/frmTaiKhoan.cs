using ThiTracNghiem.BLL;
using ThiTracNghiem.Models;

namespace ThiTracNghiem.GUI;

public partial class frmTaiKhoan : FormCoSo
{
    private readonly TaiKhoanService _taiKhoanService = new();
    private List<TaiKhoan> _danhSach = new();

    /// <summary>Mã tài khoản đang sửa; 0 = đang thêm mới.</summary>
    private int _maDangChon;
    private bool _dangNapLuoi;

    public frmTaiKhoan()
    {
        InitializeComponent();
    }

    private void frmTaiKhoan_Load(object sender, EventArgs e)
    {
        CaiDatLuoi(dgvTaiKhoan);
        cboVaiTroLoc.NapDuLieu(MucChon.TuEnum<VaiTro>(v => v.HienThi(), coMucTatCa: true));
        cboVaiTro.NapDuLieu(MucChon.TuEnum<VaiTro>(v => v.HienThi()));
        cboVaiTro.ChonGiaTri(VaiTro.SinhVien);
        NapDanhSachLop();
    }

    private void frmTaiKhoan_Shown(object sender, EventArgs e) => TaiDuLieu();

    private void NapDanhSachLop()
    {
        List<string> dsLop = new();
        if (!ThucHien(() => dsLop = _taiKhoanService.LayDanhSachLop()))
            return;

        cboLopLoc.NapDuLieu(MucChon.TuDanhSach(dsLop, lop => lop, lop => lop, coMucTatCa: true));
        cboLop.Items.Clear();
        cboLop.Items.AddRange(dsLop.ToArray<object>());
    }

    private void TaiDuLieu(int maCanChon = 0)
    {
        bool thanhCong = ThucHien(() => _danhSach = _taiKhoanService.TimKiem(
            cboVaiTroLoc.LayGiaTri<VaiTro>(),
            cboLopLoc.GiaTriChon() as string,
            txtTuKhoa.Text));

        if (!thanhCong)
            return;

        _dangNapLuoi = true;
        dgvTaiKhoan.DataSource = _danhSach.Select(tk => new
        {
            tk.MaTK,
            tk.TenDangNhap,
            tk.HoTen,
            VaiTro = tk.VaiTro.HienThi(),
            tk.Lop,
            tk.Email,
            TrangThai = tk.DangHoatDong ? "Hoạt động" : "Bị khóa"
        }).ToList();

        AnCot(dgvTaiKhoan, "MaTK");
        DatCot(dgvTaiKhoan, "TenDangNhap", "Tên đăng nhập", 35);
        DatCot(dgvTaiKhoan, "HoTen", "Họ tên", 55);
        DatCot(dgvTaiKhoan, "VaiTro", "Vai trò", 30);
        DatCot(dgvTaiKhoan, "Lop", "Lớp", 25);
        DatCot(dgvTaiKhoan, "Email", "Email", 60);
        DatCot(dgvTaiKhoan, "TrangThai", "Trạng thái", 28);
        ChonDong(dgvTaiKhoan, "MaTK", maCanChon);
        _dangNapLuoi = false;

        if (maCanChon == 0)
            LamMoiForm();
    }

    private void dgvTaiKhoan_SelectionChanged(object sender, EventArgs e)
    {
        if (_dangNapLuoi || LayMaDongChon(dgvTaiKhoan, "MaTK") is not int ma)
            return;

        var taiKhoan = _danhSach.First(tk => tk.MaTK == ma);
        _maDangChon = ma;

        txtTenDangNhap.Text = taiKhoan.TenDangNhap;
        cboVaiTro.ChonGiaTri(taiKhoan.VaiTro);
        txtHoTen.Text = taiKhoan.HoTen;
        cboLop.Text = taiKhoan.Lop ?? string.Empty;
        txtEmail.Text = taiKhoan.Email;
        txtMatKhau.Clear();
        chkDangHoatDong.Checked = taiKhoan.DangHoatDong;
        lblCheDo.Text = $"Đang sửa tài khoản {taiKhoan.TenDangNhap}";
    }

    private void LamMoiForm()
    {
        _maDangChon = 0;
        txtTenDangNhap.Clear();
        cboVaiTro.ChonGiaTri(VaiTro.SinhVien);
        txtHoTen.Clear();
        cboLop.Text = string.Empty;
        txtEmail.Clear();
        txtMatKhau.Clear();
        chkDangHoatDong.Checked = true;
        lblCheDo.Text = "Đang thêm tài khoản mới (nhập mật khẩu ban đầu)";
        dgvTaiKhoan.ClearSelection();
        txtTenDangNhap.Focus();
    }

    private void btnTim_Click(object sender, EventArgs e) => TaiDuLieu();

    private void btnThemMoi_Click(object sender, EventArgs e) => LamMoiForm();

    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        cboVaiTroLoc.SelectedIndex = 0;
        txtTuKhoa.Clear();
        NapDanhSachLop();
        TaiDuLieu();
    }

    private void btnLuu_Click(object sender, EventArgs e)
    {
        var taiKhoan = new TaiKhoan
        {
            MaTK = _maDangChon,
            TenDangNhap = txtTenDangNhap.Text,
            HoTen = txtHoTen.Text,
            VaiTro = cboVaiTro.LayGiaTri<VaiTro>() ?? VaiTro.SinhVien,
            Lop = cboLop.Text,
            Email = txtEmail.Text,
            DangHoatDong = chkDangHoatDong.Checked
        };

        bool laThemMoi = _maDangChon == 0;
        bool thanhCong = ThucHien(() =>
        {
            if (laThemMoi)
                _taiKhoanService.Them(taiKhoan, txtMatKhau.Text);
            else
                _taiKhoanService.CapNhat(taiKhoan);
        });

        if (!thanhCong)
            return;

        HienThongBao(laThemMoi ? "Đã thêm tài khoản." : "Đã cập nhật tài khoản.");
        NapDanhSachLop();
        TaiDuLieu(taiKhoan.MaTK);
    }

    private void btnDatLaiMatKhau_Click(object sender, EventArgs e)
    {
        if (_maDangChon == 0)
        {
            HienCanhBao("Hãy chọn tài khoản cần đặt lại mật khẩu.");
            return;
        }

        if (!XacNhan($"Đặt lại mật khẩu cho tài khoản {txtTenDangNhap.Text} bằng mật khẩu vừa nhập?"))
            return;

        if (ThucHien(() => _taiKhoanService.DatLaiMatKhau(_maDangChon, txtMatKhau.Text)))
        {
            txtMatKhau.Clear();
            HienThongBao("Đã đặt lại mật khẩu.");
        }
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        if (_maDangChon == 0)
        {
            HienCanhBao("Hãy chọn tài khoản cần xóa trong danh sách.");
            return;
        }

        if (!XacNhan($"Xóa tài khoản {txtTenDangNhap.Text}?"))
            return;

        if (ThucHien(() => _taiKhoanService.Xoa(_maDangChon)))
        {
            HienThongBao("Đã xóa tài khoản.");
            TaiDuLieu();
        }
    }
}
