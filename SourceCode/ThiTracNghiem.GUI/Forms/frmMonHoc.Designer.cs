namespace ThiTracNghiem.GUI
{
    partial class frmMonHoc
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
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTim = new System.Windows.Forms.Button();
            this.dgvMonHoc = new System.Windows.Forms.DataGridView();
            this.grpThongTin = new System.Windows.Forms.GroupBox();
            this.lblTenMH = new System.Windows.Forms.Label();
            this.txtTenMH = new System.Windows.Forms.TextBox();
            this.lblSoTinChi = new System.Windows.Forms.Label();
            this.nudSoTinChi = new System.Windows.Forms.NumericUpDown();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.txtMoTa = new System.Windows.Forms.TextBox();
            this.lblCheDo = new System.Windows.Forms.Label();
            this.btnThemMoi = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonHoc)).BeginInit();
            this.grpThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoTinChi)).BeginInit();
            this.SuspendLayout();
            //
            // lblTimKiem
            //
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Location = new System.Drawing.Point(12, 16);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(76, 15);
            this.lblTimKiem.TabIndex = 0;
            this.lblTimKiem.Text = "Tên môn học";
            //
            // txtTimKiem
            //
            this.txtTimKiem.Location = new System.Drawing.Point(100, 12);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(260, 23);
            this.txtTimKiem.TabIndex = 1;
            //
            // btnTim
            //
            this.btnTim.Location = new System.Drawing.Point(370, 11);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(90, 25);
            this.btnTim.TabIndex = 2;
            this.btnTim.Text = "Tìm kiếm";
            this.btnTim.UseVisualStyleBackColor = true;
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
            //
            // dgvMonHoc
            //
            this.dgvMonHoc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvMonHoc.Location = new System.Drawing.Point(12, 46);
            this.dgvMonHoc.Name = "dgvMonHoc";
            this.dgvMonHoc.Size = new System.Drawing.Size(560, 463);
            this.dgvMonHoc.TabIndex = 3;
            this.dgvMonHoc.SelectionChanged += new System.EventHandler(this.dgvMonHoc_SelectionChanged);
            //
            // grpThongTin
            //
            this.grpThongTin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpThongTin.Controls.Add(this.lblTenMH);
            this.grpThongTin.Controls.Add(this.txtTenMH);
            this.grpThongTin.Controls.Add(this.lblSoTinChi);
            this.grpThongTin.Controls.Add(this.nudSoTinChi);
            this.grpThongTin.Controls.Add(this.lblMoTa);
            this.grpThongTin.Controls.Add(this.txtMoTa);
            this.grpThongTin.Controls.Add(this.lblCheDo);
            this.grpThongTin.Controls.Add(this.btnThemMoi);
            this.grpThongTin.Controls.Add(this.btnLuu);
            this.grpThongTin.Controls.Add(this.btnXoa);
            this.grpThongTin.Controls.Add(this.btnLamMoi);
            this.grpThongTin.Location = new System.Drawing.Point(585, 40);
            this.grpThongTin.Name = "grpThongTin";
            this.grpThongTin.Size = new System.Drawing.Size(387, 330);
            this.grpThongTin.TabIndex = 4;
            this.grpThongTin.TabStop = false;
            this.grpThongTin.Text = "Thông tin môn học";
            //
            // lblTenMH
            //
            this.lblTenMH.AutoSize = true;
            this.lblTenMH.Location = new System.Drawing.Point(15, 28);
            this.lblTenMH.Name = "lblTenMH";
            this.lblTenMH.Size = new System.Drawing.Size(85, 15);
            this.lblTenMH.TabIndex = 0;
            this.lblTenMH.Text = "Tên môn học *";
            //
            // txtTenMH
            //
            this.txtTenMH.Location = new System.Drawing.Point(15, 46);
            this.txtTenMH.MaxLength = 100;
            this.txtTenMH.Name = "txtTenMH";
            this.txtTenMH.Size = new System.Drawing.Size(355, 23);
            this.txtTenMH.TabIndex = 1;
            //
            // lblSoTinChi
            //
            this.lblSoTinChi.AutoSize = true;
            this.lblSoTinChi.Location = new System.Drawing.Point(15, 80);
            this.lblSoTinChi.Name = "lblSoTinChi";
            this.lblSoTinChi.Size = new System.Drawing.Size(69, 15);
            this.lblSoTinChi.TabIndex = 2;
            this.lblSoTinChi.Text = "Số tín chỉ *";
            //
            // nudSoTinChi
            //
            this.nudSoTinChi.Location = new System.Drawing.Point(15, 98);
            this.nudSoTinChi.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            this.nudSoTinChi.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudSoTinChi.Name = "nudSoTinChi";
            this.nudSoTinChi.Size = new System.Drawing.Size(80, 23);
            this.nudSoTinChi.TabIndex = 3;
            this.nudSoTinChi.Value = new decimal(new int[] { 3, 0, 0, 0 });
            //
            // lblMoTa
            //
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Location = new System.Drawing.Point(15, 132);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(38, 15);
            this.lblMoTa.TabIndex = 4;
            this.lblMoTa.Text = "Mô tả";
            //
            // txtMoTa
            //
            this.txtMoTa.Location = new System.Drawing.Point(15, 150);
            this.txtMoTa.MaxLength = 500;
            this.txtMoTa.Multiline = true;
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMoTa.Size = new System.Drawing.Size(355, 90);
            this.txtMoTa.TabIndex = 5;
            //
            // lblCheDo
            //
            this.lblCheDo.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblCheDo.Location = new System.Drawing.Point(15, 248);
            this.lblCheDo.Name = "lblCheDo";
            this.lblCheDo.Size = new System.Drawing.Size(355, 20);
            this.lblCheDo.TabIndex = 6;
            this.lblCheDo.Text = "Chế độ";
            //
            // btnThemMoi
            //
            this.btnThemMoi.Location = new System.Drawing.Point(15, 280);
            this.btnThemMoi.Name = "btnThemMoi";
            this.btnThemMoi.Size = new System.Drawing.Size(84, 32);
            this.btnThemMoi.TabIndex = 7;
            this.btnThemMoi.Text = "Thêm mới";
            this.btnThemMoi.UseVisualStyleBackColor = true;
            this.btnThemMoi.Click += new System.EventHandler(this.btnThemMoi_Click);
            //
            // btnLuu
            //
            this.btnLuu.Location = new System.Drawing.Point(105, 280);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(84, 32);
            this.btnLuu.TabIndex = 8;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            //
            // btnXoa
            //
            this.btnXoa.Location = new System.Drawing.Point(195, 280);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(84, 32);
            this.btnXoa.TabIndex = 9;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            //
            // btnLamMoi
            //
            this.btnLamMoi.Location = new System.Drawing.Point(285, 280);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(84, 32);
            this.btnLamMoi.TabIndex = 10;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            //
            // frmMonHoc
            //
            this.AcceptButton = this.btnTim;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 521);
            this.Controls.Add(this.grpThongTin);
            this.Controls.Add(this.dgvMonHoc);
            this.Controls.Add(this.btnTim);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.lblTimKiem);
            this.Name = "frmMonHoc";
            this.Text = "Quản lý môn học";
            this.Load += new System.EventHandler(this.frmMonHoc_Load);
            this.Shown += new System.EventHandler(this.frmMonHoc_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonHoc)).EndInit();
            this.grpThongTin.ResumeLayout(false);
            this.grpThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoTinChi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.DataGridView dgvMonHoc;
        private System.Windows.Forms.GroupBox grpThongTin;
        private System.Windows.Forms.Label lblTenMH;
        private System.Windows.Forms.TextBox txtTenMH;
        private System.Windows.Forms.Label lblSoTinChi;
        private System.Windows.Forms.NumericUpDown nudSoTinChi;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.TextBox txtMoTa;
        private System.Windows.Forms.Label lblCheDo;
        private System.Windows.Forms.Button btnThemMoi;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
    }
}
