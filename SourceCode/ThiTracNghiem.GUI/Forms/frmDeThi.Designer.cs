namespace ThiTracNghiem.GUI
{
    partial class frmDeThi
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
            this.lblMonHocLoc = new System.Windows.Forms.Label();
            this.cboMonHocLoc = new System.Windows.Forms.ComboBox();
            this.lblTrangThaiLoc = new System.Windows.Forms.Label();
            this.cboTrangThaiLoc = new System.Windows.Forms.ComboBox();
            this.lblTuKhoa = new System.Windows.Forms.Label();
            this.txtTuKhoa = new System.Windows.Forms.TextBox();
            this.btnTim = new System.Windows.Forms.Button();
            this.dgvDeThi = new System.Windows.Forms.DataGridView();
            this.grpChiTiet = new System.Windows.Forms.GroupBox();
            this.lblTenDe = new System.Windows.Forms.Label();
            this.txtTenDe = new System.Windows.Forms.TextBox();
            this.lblMonHoc = new System.Windows.Forms.Label();
            this.cboMonHoc = new System.Windows.Forms.ComboBox();
            this.lblThoiGianLam = new System.Windows.Forms.Label();
            this.nudThoiGianLam = new System.Windows.Forms.NumericUpDown();
            this.lblNganHang = new System.Windows.Forms.Label();
            this.lblSoCauDe = new System.Windows.Forms.Label();
            this.nudSoCauDe = new System.Windows.Forms.NumericUpDown();
            this.lblSoCauTB = new System.Windows.Forms.Label();
            this.nudSoCauTB = new System.Windows.Forms.NumericUpDown();
            this.lblSoCauKho = new System.Windows.Forms.Label();
            this.nudSoCauKho = new System.Windows.Forms.NumericUpDown();
            this.lblTongSoCau = new System.Windows.Forms.Label();
            this.lblThoiGianMo = new System.Windows.Forms.Label();
            this.dtpThoiGianMo = new System.Windows.Forms.DateTimePicker();
            this.lblThoiGianDong = new System.Windows.Forms.Label();
            this.dtpThoiGianDong = new System.Windows.Forms.DateTimePicker();
            this.lblSoLanThi = new System.Windows.Forms.Label();
            this.nudSoLanThi = new System.Windows.Forms.NumericUpDown();
            this.lblCanhBao = new System.Windows.Forms.Label();
            this.lblCheDo = new System.Windows.Forms.Label();
            this.btnThemMoi = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeThi)).BeginInit();
            this.grpChiTiet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThoiGianLam)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoCauDe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoCauTB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoCauKho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLanThi)).BeginInit();
            this.SuspendLayout();
            //
            // lblMonHocLoc
            //
            this.lblMonHocLoc.AutoSize = true;
            this.lblMonHocLoc.Location = new System.Drawing.Point(12, 16);
            this.lblMonHocLoc.Name = "lblMonHocLoc";
            this.lblMonHocLoc.Size = new System.Drawing.Size(32, 15);
            this.lblMonHocLoc.TabIndex = 0;
            this.lblMonHocLoc.Text = "Môn";
            //
            // cboMonHocLoc
            //
            this.cboMonHocLoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMonHocLoc.Location = new System.Drawing.Point(50, 12);
            this.cboMonHocLoc.Name = "cboMonHocLoc";
            this.cboMonHocLoc.Size = new System.Drawing.Size(220, 23);
            this.cboMonHocLoc.TabIndex = 1;
            //
            // lblTrangThaiLoc
            //
            this.lblTrangThaiLoc.AutoSize = true;
            this.lblTrangThaiLoc.Location = new System.Drawing.Point(282, 16);
            this.lblTrangThaiLoc.Name = "lblTrangThaiLoc";
            this.lblTrangThaiLoc.Size = new System.Drawing.Size(60, 15);
            this.lblTrangThaiLoc.TabIndex = 2;
            this.lblTrangThaiLoc.Text = "Trạng thái";
            //
            // cboTrangThaiLoc
            //
            this.cboTrangThaiLoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThaiLoc.Location = new System.Drawing.Point(348, 12);
            this.cboTrangThaiLoc.Name = "cboTrangThaiLoc";
            this.cboTrangThaiLoc.Size = new System.Drawing.Size(110, 23);
            this.cboTrangThaiLoc.TabIndex = 3;
            //
            // lblTuKhoa
            //
            this.lblTuKhoa.AutoSize = true;
            this.lblTuKhoa.Location = new System.Drawing.Point(470, 16);
            this.lblTuKhoa.Name = "lblTuKhoa";
            this.lblTuKhoa.Size = new System.Drawing.Size(48, 15);
            this.lblTuKhoa.TabIndex = 4;
            this.lblTuKhoa.Text = "Tên đề";
            //
            // txtTuKhoa
            //
            this.txtTuKhoa.Location = new System.Drawing.Point(522, 12);
            this.txtTuKhoa.Name = "txtTuKhoa";
            this.txtTuKhoa.Size = new System.Drawing.Size(200, 23);
            this.txtTuKhoa.TabIndex = 5;
            //
            // btnTim
            //
            this.btnTim.Location = new System.Drawing.Point(732, 11);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(90, 25);
            this.btnTim.TabIndex = 6;
            this.btnTim.Text = "Tìm kiếm";
            this.btnTim.UseVisualStyleBackColor = true;
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
            //
            // dgvDeThi
            //
            this.dgvDeThi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDeThi.Location = new System.Drawing.Point(12, 46);
            this.dgvDeThi.Name = "dgvDeThi";
            this.dgvDeThi.Size = new System.Drawing.Size(660, 543);
            this.dgvDeThi.TabIndex = 7;
            this.dgvDeThi.SelectionChanged += new System.EventHandler(this.dgvDeThi_SelectionChanged);
            //
            // grpChiTiet
            //
            this.grpChiTiet.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpChiTiet.Controls.Add(this.lblTenDe);
            this.grpChiTiet.Controls.Add(this.txtTenDe);
            this.grpChiTiet.Controls.Add(this.lblMonHoc);
            this.grpChiTiet.Controls.Add(this.cboMonHoc);
            this.grpChiTiet.Controls.Add(this.lblThoiGianLam);
            this.grpChiTiet.Controls.Add(this.nudThoiGianLam);
            this.grpChiTiet.Controls.Add(this.lblNganHang);
            this.grpChiTiet.Controls.Add(this.lblSoCauDe);
            this.grpChiTiet.Controls.Add(this.nudSoCauDe);
            this.grpChiTiet.Controls.Add(this.lblSoCauTB);
            this.grpChiTiet.Controls.Add(this.nudSoCauTB);
            this.grpChiTiet.Controls.Add(this.lblSoCauKho);
            this.grpChiTiet.Controls.Add(this.nudSoCauKho);
            this.grpChiTiet.Controls.Add(this.lblTongSoCau);
            this.grpChiTiet.Controls.Add(this.lblThoiGianMo);
            this.grpChiTiet.Controls.Add(this.dtpThoiGianMo);
            this.grpChiTiet.Controls.Add(this.lblThoiGianDong);
            this.grpChiTiet.Controls.Add(this.dtpThoiGianDong);
            this.grpChiTiet.Controls.Add(this.lblSoLanThi);
            this.grpChiTiet.Controls.Add(this.nudSoLanThi);
            this.grpChiTiet.Controls.Add(this.lblCanhBao);
            this.grpChiTiet.Controls.Add(this.lblCheDo);
            this.grpChiTiet.Controls.Add(this.btnThemMoi);
            this.grpChiTiet.Controls.Add(this.btnLuu);
            this.grpChiTiet.Controls.Add(this.btnXoa);
            this.grpChiTiet.Controls.Add(this.btnLamMoi);
            this.grpChiTiet.Location = new System.Drawing.Point(685, 40);
            this.grpChiTiet.Name = "grpChiTiet";
            this.grpChiTiet.Size = new System.Drawing.Size(467, 549);
            this.grpChiTiet.TabIndex = 8;
            this.grpChiTiet.TabStop = false;
            this.grpChiTiet.Text = "Thông tin đề thi";
            //
            // lblTenDe
            //
            this.lblTenDe.AutoSize = true;
            this.lblTenDe.Location = new System.Drawing.Point(15, 26);
            this.lblTenDe.Name = "lblTenDe";
            this.lblTenDe.Size = new System.Drawing.Size(73, 15);
            this.lblTenDe.TabIndex = 0;
            this.lblTenDe.Text = "Tên đề thi *";
            //
            // txtTenDe
            //
            this.txtTenDe.Location = new System.Drawing.Point(15, 44);
            this.txtTenDe.MaxLength = 200;
            this.txtTenDe.Name = "txtTenDe";
            this.txtTenDe.Size = new System.Drawing.Size(435, 23);
            this.txtTenDe.TabIndex = 1;
            //
            // lblMonHoc
            //
            this.lblMonHoc.AutoSize = true;
            this.lblMonHoc.Location = new System.Drawing.Point(15, 78);
            this.lblMonHoc.Name = "lblMonHoc";
            this.lblMonHoc.Size = new System.Drawing.Size(63, 15);
            this.lblMonHoc.TabIndex = 2;
            this.lblMonHoc.Text = "Môn học *";
            //
            // cboMonHoc
            //
            this.cboMonHoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMonHoc.Location = new System.Drawing.Point(15, 96);
            this.cboMonHoc.Name = "cboMonHoc";
            this.cboMonHoc.Size = new System.Drawing.Size(270, 23);
            this.cboMonHoc.TabIndex = 2;
            this.cboMonHoc.SelectedIndexChanged += new System.EventHandler(this.cboMonHoc_SelectedIndexChanged);
            //
            // lblThoiGianLam
            //
            this.lblThoiGianLam.AutoSize = true;
            this.lblThoiGianLam.Location = new System.Drawing.Point(300, 78);
            this.lblThoiGianLam.Name = "lblThoiGianLam";
            this.lblThoiGianLam.Size = new System.Drawing.Size(128, 15);
            this.lblThoiGianLam.TabIndex = 4;
            this.lblThoiGianLam.Text = "Thời gian làm (phút) *";
            //
            // nudThoiGianLam
            //
            this.nudThoiGianLam.Location = new System.Drawing.Point(300, 96);
            this.nudThoiGianLam.Maximum = new decimal(new int[] { 180, 0, 0, 0 });
            this.nudThoiGianLam.Minimum = new decimal(new int[] { 5, 0, 0, 0 });
            this.nudThoiGianLam.Name = "nudThoiGianLam";
            this.nudThoiGianLam.Size = new System.Drawing.Size(90, 23);
            this.nudThoiGianLam.TabIndex = 3;
            this.nudThoiGianLam.Value = new decimal(new int[] { 15, 0, 0, 0 });
            //
            // lblNganHang
            //
            this.lblNganHang.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblNganHang.Location = new System.Drawing.Point(15, 128);
            this.lblNganHang.Name = "lblNganHang";
            this.lblNganHang.Size = new System.Drawing.Size(435, 20);
            this.lblNganHang.TabIndex = 6;
            this.lblNganHang.Text = "Ngân hàng câu hỏi";
            //
            // lblSoCauDe
            //
            this.lblSoCauDe.AutoSize = true;
            this.lblSoCauDe.Location = new System.Drawing.Point(15, 154);
            this.lblSoCauDe.Name = "lblSoCauDe";
            this.lblSoCauDe.Size = new System.Drawing.Size(58, 15);
            this.lblSoCauDe.TabIndex = 7;
            this.lblSoCauDe.Text = "Số câu dễ";
            //
            // nudSoCauDe
            //
            this.nudSoCauDe.Location = new System.Drawing.Point(15, 172);
            this.nudSoCauDe.Name = "nudSoCauDe";
            this.nudSoCauDe.Size = new System.Drawing.Size(90, 23);
            this.nudSoCauDe.TabIndex = 4;
            this.nudSoCauDe.ValueChanged += new System.EventHandler(this.nudSoCau_ValueChanged);
            //
            // lblSoCauTB
            //
            this.lblSoCauTB.AutoSize = true;
            this.lblSoCauTB.Location = new System.Drawing.Point(120, 154);
            this.lblSoCauTB.Name = "lblSoCauTB";
            this.lblSoCauTB.Size = new System.Drawing.Size(103, 15);
            this.lblSoCauTB.TabIndex = 9;
            this.lblSoCauTB.Text = "Số câu trung bình";
            //
            // nudSoCauTB
            //
            this.nudSoCauTB.Location = new System.Drawing.Point(120, 172);
            this.nudSoCauTB.Name = "nudSoCauTB";
            this.nudSoCauTB.Size = new System.Drawing.Size(90, 23);
            this.nudSoCauTB.TabIndex = 5;
            this.nudSoCauTB.ValueChanged += new System.EventHandler(this.nudSoCau_ValueChanged);
            //
            // lblSoCauKho
            //
            this.lblSoCauKho.AutoSize = true;
            this.lblSoCauKho.Location = new System.Drawing.Point(235, 154);
            this.lblSoCauKho.Name = "lblSoCauKho";
            this.lblSoCauKho.Size = new System.Drawing.Size(63, 15);
            this.lblSoCauKho.TabIndex = 11;
            this.lblSoCauKho.Text = "Số câu khó";
            //
            // nudSoCauKho
            //
            this.nudSoCauKho.Location = new System.Drawing.Point(235, 172);
            this.nudSoCauKho.Name = "nudSoCauKho";
            this.nudSoCauKho.Size = new System.Drawing.Size(90, 23);
            this.nudSoCauKho.TabIndex = 6;
            this.nudSoCauKho.ValueChanged += new System.EventHandler(this.nudSoCau_ValueChanged);
            //
            // lblTongSoCau
            //
            this.lblTongSoCau.AutoSize = true;
            this.lblTongSoCau.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTongSoCau.Location = new System.Drawing.Point(345, 175);
            this.lblTongSoCau.Name = "lblTongSoCau";
            this.lblTongSoCau.Size = new System.Drawing.Size(77, 15);
            this.lblTongSoCau.TabIndex = 13;
            this.lblTongSoCau.Text = "Tổng: 0 câu";
            //
            // lblThoiGianMo
            //
            this.lblThoiGianMo.AutoSize = true;
            this.lblThoiGianMo.Location = new System.Drawing.Point(15, 206);
            this.lblThoiGianMo.Name = "lblThoiGianMo";
            this.lblThoiGianMo.Size = new System.Drawing.Size(85, 15);
            this.lblThoiGianMo.TabIndex = 14;
            this.lblThoiGianMo.Text = "Thời gian mở *";
            //
            // dtpThoiGianMo
            //
            this.dtpThoiGianMo.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpThoiGianMo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpThoiGianMo.Location = new System.Drawing.Point(15, 224);
            this.dtpThoiGianMo.Name = "dtpThoiGianMo";
            this.dtpThoiGianMo.Size = new System.Drawing.Size(200, 23);
            this.dtpThoiGianMo.TabIndex = 7;
            //
            // lblThoiGianDong
            //
            this.lblThoiGianDong.AutoSize = true;
            this.lblThoiGianDong.Location = new System.Drawing.Point(235, 206);
            this.lblThoiGianDong.Name = "lblThoiGianDong";
            this.lblThoiGianDong.Size = new System.Drawing.Size(97, 15);
            this.lblThoiGianDong.TabIndex = 16;
            this.lblThoiGianDong.Text = "Thời gian đóng *";
            //
            // dtpThoiGianDong
            //
            this.dtpThoiGianDong.CustomFormat = "dd/MM/yyyy HH:mm";
            this.dtpThoiGianDong.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpThoiGianDong.Location = new System.Drawing.Point(235, 224);
            this.dtpThoiGianDong.Name = "dtpThoiGianDong";
            this.dtpThoiGianDong.Size = new System.Drawing.Size(200, 23);
            this.dtpThoiGianDong.TabIndex = 8;
            //
            // lblSoLanThi
            //
            this.lblSoLanThi.AutoSize = true;
            this.lblSoLanThi.Location = new System.Drawing.Point(15, 258);
            this.lblSoLanThi.Name = "lblSoLanThi";
            this.lblSoLanThi.Size = new System.Drawing.Size(109, 15);
            this.lblSoLanThi.TabIndex = 18;
            this.lblSoLanThi.Text = "Số lần thi tối đa *";
            //
            // nudSoLanThi
            //
            this.nudSoLanThi.Location = new System.Drawing.Point(15, 276);
            this.nudSoLanThi.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            this.nudSoLanThi.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudSoLanThi.Name = "nudSoLanThi";
            this.nudSoLanThi.Size = new System.Drawing.Size(90, 23);
            this.nudSoLanThi.TabIndex = 9;
            this.nudSoLanThi.Value = new decimal(new int[] { 1, 0, 0, 0 });
            //
            // lblCanhBao
            //
            this.lblCanhBao.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblCanhBao.Location = new System.Drawing.Point(15, 312);
            this.lblCanhBao.Name = "lblCanhBao";
            this.lblCanhBao.Size = new System.Drawing.Size(435, 40);
            this.lblCanhBao.TabIndex = 20;
            //
            // lblCheDo
            //
            this.lblCheDo.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblCheDo.Location = new System.Drawing.Point(15, 358);
            this.lblCheDo.Name = "lblCheDo";
            this.lblCheDo.Size = new System.Drawing.Size(435, 20);
            this.lblCheDo.TabIndex = 21;
            this.lblCheDo.Text = "Chế độ";
            //
            // btnThemMoi
            //
            this.btnThemMoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnThemMoi.Location = new System.Drawing.Point(15, 500);
            this.btnThemMoi.Name = "btnThemMoi";
            this.btnThemMoi.Size = new System.Drawing.Size(90, 32);
            this.btnThemMoi.TabIndex = 10;
            this.btnThemMoi.Text = "Thêm mới";
            this.btnThemMoi.UseVisualStyleBackColor = true;
            this.btnThemMoi.Click += new System.EventHandler(this.btnThemMoi_Click);
            //
            // btnLuu
            //
            this.btnLuu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLuu.Location = new System.Drawing.Point(111, 500);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(90, 32);
            this.btnLuu.TabIndex = 11;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            //
            // btnXoa
            //
            this.btnXoa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnXoa.Location = new System.Drawing.Point(207, 500);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(90, 32);
            this.btnXoa.TabIndex = 12;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            //
            // btnLamMoi
            //
            this.btnLamMoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLamMoi.Location = new System.Drawing.Point(303, 500);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(90, 32);
            this.btnLamMoi.TabIndex = 13;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = true;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            //
            // frmDeThi
            //
            this.AcceptButton = this.btnTim;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1164, 601);
            this.Controls.Add(this.grpChiTiet);
            this.Controls.Add(this.dgvDeThi);
            this.Controls.Add(this.btnTim);
            this.Controls.Add(this.txtTuKhoa);
            this.Controls.Add(this.lblTuKhoa);
            this.Controls.Add(this.cboTrangThaiLoc);
            this.Controls.Add(this.lblTrangThaiLoc);
            this.Controls.Add(this.cboMonHocLoc);
            this.Controls.Add(this.lblMonHocLoc);
            this.Name = "frmDeThi";
            this.Text = "Quản lý đề thi";
            this.Load += new System.EventHandler(this.frmDeThi_Load);
            this.Shown += new System.EventHandler(this.frmDeThi_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeThi)).EndInit();
            this.grpChiTiet.ResumeLayout(false);
            this.grpChiTiet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudThoiGianLam)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoCauDe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoCauTB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoCauKho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLanThi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblMonHocLoc;
        private System.Windows.Forms.ComboBox cboMonHocLoc;
        private System.Windows.Forms.Label lblTrangThaiLoc;
        private System.Windows.Forms.ComboBox cboTrangThaiLoc;
        private System.Windows.Forms.Label lblTuKhoa;
        private System.Windows.Forms.TextBox txtTuKhoa;
        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.DataGridView dgvDeThi;
        private System.Windows.Forms.GroupBox grpChiTiet;
        private System.Windows.Forms.Label lblTenDe;
        private System.Windows.Forms.TextBox txtTenDe;
        private System.Windows.Forms.Label lblMonHoc;
        private System.Windows.Forms.ComboBox cboMonHoc;
        private System.Windows.Forms.Label lblThoiGianLam;
        private System.Windows.Forms.NumericUpDown nudThoiGianLam;
        private System.Windows.Forms.Label lblNganHang;
        private System.Windows.Forms.Label lblSoCauDe;
        private System.Windows.Forms.NumericUpDown nudSoCauDe;
        private System.Windows.Forms.Label lblSoCauTB;
        private System.Windows.Forms.NumericUpDown nudSoCauTB;
        private System.Windows.Forms.Label lblSoCauKho;
        private System.Windows.Forms.NumericUpDown nudSoCauKho;
        private System.Windows.Forms.Label lblTongSoCau;
        private System.Windows.Forms.Label lblThoiGianMo;
        private System.Windows.Forms.DateTimePicker dtpThoiGianMo;
        private System.Windows.Forms.Label lblThoiGianDong;
        private System.Windows.Forms.DateTimePicker dtpThoiGianDong;
        private System.Windows.Forms.Label lblSoLanThi;
        private System.Windows.Forms.NumericUpDown nudSoLanThi;
        private System.Windows.Forms.Label lblCanhBao;
        private System.Windows.Forms.Label lblCheDo;
        private System.Windows.Forms.Button btnThemMoi;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnLamMoi;
    }
}
