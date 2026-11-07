using System.ComponentModel;
using ThiTracNghiem.BLL;
using ThiTracNghiem.Models;

namespace ThiTracNghiem.GUI;

/// <summary>Form chính (MDI): menu thay đổi theo vai trò người dùng.</summary>
public partial class frmMain : FormCoSo
{
    private readonly TaiKhoanService _taiKhoanService = new();

    public frmMain()
    {
        InitializeComponent();
    }

    /// <summary>true nếu người dùng chọn Đăng xuất (Program sẽ hiện lại form đăng nhập).</summary>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool DaDangXuat { get; private set; }

    private void frmMain_Load(object sender, EventArgs e)
    {
        var nguoiDung = PhienDangNhap.NguoiDung!;

        lblNguoiDung.Text = $"Người dùng: {nguoiDung.HoTen} ({nguoiDung.TenDangNhap})";
        lblVaiTro.Text = "   |   Vai trò: " + nguoiDung.VaiTro.HienThi()
                         + (nguoiDung.Lop != null ? " - Lớp " + nguoiDung.Lop : string.Empty);

        // Phân quyền giao diện theo vai trò
        mnuQuanLy.Visible = nguoiDung.LaGiangVien;
        mnuThongKe.Visible = nguoiDung.LaGiangVien;
        mnuPhongThi.Visible = nguoiDung.LaSinhVien;

        if (nguoiDung.LaSinhVien)
            MoForm<frmPhongThi>();
        else
            MoForm<frmThongKe>();
    }

    /// <summary>Mở form con trong MDI; nếu form đó đang mở thì chỉ đưa lên trước.</summary>
    private void MoForm<T>() where T : Form, new()
    {
        var form = MdiChildren.OfType<T>().FirstOrDefault();
        if (form == null)
        {
            form = new T { MdiParent = this, WindowState = FormWindowState.Maximized };
            form.Show();
        }
        else
        {
            form.Activate();
        }
    }

    private void mnuMonHoc_Click(object sender, EventArgs e) => MoForm<frmMonHoc>();

    private void mnuCauHoi_Click(object sender, EventArgs e) => MoForm<frmCauHoi>();

    private void mnuDeThi_Click(object sender, EventArgs e) => MoForm<frmDeThi>();

    private void mnuTaiKhoan_Click(object sender, EventArgs e) => MoForm<frmTaiKhoan>();

    private void mnuThongKe_Click(object sender, EventArgs e) => MoForm<frmThongKe>();

    private void mnuPhongThi_Click(object sender, EventArgs e) => MoForm<frmPhongThi>();

    private void mnuDoiMatKhau_Click(object sender, EventArgs e)
    {
        using var form = new frmDoiMatKhau();
        form.ShowDialog(this);
    }

    private void mnuDangXuat_Click(object sender, EventArgs e)
    {
        if (!XacNhan("Bạn có chắc muốn đăng xuất?"))
            return;

        _taiKhoanService.DangXuat();
        DaDangXuat = true;
        Close();
    }

    private void mnuThoat_Click(object sender, EventArgs e) => Close();
}
