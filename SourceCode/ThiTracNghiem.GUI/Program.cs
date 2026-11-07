namespace ThiTracNghiem.GUI;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        // Bắt mọi lỗi chưa được xử lý để chương trình không dừng đột ngột
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (_, e) => FormCoSo.HienLoiKhongMongMuon(e.Exception);
        AppDomain.CurrentDomain.UnhandledException += (_, e) =>
        {
            if (e.ExceptionObject is Exception ex)
                FormCoSo.HienLoiKhongMongMuon(ex);
        };

        // Đăng nhập → form chính; chọn "Đăng xuất" thì quay lại form đăng nhập
        while (true)
        {
            using (var formDangNhap = new frmDangNhap())
            {
                if (formDangNhap.ShowDialog() != DialogResult.OK)
                    return;
            }

            using var formChinh = new frmMain();
            Application.Run(formChinh);

            if (!formChinh.DaDangXuat)
                return;
        }
    }
}
