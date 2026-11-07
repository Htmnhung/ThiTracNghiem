using ThiTracNghiem.BLL;

namespace ThiTracNghiem.GUI;

public partial class frmDangNhap : FormCoSo
{
    private readonly TaiKhoanService _taiKhoanService = new();

    public frmDangNhap()
    {
        InitializeComponent();
    }

    private void frmDangNhap_Load(object sender, EventArgs e)
    {
        if (!_taiKhoanService.KiemTraKetNoi())
        {
            HienLoi("Không kết nối được tới cơ sở dữ liệu.\n\n" +
                    "Hãy kiểm tra:\n" +
                    "• SQL Server (hoặc LocalDB) đang chạy\n" +
                    "• Đã chạy file Database\\ThiTracNghiem.sql để tạo CSDL\n" +
                    "• Chuỗi kết nối trong file App.config");
        }
    }

    private void btnDangNhap_Click(object sender, EventArgs e)
    {
        bool thanhCong = ThucHien(() => _taiKhoanService.DangNhap(txtTenDangNhap.Text, txtMatKhau.Text));

        if (thanhCong)
        {
            DialogResult = DialogResult.OK;
            return;
        }

        txtMatKhau.SelectAll();
        txtMatKhau.Focus();
    }

    private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
    {
        txtMatKhau.UseSystemPasswordChar = !chkHienMatKhau.Checked;
    }
}
