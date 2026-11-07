using ThiTracNghiem.BLL;

namespace ThiTracNghiem.GUI;

public partial class frmDoiMatKhau : FormCoSo
{
    private readonly TaiKhoanService _taiKhoanService = new();

    public frmDoiMatKhau()
    {
        InitializeComponent();
    }

    private void btnLuu_Click(object sender, EventArgs e)
    {
        if (ThucHien(() => _taiKhoanService.DoiMatKhau(txtMatKhauCu.Text, txtMatKhauMoi.Text, txtXacNhan.Text)))
        {
            HienThongBao("Đổi mật khẩu thành công.");
            DialogResult = DialogResult.OK;
        }
    }
}
