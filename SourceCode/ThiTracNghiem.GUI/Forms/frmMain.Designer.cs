namespace ThiTracNghiem.GUI
{
    partial class frmMain
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.menuChinh = new System.Windows.Forms.MenuStrip();
            this.mnuHeThong = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDoiMatKhau = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNganCach1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDangXuat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThoat = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuQuanLy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMonHoc = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCauHoi = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDeThi = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNganCach2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuTaiKhoan = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuThongKe = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPhongThi = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCuaSo = new System.Windows.Forms.ToolStripMenuItem();
            this.thanhTrangThai = new System.Windows.Forms.StatusStrip();
            this.lblNguoiDung = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblVaiTro = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuChinh.SuspendLayout();
            this.thanhTrangThai.SuspendLayout();
            this.SuspendLayout();
            //
            // menuChinh
            //
            this.menuChinh.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHeThong,
            this.mnuQuanLy,
            this.mnuThongKe,
            this.mnuPhongThi,
            this.mnuCuaSo});
            this.menuChinh.Location = new System.Drawing.Point(0, 0);
            this.menuChinh.MdiWindowListItem = this.mnuCuaSo;
            this.menuChinh.Name = "menuChinh";
            this.menuChinh.Size = new System.Drawing.Size(1184, 24);
            this.menuChinh.TabIndex = 0;
            //
            // mnuHeThong
            //
            this.mnuHeThong.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuDoiMatKhau,
            this.mnuNganCach1,
            this.mnuDangXuat,
            this.mnuThoat});
            this.mnuHeThong.Name = "mnuHeThong";
            this.mnuHeThong.Size = new System.Drawing.Size(69, 20);
            this.mnuHeThong.Text = "&Hệ thống";
            //
            // mnuDoiMatKhau
            //
            this.mnuDoiMatKhau.Name = "mnuDoiMatKhau";
            this.mnuDoiMatKhau.Size = new System.Drawing.Size(180, 22);
            this.mnuDoiMatKhau.Text = "Đổi mật khẩu";
            this.mnuDoiMatKhau.Click += new System.EventHandler(this.mnuDoiMatKhau_Click);
            //
            // mnuNganCach1
            //
            this.mnuNganCach1.Name = "mnuNganCach1";
            this.mnuNganCach1.Size = new System.Drawing.Size(177, 6);
            //
            // mnuDangXuat
            //
            this.mnuDangXuat.Name = "mnuDangXuat";
            this.mnuDangXuat.Size = new System.Drawing.Size(180, 22);
            this.mnuDangXuat.Text = "Đăng xuất";
            this.mnuDangXuat.Click += new System.EventHandler(this.mnuDangXuat_Click);
            //
            // mnuThoat
            //
            this.mnuThoat.Name = "mnuThoat";
            this.mnuThoat.Size = new System.Drawing.Size(180, 22);
            this.mnuThoat.Text = "Thoát";
            this.mnuThoat.Click += new System.EventHandler(this.mnuThoat_Click);
            //
            // mnuQuanLy
            //
            this.mnuQuanLy.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMonHoc,
            this.mnuCauHoi,
            this.mnuDeThi,
            this.mnuNganCach2,
            this.mnuTaiKhoan});
            this.mnuQuanLy.Name = "mnuQuanLy";
            this.mnuQuanLy.Size = new System.Drawing.Size(60, 20);
            this.mnuQuanLy.Text = "&Quản lý";
            //
            // mnuMonHoc
            //
            this.mnuMonHoc.Name = "mnuMonHoc";
            this.mnuMonHoc.Size = new System.Drawing.Size(200, 22);
            this.mnuMonHoc.Text = "Môn học";
            this.mnuMonHoc.Click += new System.EventHandler(this.mnuMonHoc_Click);
            //
            // mnuCauHoi
            //
            this.mnuCauHoi.Name = "mnuCauHoi";
            this.mnuCauHoi.Size = new System.Drawing.Size(200, 22);
            this.mnuCauHoi.Text = "Ngân hàng câu hỏi";
            this.mnuCauHoi.Click += new System.EventHandler(this.mnuCauHoi_Click);
            //
            // mnuDeThi
            //
            this.mnuDeThi.Name = "mnuDeThi";
            this.mnuDeThi.Size = new System.Drawing.Size(200, 22);
            this.mnuDeThi.Text = "Đề thi";
            this.mnuDeThi.Click += new System.EventHandler(this.mnuDeThi_Click);
            //
            // mnuNganCach2
            //
            this.mnuNganCach2.Name = "mnuNganCach2";
            this.mnuNganCach2.Size = new System.Drawing.Size(197, 6);
            //
            // mnuTaiKhoan
            //
            this.mnuTaiKhoan.Name = "mnuTaiKhoan";
            this.mnuTaiKhoan.Size = new System.Drawing.Size(200, 22);
            this.mnuTaiKhoan.Text = "Tài khoản";
            this.mnuTaiKhoan.Click += new System.EventHandler(this.mnuTaiKhoan_Click);
            //
            // mnuThongKe
            //
            this.mnuThongKe.Name = "mnuThongKe";
            this.mnuThongKe.Size = new System.Drawing.Size(125, 20);
            this.mnuThongKe.Text = "&Kết quả - Thống kê";
            this.mnuThongKe.Click += new System.EventHandler(this.mnuThongKe_Click);
            //
            // mnuPhongThi
            //
            this.mnuPhongThi.Name = "mnuPhongThi";
            this.mnuPhongThi.Size = new System.Drawing.Size(73, 20);
            this.mnuPhongThi.Text = "&Phòng thi";
            this.mnuPhongThi.Click += new System.EventHandler(this.mnuPhongThi_Click);
            //
            // mnuCuaSo
            //
            this.mnuCuaSo.Name = "mnuCuaSo";
            this.mnuCuaSo.Size = new System.Drawing.Size(57, 20);
            this.mnuCuaSo.Text = "&Cửa sổ";
            //
            // thanhTrangThai
            //
            this.thanhTrangThai.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblNguoiDung,
            this.lblVaiTro});
            this.thanhTrangThai.Location = new System.Drawing.Point(0, 739);
            this.thanhTrangThai.Name = "thanhTrangThai";
            this.thanhTrangThai.Size = new System.Drawing.Size(1184, 22);
            this.thanhTrangThai.TabIndex = 1;
            //
            // lblNguoiDung
            //
            this.lblNguoiDung.Name = "lblNguoiDung";
            this.lblNguoiDung.Size = new System.Drawing.Size(70, 17);
            this.lblNguoiDung.Text = "Người dùng";
            //
            // lblVaiTro
            //
            this.lblVaiTro.Name = "lblVaiTro";
            this.lblVaiTro.Size = new System.Drawing.Size(42, 17);
            this.lblVaiTro.Text = "Vai trò";
            //
            // frmMain
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.thanhTrangThai);
            this.Controls.Add(this.menuChinh);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuChinh;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống thi trắc nghiệm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuChinh.ResumeLayout(false);
            this.menuChinh.PerformLayout();
            this.thanhTrangThai.ResumeLayout(false);
            this.thanhTrangThai.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.MenuStrip menuChinh;
        private System.Windows.Forms.ToolStripMenuItem mnuHeThong;
        private System.Windows.Forms.ToolStripMenuItem mnuDoiMatKhau;
        private System.Windows.Forms.ToolStripSeparator mnuNganCach1;
        private System.Windows.Forms.ToolStripMenuItem mnuDangXuat;
        private System.Windows.Forms.ToolStripMenuItem mnuThoat;
        private System.Windows.Forms.ToolStripMenuItem mnuQuanLy;
        private System.Windows.Forms.ToolStripMenuItem mnuMonHoc;
        private System.Windows.Forms.ToolStripMenuItem mnuCauHoi;
        private System.Windows.Forms.ToolStripMenuItem mnuDeThi;
        private System.Windows.Forms.ToolStripSeparator mnuNganCach2;
        private System.Windows.Forms.ToolStripMenuItem mnuTaiKhoan;
        private System.Windows.Forms.ToolStripMenuItem mnuThongKe;
        private System.Windows.Forms.ToolStripMenuItem mnuPhongThi;
        private System.Windows.Forms.ToolStripMenuItem mnuCuaSo;
        private System.Windows.Forms.StatusStrip thanhTrangThai;
        private System.Windows.Forms.ToolStripStatusLabel lblNguoiDung;
        private System.Windows.Forms.ToolStripStatusLabel lblVaiTro;
    }
}
