namespace ThiTracNghiem.GUI
{
    partial class frmTaiKhoan
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
            this.lblVaiTroLoc = new System.Windows.Forms.Label();
            this.cboVaiTroLoc = new System.Windows.Forms.ComboBox();
            this.lblLopLoc = new System.Windows.Forms.Label();
            this.cboLopLoc = new System.Windows.Forms.ComboBox();
            this.lblTuKhoa = new System.Windows.Forms.Label();
            this.txtTuKhoa = new System.Windows.Forms.TextBox();
            this.btnTim = new System.Windows.Forms.Button();
            this.dgvTaiKhoan = new System.Windows.Forms.DataGridView();
            this.grpChiTiet = new System.Windows.Forms.GroupBox();
            this.lblTenDangNhap = new System.Windows.Forms.Label();
            this.txtTenDangNhap = new System.Windows.Forms.TextBox();
            this.lblVaiTro = new System.Windows.Forms.Label();
            this.cboVaiTro = new System.Windows.Forms.ComboBox();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.txtHoTen = new System.Windows.Forms.TextBox();
            this.lblLop = new System.Windows.Forms.Label();
            this.cboLop = new System.Windows.Forms.ComboBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblMatKhau = new System.Windows.Forms.Label();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.chkDangHoatDong = new System.Windows.Forms.CheckBox();
            this.lblCheDo = new System.Windows.Forms.Label();
            this.btnThemMoi = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnDatLaiMatKhau = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaiKhoan)).BeginInit();
            this.grpChiTiet.SuspendLayout();
            this.SuspendLayout();
            //
            // lblVaiTroLoc
            //
            this.lblVaiTroLoc.AutoSize = true;
            this.lblVaiTroLoc.Location = new System.Drawing.Point(12, 16);
            this.lblVaiTroLoc.Name = "lblVaiTroLoc";
            this.lblVaiTroLoc.Size = new System.Drawing.Size(42, 15);
            this.lblVaiTroLoc.TabIndex = 0;
            this.lblVaiTroLoc.Text = "Vai trò";
            //
            // cboVaiTroLoc
            //
            this.cboVaiTroLoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVaiTroLoc.Location = new System.Drawing.Point(60, 12);
            this.cboVaiTroLoc.Name = "cboVaiTroLoc";
            this.cboVaiTroLoc.Size = new System.Drawing.Size(120, 23);
            this.cboVaiTroLoc.TabIndex = 1;
            //
            // lblLopLoc
            //
            this.lblLopLoc.AutoSize = true;
            this.lblLopLoc.Location = new System.Drawing.Point(192, 16);
            this.lblLopLoc.Name = "lblLopLoc";
            this.lblLopLoc.Size = new System.Drawing.Size(27, 15);
            this.lblLopLoc.TabIndex = 2;
            this.lblLopLoc.Text = "Lớp";
            //
            // cboLopLoc
            //
            this.cboLopLoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLopLoc.Location = new System.Drawing.Point(225, 12);
            this.cboLopLoc.Name = "cboLopLoc";
            this.cboLopLoc.Size = new System.Drawing.Size(120, 23);
            this.cboLopLoc.TabIndex = 3;
            //
            // lblTuKhoa
            //
            this.lblTuKhoa.AutoSize = true;
            this.lblTuKhoa.Location = new System.Drawing.Point(357, 16);
            this.lblTuKhoa.Name = "lblTuKhoa";
            this.lblTuKhoa.Size = new System.Drawing.Size(48, 15);
            this.lblTuKhoa.TabIndex = 4;
            this.lblTuKhoa.Text = "Từ khóa";
            //
            // txtTuKhoa
            //
            this.txtTuKhoa.Location = new System.Drawing.Point(495, 12);
            this.txtTuKhoa.Name = "txtTuKhoa";
            this.txtTuKhoa.Size = new System.Drawing.Size(180, 23);
            this.txtTuKhoa.TabIndex = 5;
            //
            // btnTim
            //
            this.btnTim.Location = new System.Drawing.Point(685, 11);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(90, 25);
            this.btnTim.TabIndex = 6;
            this.btnTim.Text = "Tìm kiếm";
            this.btnTim.UseVisualStyleBackColor = true;
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
            //
            // dgvTaiKhoan
            //
            this.dgvTaiKhoan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTaiKhoan.Location = new System.Drawing.Point(12, 46);
            this.dgvTaiKhoan.Name = "dgvTaiKhoan";
            this.dgvTaiKhoan.Size = new System.Drawing.Size(660, 513);
            this.dgvTaiKhoan.TabIndex = 7;
            this.dgvTaiKhoan.SelectionChanged += new System.EventHandler(this.dgvTaiKhoan_SelectionChanged);
            //
            // grpChiTiet
            //
            this.grpChiTiet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpChiTiet.Controls.Add(this.lblTenDangNhap);
            this.grpChiTiet.Controls.Add(this.txtTenDangNhap);
            this.grpChiTiet.Controls.Add(this.lblVaiTro);
            this.grpChiTiet.Controls.Add(this.cboVaiTro);
            this.grpChiTiet.Controls.Add(this.lblHoTen);
            this.grpChiTiet.Controls.Add(this.txtHoTen);
            this.grpChiTiet.Controls.Add(this.lblLop);
            this.grpChiTiet.Controls.Add(this.cboLop);
            this.grpChiTiet.Controls.Add(this.lblEmail);
            this.grpChiTiet.Controls.Add(this.txtEmail);
            this.grpChiTiet.Controls.Add(this.lblMatKhau);
            this.grpChiTiet.Controls.Add(this.txtMatKhau);
            this.grpChiTiet.Controls.Add(this.chkDangHoatDong);
            this.grpChiTiet.Controls.Add(this.lblCheDo);
            this.grpChiTiet.Controls.Add(this.btnThemMoi);
            this.grpChiTiet.Controls.Add(this.btnLuu);
            this.grpChiTiet.Controls.Add(this.btnXoa);
            this.grpChiTiet.Controls.Add(this.btnLamMoi);
            this.grpChiTiet.Controls.Add(this.btnDatLaiMatKhau);
            this.grpChiTiet.Location = new System.Drawing.Point(685, 40);
            this.grpChiTiet.Name = "grpChiTiet";
            this.grpChiTiet.Size = new System.Drawing.Size(447, 360);
            this.grpChiTiet.TabIndex = 8;
            this.grpChiTiet.TabStop = false;
            this.grpChiTiet.Text = "Thông tin tài khoản";
            //
            // lblTenDangNhap
            //
            this.lblTenDangNhap.AutoSize = true;
            this.lblTenDangNhap.Location = new System.Drawing.Point(15, 26);
            this.lblTenDangNhap.Name = "lblTenDangNhap";
            this.lblTenDangNhap.Size = new System.Drawing.Size(140, 15);
            this.lblTenDangNhap.TabIndex = 0;
            this.lblTenDangNhap.Text = "Tên đăng nhập (MSSV) *";
            //
            // txtTenDangNhap
            //
            this.txtTenDangNhap.Location = new System.Drawing.Point(15, 44);
            this.txtTenDangNhap.MaxLength = 30;
            this.txtTenDangNhap.Name = "txtTenDangNhap";
            this.txtTenDangNhap.Size = new System.Drawing.Size(200, 23);
            this.txtTenDangNhap.TabIndex = 1;
            //
            // lblVaiTro
            //
            this.lblVaiTro.AutoSize = true;
            this.lblVaiTro.Location = new System.Drawing.Point(230, 26);
            this.lblVaiTro.Name = "lblVaiTro";
            this.lblVaiTro.Size = new System.Drawing.Size(52, 15);
            this.lblVaiTro.TabIndex = 2;
            this.lblVaiTro.Text = "Vai trò *";
            //
            // cboVaiTro
            //
            this.cboVaiTro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVaiTro.Location = new System.Drawing.Point(230, 44);
            this.cboVaiTro.Name = "cboVaiTro";
            this.cboVaiTro.Size = new System.Drawing.Size(200, 23);
            this.cboVaiTro.TabIndex = 2;
            //
            // lblHoTen
            //
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Location = new System.Drawing.Point(15, 78);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(54, 15);
            this.lblHoTen.TabIndex = 4;
            this.lblHoTen.Text = "Họ tên *";
            //
            // txtHoTen
            //
            this.txtHoTen.Location = new System.Drawing.Point(15, 96);
            this.txtHoTen.MaxLength = 100;
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new System.Drawing.Size(415, 23);
            this.txtHoTen.TabIndex = 3;
            //
            // lblLop
            //
            this.lblLop.AutoSize = true;
            this.lblLop.Location = new System.Drawing.Point(15, 130);
            this.lblLop.Name = "lblLop";
            this.lblLop.Size = new System.Drawing.Size(155, 15);
            this.lblLop.TabIndex = 6;
            this.lblLop.Text = "Lớp (bắt buộc với sinh viên)";
            //
            // cboLop
            //
            this.cboLop.Location = new System.Drawing.Point(15, 148);
            this.cboLop.MaxLength = 20;
            this.cboLop.Name = "cboLop";
            this.cboLop.Size = new System.Drawing.Size(200, 23);
            this.cboLop.TabIndex = 4;
            //
            // lblEmail
            //
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(230, 130);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(46, 15);
            this.lblEmail.TabIndex = 8;
            this.lblEmail.Text = "Email *";
            //
            // txtEmail
            //
            this.txtEmail.Location = new System.Drawing.Point(230, 148);
            this.txtEmail.MaxLength = 100;
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(200, 23);
            this.txtEmail.TabIndex = 5;
            //
            // lblMatKhau
            //
            this.lblMatKhau.AutoSize = true;
            this.lblMatKhau.Location = new System.Drawing.Point(15, 182);
            this.lblMatKhau.Name = "lblMatKhau";
            this.lblMatKhau.Size = new System.Drawing.Size(199, 15);
            this.lblMatKhau.TabIndex = 10;
            this.lblMatKhau.Text = "Mật khẩu (khi thêm mới / đặt lại)";
            //
            // txtMatKhau
            //
            this.txtMatKhau.Location = new System.Drawing.Point(15, 200);
            this.txtMatKhau.MaxLength = 50;
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.Size = new System.Drawing.Size(200, 23);
            this.txtMatKhau.TabIndex = 6;
            this.txtMatKhau.UseSystemPasswordChar = true;
            //
            // chkDangHoatDong
            //
            this.chkDangHoatDong.AutoSize = true;
            this.chkDangHoatDong.Checked = true;
            this.chkDangHoatDong.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDangHoatDong.Location = new System.Drawing.Point(230, 202);
            this.chkDangHoatDong.Name = "chkDangHoatDong";
            this.chkDangHoatDong.Size = new System.Drawing.Size(203, 19);
            this.chkDangHoatDong.TabIndex = 7;
            this.chkDangHoatDong.Text = "Hoạt động (bỏ chọn = khóa)";
            this.chkDangHoatDong.UseVisualStyleBackColor = true;
            //
            // lblCheDo
            //
            this.lblCheDo.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblCheDo.Location = new System.Drawing.Point(15, 236);
            this.lblCheDo.Name = "lblCheDo";
            this.lblCheDo.Size = new System.Drawing.Size(415, 20);
            this.lblCheDo.TabIndex = 13;
            this.lblCheDo.Text = "Chế độ";
            //
            // btnThemMoi
            //
            this.btnThemMoi.Location = new System.Drawing.Point(15, 268);
            this.btnThemMoi.Name = "btnThemMoi";
            this.btnThemMoi.Size = new System.Drawing.Size(98, 32);
            this.btnThemMoi.TabIndex = 8;
            this.btnThemMoi.Text = "Thêm mới";
            this.btnThemMoi.UseVisualStyleBackColor = true;
            this.btnThemMoi.Click += new System.EventHandler(this.btnThemMoi_Click);
            //
            // btnLuu
            //
            this.btnLuu.Location = new System.Drawing.Point(119, 268);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(98, 32);
            this.btnLuu.TabIndex = 9;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            //
            // btnXoa
            //
            this.btnXoa.Location = new System.Drawing.Point(223, 268);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(98, 32);
            this.btnXoa.TabIndex = 10;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            //
            // btnLamMoi
            //
            this.btnLamMoi.Location = new System.Drawing.Point(327, 268);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(103, 32);
            this.btnLamMoi.TabIndex = 11;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            //
            // btnDatLaiMatKhau
            //
            this.btnDatLaiMatKhau.Location = new System.Drawing.Point(15, 308);
            this.btnDatLaiMatKhau.Name = "btnDatLaiMatKhau";
            this.btnDatLaiMatKhau.Size = new System.Drawing.Size(202, 32);
            this.btnDatLaiMatKhau.TabIndex = 12;
            this.btnDatLaiMatKhau.Text = "Đặt lại mật khẩu";
            this.btnDatLaiMatKhau.UseVisualStyleBackColor = true;
            this.btnDatLaiMatKhau.Click += new System.EventHandler(this.btnDatLaiMatKhau_Click);
            //
            // frmTaiKhoan
            //
            this.AcceptButton = this.btnTim;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 571);
            this.Controls.Add(this.grpChiTiet);
            this.Controls.Add(this.dgvTaiKhoan);
            this.Controls.Add(this.btnTim);
            this.Controls.Add(this.txtTuKhoa);
            this.Controls.Add(this.lblTuKhoa);
            this.Controls.Add(this.cboLopLoc);
            this.Controls.Add(this.lblLopLoc);
            this.Controls.Add(this.cboVaiTroLoc);
            this.Controls.Add(this.lblVaiTroLoc);
            this.Name = "frmTaiKhoan";
            this.Text = "Quản lý tài khoản";
            this.Load += new System.EventHandler(this.frmTaiKhoan_Load);
            this.Shown += new System.EventHandler(this.frmTaiKhoan_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaiKhoan)).EndInit();
            this.grpChiTiet.ResumeLayout(false);
            this.grpChiTiet.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblVaiTroLoc;
        private System.Windows.Forms.ComboBox cboVaiTroLoc;
        private System.Windows.Forms.Label lblLopLoc;
        private System.Windows.Forms.ComboBox cboLopLoc;
        private System.Windows.Forms.Label lblTuKhoa;
        private System.Windows.Forms.TextBox txtTuKhoa;
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.DataGridView dgvTaiKhoan;
        private System.Windows.Forms.GroupBox grpChiTiet;
        private System.Windows.Forms.Label lblTenDangNhap;
        private System.Windows.Forms.TextBox txtTenDangNhap;
        private System.Windows.Forms.Label lblVaiTro;
        private System.Windows.Forms.ComboBox cboVaiTro;
        private System.Windows.Forms.Label lblHoTen;
        private System.Windows.Forms.TextBox txtHoTen;
        private System.Windows.Forms.Label lblLop;
        private System.Windows.Forms.ComboBox cboLop;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblMatKhau;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.CheckBox chkDangHoatDong;
        private System.Windows.Forms.Label lblCheDo;
        private System.Windows.Forms.Button btnThemMoi;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnDatLaiMatKhau;
    }
}
