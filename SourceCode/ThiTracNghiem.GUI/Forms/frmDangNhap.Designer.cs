namespace ThiTracNghiem.GUI
{
    partial class frmDangNhap
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
            this.lblTieuDe = new System.Windows.Forms.Label();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.lblTenDangNhap = new System.Windows.Forms.Label();
            this.txtTenDangNhap = new System.Windows.Forms.TextBox();
            this.lblMatKhau = new System.Windows.Forms.Label();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.chkHienMatKhau = new System.Windows.Forms.CheckBox();
            this.btnDangNhap = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            this.lblGoiY = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // lblTieuDe
            //
            this.lblTieuDe.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.ForeColor = System.Drawing.Color.FromArgb(31, 78, 121);
            this.lblTieuDe.Location = new System.Drawing.Point(0, 18);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(434, 34);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "ĐĂNG NHẬP HỆ THỐNG";
            this.lblTieuDe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblMoTa
            //
            this.lblMoTa.ForeColor = System.Drawing.Color.DimGray;
            this.lblMoTa.Location = new System.Drawing.Point(0, 52);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(434, 20);
            this.lblMoTa.TabIndex = 1;
            this.lblMoTa.Text = "Hệ thống thi trắc nghiệm trên máy tính";
            this.lblMoTa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblTenDangNhap
            //
            this.lblTenDangNhap.AutoSize = true;
            this.lblTenDangNhap.Location = new System.Drawing.Point(40, 98);
            this.lblTenDangNhap.Name = "lblTenDangNhap";
            this.lblTenDangNhap.Size = new System.Drawing.Size(86, 15);
            this.lblTenDangNhap.TabIndex = 2;
            this.lblTenDangNhap.Text = "Tên đăng nhập";
            //
            // txtTenDangNhap
            //
            this.txtTenDangNhap.Location = new System.Drawing.Point(150, 95);
            this.txtTenDangNhap.MaxLength = 30;
            this.txtTenDangNhap.Name = "txtTenDangNhap";
            this.txtTenDangNhap.Size = new System.Drawing.Size(240, 23);
            this.txtTenDangNhap.TabIndex = 3;
            //
            // lblMatKhau
            //
            this.lblMatKhau.AutoSize = true;
            this.lblMatKhau.Location = new System.Drawing.Point(40, 138);
            this.lblMatKhau.Name = "lblMatKhau";
            this.lblMatKhau.Size = new System.Drawing.Size(57, 15);
            this.lblMatKhau.TabIndex = 4;
            this.lblMatKhau.Text = "Mật khẩu";
            //
            // txtMatKhau
            //
            this.txtMatKhau.Location = new System.Drawing.Point(150, 135);
            this.txtMatKhau.MaxLength = 50;
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.Size = new System.Drawing.Size(240, 23);
            this.txtMatKhau.TabIndex = 5;
            this.txtMatKhau.UseSystemPasswordChar = true;
            //
            // chkHienMatKhau
            //
            this.chkHienMatKhau.AutoSize = true;
            this.chkHienMatKhau.Location = new System.Drawing.Point(150, 166);
            this.chkHienMatKhau.Name = "chkHienMatKhau";
            this.chkHienMatKhau.Size = new System.Drawing.Size(102, 19);
            this.chkHienMatKhau.TabIndex = 6;
            this.chkHienMatKhau.Text = "Hiện mật khẩu";
            this.chkHienMatKhau.UseVisualStyleBackColor = true;
            this.chkHienMatKhau.CheckedChanged += new System.EventHandler(this.chkHienMatKhau_CheckedChanged);
            //
            // btnDangNhap
            //
            this.btnDangNhap.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnDangNhap.Location = new System.Drawing.Point(150, 200);
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.Size = new System.Drawing.Size(115, 34);
            this.btnDangNhap.TabIndex = 7;
            this.btnDangNhap.Text = "Đăng nhập";
            this.btnDangNhap.UseVisualStyleBackColor = true;
            this.btnDangNhap.Click += new System.EventHandler(this.btnDangNhap_Click);
            //
            // btnThoat
            //
            this.btnThoat.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnThoat.Location = new System.Drawing.Point(275, 200);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(115, 34);
            this.btnThoat.TabIndex = 8;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            //
            // lblGoiY
            //
            this.lblGoiY.ForeColor = System.Drawing.Color.Gray;
            this.lblGoiY.Location = new System.Drawing.Point(0, 250);
            this.lblGoiY.Name = "lblGoiY";
            this.lblGoiY.Size = new System.Drawing.Size(434, 38);
            this.lblGoiY.TabIndex = 9;
            this.lblGoiY.Text = "Tài khoản demo: gv01 (giảng viên), 2380001 (sinh viên)\r\nMật khẩu: 123456";
            this.lblGoiY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // frmDangNhap
            //
            this.AcceptButton = this.btnDangNhap;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnThoat;
            this.ClientSize = new System.Drawing.Size(434, 298);
            this.Controls.Add(this.lblGoiY);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnDangNhap);
            this.Controls.Add(this.chkHienMatKhau);
            this.Controls.Add(this.txtMatKhau);
            this.Controls.Add(this.lblMatKhau);
            this.Controls.Add(this.txtTenDangNhap);
            this.Controls.Add(this.lblTenDangNhap);
            this.Controls.Add(this.lblMoTa);
            this.Controls.Add(this.lblTieuDe);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDangNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập - Thi trắc nghiệm";
            this.Load += new System.EventHandler(this.frmDangNhap_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTieuDe;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.Label lblTenDangNhap;
        private System.Windows.Forms.TextBox txtTenDangNhap;
        private System.Windows.Forms.Label lblMatKhau;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.CheckBox chkHienMatKhau;
        private System.Windows.Forms.Button btnDangNhap;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Label lblGoiY;
    }
}
