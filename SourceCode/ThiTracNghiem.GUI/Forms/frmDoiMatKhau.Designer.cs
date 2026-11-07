namespace ThiTracNghiem.GUI
{
    partial class frmDoiMatKhau
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
            this.lblMatKhauCu = new System.Windows.Forms.Label();
            this.txtMatKhauCu = new System.Windows.Forms.TextBox();
            this.lblMatKhauMoi = new System.Windows.Forms.Label();
            this.txtMatKhauMoi = new System.Windows.Forms.TextBox();
            this.lblXacNhan = new System.Windows.Forms.Label();
            this.txtXacNhan = new System.Windows.Forms.TextBox();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnHuy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // lblMatKhauCu
            //
            this.lblMatKhauCu.AutoSize = true;
            this.lblMatKhauCu.Location = new System.Drawing.Point(24, 26);
            this.lblMatKhauCu.Name = "lblMatKhauCu";
            this.lblMatKhauCu.Size = new System.Drawing.Size(105, 15);
            this.lblMatKhauCu.TabIndex = 0;
            this.lblMatKhauCu.Text = "Mật khẩu hiện tại *";
            //
            // txtMatKhauCu
            //
            this.txtMatKhauCu.Location = new System.Drawing.Point(170, 23);
            this.txtMatKhauCu.MaxLength = 50;
            this.txtMatKhauCu.Name = "txtMatKhauCu";
            this.txtMatKhauCu.Size = new System.Drawing.Size(210, 23);
            this.txtMatKhauCu.TabIndex = 1;
            this.txtMatKhauCu.UseSystemPasswordChar = true;
            //
            // lblMatKhauMoi
            //
            this.lblMatKhauMoi.AutoSize = true;
            this.lblMatKhauMoi.Location = new System.Drawing.Point(24, 64);
            this.lblMatKhauMoi.Name = "lblMatKhauMoi";
            this.lblMatKhauMoi.Size = new System.Drawing.Size(92, 15);
            this.lblMatKhauMoi.TabIndex = 2;
            this.lblMatKhauMoi.Text = "Mật khẩu mới *";
            //
            // txtMatKhauMoi
            //
            this.txtMatKhauMoi.Location = new System.Drawing.Point(170, 61);
            this.txtMatKhauMoi.MaxLength = 50;
            this.txtMatKhauMoi.Name = "txtMatKhauMoi";
            this.txtMatKhauMoi.Size = new System.Drawing.Size(210, 23);
            this.txtMatKhauMoi.TabIndex = 3;
            this.txtMatKhauMoi.UseSystemPasswordChar = true;
            //
            // lblXacNhan
            //
            this.lblXacNhan.AutoSize = true;
            this.lblXacNhan.Location = new System.Drawing.Point(24, 102);
            this.lblXacNhan.Name = "lblXacNhan";
            this.lblXacNhan.Size = new System.Drawing.Size(137, 15);
            this.lblXacNhan.TabIndex = 4;
            this.lblXacNhan.Text = "Nhập lại mật khẩu mới *";
            //
            // txtXacNhan
            //
            this.txtXacNhan.Location = new System.Drawing.Point(170, 99);
            this.txtXacNhan.MaxLength = 50;
            this.txtXacNhan.Name = "txtXacNhan";
            this.txtXacNhan.Size = new System.Drawing.Size(210, 23);
            this.txtXacNhan.TabIndex = 5;
            this.txtXacNhan.UseSystemPasswordChar = true;
            //
            // lblGhiChu
            //
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.ForeColor = System.Drawing.Color.Gray;
            this.lblGhiChu.Location = new System.Drawing.Point(170, 128);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(200, 15);
            this.lblGhiChu.TabIndex = 6;
            this.lblGhiChu.Text = "6-50 ký tự, không chứa khoảng trắng";
            //
            // btnLuu
            //
            this.btnLuu.Location = new System.Drawing.Point(170, 160);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(100, 32);
            this.btnLuu.TabIndex = 7;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            //
            // btnHuy
            //
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Location = new System.Drawing.Point(280, 160);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(100, 32);
            this.btnHuy.TabIndex = 8;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = true;
            //
            // frmDoiMatKhau
            //
            this.AcceptButton = this.btnLuu;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnHuy;
            this.ClientSize = new System.Drawing.Size(404, 211);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.lblGhiChu);
            this.Controls.Add(this.txtXacNhan);
            this.Controls.Add(this.lblXacNhan);
            this.Controls.Add(this.txtMatKhauMoi);
            this.Controls.Add(this.lblMatKhauMoi);
            this.Controls.Add(this.txtMatKhauCu);
            this.Controls.Add(this.lblMatKhauCu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDoiMatKhau";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Đổi mật khẩu";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblMatKhauCu;
        private System.Windows.Forms.TextBox txtMatKhauCu;
        private System.Windows.Forms.Label lblMatKhauMoi;
        private System.Windows.Forms.TextBox txtMatKhauMoi;
        private System.Windows.Forms.Label lblXacNhan;
        private System.Windows.Forms.TextBox txtXacNhan;
        private System.Windows.Forms.Label lblGhiChu;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnHuy;
    }
}
