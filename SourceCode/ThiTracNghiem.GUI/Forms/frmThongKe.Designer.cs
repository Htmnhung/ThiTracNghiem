namespace ThiTracNghiem.GUI
{
    partial class frmThongKe
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
            this.tabChinh = new System.Windows.Forms.TabControl();
            this.tabKetQua = new System.Windows.Forms.TabPage();
            this.lblDeThi = new System.Windows.Forms.Label();
            this.cboDeThi = new System.Windows.Forms.ComboBox();
            this.lblLop = new System.Windows.Forms.Label();
            this.cboLop = new System.Windows.Forms.ComboBox();
            this.chkLocDiem = new System.Windows.Forms.CheckBox();
            this.nudDiemTu = new System.Windows.Forms.NumericUpDown();
            this.lblDenDiem = new System.Windows.Forms.Label();
            this.nudDiemDen = new System.Windows.Forms.NumericUpDown();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.btnTimKetQua = new System.Windows.Forms.Button();
            this.btnXemBaiLam = new System.Windows.Forms.Button();
            this.lblTongKetKetQua = new System.Windows.Forms.Label();
            this.dgvKetQua = new System.Windows.Forms.DataGridView();
            this.tabTheoDe = new System.Windows.Forms.TabPage();
            this.btnTaiThongKe = new System.Windows.Forms.Button();
            this.lblHuongDanDe = new System.Windows.Forms.Label();
            this.dgvThongKeDe = new System.Windows.Forms.DataGridView();
            this.bieuDoPhoDiem = new ThiTracNghiem.GUI.BieuDoCot();
            this.tabCauSai = new System.Windows.Forms.TabPage();
            this.lblMonHocCauSai = new System.Windows.Forms.Label();
            this.cboMonHocCauSai = new System.Windows.Forms.ComboBox();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.nudSoLuong = new System.Windows.Forms.NumericUpDown();
            this.btnXemCauSai = new System.Windows.Forms.Button();
            this.dgvCauSai = new System.Windows.Forms.DataGridView();
            this.tabChuaThi = new System.Windows.Forms.TabPage();
            this.lblDeChuaThi = new System.Windows.Forms.Label();
            this.cboDeChuaThi = new System.Windows.Forms.ComboBox();
            this.lblLopChuaThi = new System.Windows.Forms.Label();
            this.cboLopChuaThi = new System.Windows.Forms.ComboBox();
            this.btnXemChuaThi = new System.Windows.Forms.Button();
            this.lblTongChuaThi = new System.Windows.Forms.Label();
            this.dgvChuaThi = new System.Windows.Forms.DataGridView();
            this.tabChinh.SuspendLayout();
            this.tabKetQua.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDiemTu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDiemDen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQua)).BeginInit();
            this.tabTheoDe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongKeDe)).BeginInit();
            this.tabCauSai.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCauSai)).BeginInit();
            this.tabChuaThi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChuaThi)).BeginInit();
            this.SuspendLayout();
            //
            // tabChinh
            //
            this.tabChinh.Controls.Add(this.tabKetQua);
            this.tabChinh.Controls.Add(this.tabTheoDe);
            this.tabChinh.Controls.Add(this.tabCauSai);
            this.tabChinh.Controls.Add(this.tabChuaThi);
            this.tabChinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabChinh.Location = new System.Drawing.Point(0, 0);
            this.tabChinh.Name = "tabChinh";
            this.tabChinh.SelectedIndex = 0;
            this.tabChinh.Size = new System.Drawing.Size(1164, 641);
            this.tabChinh.TabIndex = 0;
            this.tabChinh.SelectedIndexChanged += new System.EventHandler(this.tabChinh_SelectedIndexChanged);
            //
            // tabKetQua
            //
            this.tabKetQua.Controls.Add(this.lblDeThi);
            this.tabKetQua.Controls.Add(this.cboDeThi);
            this.tabKetQua.Controls.Add(this.lblLop);
            this.tabKetQua.Controls.Add(this.cboLop);
            this.tabKetQua.Controls.Add(this.chkLocDiem);
            this.tabKetQua.Controls.Add(this.nudDiemTu);
            this.tabKetQua.Controls.Add(this.lblDenDiem);
            this.tabKetQua.Controls.Add(this.nudDiemDen);
            this.tabKetQua.Controls.Add(this.lblTuNgay);
            this.tabKetQua.Controls.Add(this.dtpTuNgay);
            this.tabKetQua.Controls.Add(this.lblDenNgay);
            this.tabKetQua.Controls.Add(this.dtpDenNgay);
            this.tabKetQua.Controls.Add(this.btnTimKetQua);
            this.tabKetQua.Controls.Add(this.btnXemBaiLam);
            this.tabKetQua.Controls.Add(this.lblTongKetKetQua);
            this.tabKetQua.Controls.Add(this.dgvKetQua);
            this.tabKetQua.Location = new System.Drawing.Point(4, 24);
            this.tabKetQua.Name = "tabKetQua";
            this.tabKetQua.Padding = new System.Windows.Forms.Padding(3);
            this.tabKetQua.Size = new System.Drawing.Size(1156, 613);
            this.tabKetQua.TabIndex = 0;
            this.tabKetQua.Text = "Tra cứu kết quả";
            this.tabKetQua.UseVisualStyleBackColor = true;
            //
            // lblDeThi
            //
            this.lblDeThi.AutoSize = true;
            this.lblDeThi.Location = new System.Drawing.Point(10, 15);
            this.lblDeThi.Name = "lblDeThi";
            this.lblDeThi.Size = new System.Drawing.Size(39, 15);
            this.lblDeThi.TabIndex = 0;
            this.lblDeThi.Text = "Đề thi";
            //
            // cboDeThi
            //
            this.cboDeThi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDeThi.Location = new System.Drawing.Point(90, 11);
            this.cboDeThi.Name = "cboDeThi";
            this.cboDeThi.Size = new System.Drawing.Size(300, 23);
            this.cboDeThi.TabIndex = 1;
            //
            // lblLop
            //
            this.lblLop.AutoSize = true;
            this.lblLop.Location = new System.Drawing.Point(405, 15);
            this.lblLop.Name = "lblLop";
            this.lblLop.Size = new System.Drawing.Size(27, 15);
            this.lblLop.TabIndex = 2;
            this.lblLop.Text = "Lớp";
            //
            // cboLop
            //
            this.cboLop.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLop.Location = new System.Drawing.Point(440, 11);
            this.cboLop.Name = "cboLop";
            this.cboLop.Size = new System.Drawing.Size(120, 23);
            this.cboLop.TabIndex = 3;
            //
            // chkLocDiem
            //
            this.chkLocDiem.AutoSize = true;
            this.chkLocDiem.Location = new System.Drawing.Point(580, 13);
            this.chkLocDiem.Name = "chkLocDiem";
            this.chkLocDiem.Size = new System.Drawing.Size(97, 19);
            this.chkLocDiem.TabIndex = 4;
            this.chkLocDiem.Text = "Lọc điểm từ";
            this.chkLocDiem.UseVisualStyleBackColor = true;
            //
            // nudDiemTu
            //
            this.nudDiemTu.DecimalPlaces = 2;
            this.nudDiemTu.Increment = new decimal(new int[] { 25, 0, 0, 131072 });
            this.nudDiemTu.Location = new System.Drawing.Point(680, 11);
            this.nudDiemTu.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            this.nudDiemTu.Name = "nudDiemTu";
            this.nudDiemTu.Size = new System.Drawing.Size(65, 23);
            this.nudDiemTu.TabIndex = 5;
            //
            // lblDenDiem
            //
            this.lblDenDiem.AutoSize = true;
            this.lblDenDiem.Location = new System.Drawing.Point(752, 15);
            this.lblDenDiem.Name = "lblDenDiem";
            this.lblDenDiem.Size = new System.Drawing.Size(26, 15);
            this.lblDenDiem.TabIndex = 6;
            this.lblDenDiem.Text = "đến";
            //
            // nudDiemDen
            //
            this.nudDiemDen.DecimalPlaces = 2;
            this.nudDiemDen.Increment = new decimal(new int[] { 25, 0, 0, 131072 });
            this.nudDiemDen.Location = new System.Drawing.Point(784, 11);
            this.nudDiemDen.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            this.nudDiemDen.Name = "nudDiemDen";
            this.nudDiemDen.Size = new System.Drawing.Size(65, 23);
            this.nudDiemDen.TabIndex = 7;
            this.nudDiemDen.Value = new decimal(new int[] { 10, 0, 0, 0 });
            //
            // lblTuNgay
            //
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Location = new System.Drawing.Point(10, 48);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(72, 15);
            this.lblTuNgay.TabIndex = 8;
            this.lblTuNgay.Text = "Nộp từ ngày";
            //
            // dtpTuNgay
            //
            this.dtpTuNgay.Checked = false;
            this.dtpTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTuNgay.Location = new System.Drawing.Point(90, 44);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.ShowCheckBox = true;
            this.dtpTuNgay.Size = new System.Drawing.Size(135, 23);
            this.dtpTuNgay.TabIndex = 9;
            //
            // lblDenNgay
            //
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Location = new System.Drawing.Point(235, 48);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(55, 15);
            this.lblDenNgay.TabIndex = 10;
            this.lblDenNgay.Text = "đến ngày";
            //
            // dtpDenNgay
            //
            this.dtpDenNgay.Checked = false;
            this.dtpDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dtpDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDenNgay.Location = new System.Drawing.Point(296, 44);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.ShowCheckBox = true;
            this.dtpDenNgay.Size = new System.Drawing.Size(135, 23);
            this.dtpDenNgay.TabIndex = 11;
            //
            // btnTimKetQua
            //
            this.btnTimKetQua.Location = new System.Drawing.Point(440, 42);
            this.btnTimKetQua.Name = "btnTimKetQua";
            this.btnTimKetQua.Size = new System.Drawing.Size(100, 27);
            this.btnTimKetQua.TabIndex = 12;
            this.btnTimKetQua.Text = "Tìm kiếm";
            this.btnTimKetQua.UseVisualStyleBackColor = true;
            this.btnTimKetQua.Click += new System.EventHandler(this.btnTimKetQua_Click);
            //
            // btnXemBaiLam
            //
            this.btnXemBaiLam.Location = new System.Drawing.Point(550, 42);
            this.btnXemBaiLam.Name = "btnXemBaiLam";
            this.btnXemBaiLam.Size = new System.Drawing.Size(120, 27);
            this.btnXemBaiLam.TabIndex = 13;
            this.btnXemBaiLam.Text = "Xem bài làm";
            this.btnXemBaiLam.UseVisualStyleBackColor = true;
            this.btnXemBaiLam.Click += new System.EventHandler(this.btnXemBaiLam_Click);
            //
            // lblTongKetKetQua
            //
            this.lblTongKetKetQua.AutoSize = true;
            this.lblTongKetKetQua.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTongKetKetQua.Location = new System.Drawing.Point(10, 80);
            this.lblTongKetKetQua.Name = "lblTongKetKetQua";
            this.lblTongKetKetQua.Size = new System.Drawing.Size(0, 15);
            this.lblTongKetKetQua.TabIndex = 14;
            //
            // dgvKetQua
            //
            this.dgvKetQua.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvKetQua.Location = new System.Drawing.Point(10, 104);
            this.dgvKetQua.Name = "dgvKetQua";
            this.dgvKetQua.Size = new System.Drawing.Size(1136, 499);
            this.dgvKetQua.TabIndex = 15;
            this.dgvKetQua.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvKetQua_CellDoubleClick);
            //
            // tabTheoDe
            //
            this.tabTheoDe.Controls.Add(this.btnTaiThongKe);
            this.tabTheoDe.Controls.Add(this.lblHuongDanDe);
            this.tabTheoDe.Controls.Add(this.dgvThongKeDe);
            this.tabTheoDe.Controls.Add(this.bieuDoPhoDiem);
            this.tabTheoDe.Location = new System.Drawing.Point(4, 24);
            this.tabTheoDe.Name = "tabTheoDe";
            this.tabTheoDe.Padding = new System.Windows.Forms.Padding(3);
            this.tabTheoDe.Size = new System.Drawing.Size(1156, 613);
            this.tabTheoDe.TabIndex = 1;
            this.tabTheoDe.Text = "Thống kê theo đề";
            this.tabTheoDe.UseVisualStyleBackColor = true;
            //
            // btnTaiThongKe
            //
            this.btnTaiThongKe.Location = new System.Drawing.Point(10, 10);
            this.btnTaiThongKe.Name = "btnTaiThongKe";
            this.btnTaiThongKe.Size = new System.Drawing.Size(100, 27);
            this.btnTaiThongKe.TabIndex = 0;
            this.btnTaiThongKe.Text = "Làm mới";
            this.btnTaiThongKe.UseVisualStyleBackColor = true;
            this.btnTaiThongKe.Click += new System.EventHandler(this.btnTaiThongKe_Click);
            //
            // lblHuongDanDe
            //
            this.lblHuongDanDe.AutoSize = true;
            this.lblHuongDanDe.ForeColor = System.Drawing.Color.DimGray;
            this.lblHuongDanDe.Location = new System.Drawing.Point(120, 16);
            this.lblHuongDanDe.Name = "lblHuongDanDe";
            this.lblHuongDanDe.Size = new System.Drawing.Size(260, 15);
            this.lblHuongDanDe.TabIndex = 1;
            this.lblHuongDanDe.Text = "Chọn một đề trong bảng để xem biểu đồ phổ điểm.";
            //
            // dgvThongKeDe
            //
            this.dgvThongKeDe.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvThongKeDe.Location = new System.Drawing.Point(10, 45);
            this.dgvThongKeDe.Name = "dgvThongKeDe";
            this.dgvThongKeDe.Size = new System.Drawing.Size(1136, 220);
            this.dgvThongKeDe.TabIndex = 2;
            this.dgvThongKeDe.SelectionChanged += new System.EventHandler(this.dgvThongKeDe_SelectionChanged);
            //
            // bieuDoPhoDiem
            //
            this.bieuDoPhoDiem.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.bieuDoPhoDiem.BackColor = System.Drawing.Color.White;
            this.bieuDoPhoDiem.Location = new System.Drawing.Point(10, 275);
            this.bieuDoPhoDiem.Name = "bieuDoPhoDiem";
            this.bieuDoPhoDiem.Size = new System.Drawing.Size(1136, 328);
            this.bieuDoPhoDiem.TabIndex = 3;
            this.bieuDoPhoDiem.TieuDe = "Phổ điểm";
            //
            // tabCauSai
            //
            this.tabCauSai.Controls.Add(this.lblMonHocCauSai);
            this.tabCauSai.Controls.Add(this.cboMonHocCauSai);
            this.tabCauSai.Controls.Add(this.lblSoLuong);
            this.tabCauSai.Controls.Add(this.nudSoLuong);
            this.tabCauSai.Controls.Add(this.btnXemCauSai);
            this.tabCauSai.Controls.Add(this.dgvCauSai);
            this.tabCauSai.Location = new System.Drawing.Point(4, 24);
            this.tabCauSai.Name = "tabCauSai";
            this.tabCauSai.Padding = new System.Windows.Forms.Padding(3);
            this.tabCauSai.Size = new System.Drawing.Size(1156, 613);
            this.tabCauSai.TabIndex = 2;
            this.tabCauSai.Text = "Câu hỏi sai nhiều";
            this.tabCauSai.UseVisualStyleBackColor = true;
            //
            // lblMonHocCauSai
            //
            this.lblMonHocCauSai.AutoSize = true;
            this.lblMonHocCauSai.Location = new System.Drawing.Point(10, 15);
            this.lblMonHocCauSai.Name = "lblMonHocCauSai";
            this.lblMonHocCauSai.Size = new System.Drawing.Size(53, 15);
            this.lblMonHocCauSai.TabIndex = 0;
            this.lblMonHocCauSai.Text = "Môn học";
            //
            // cboMonHocCauSai
            //
            this.cboMonHocCauSai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMonHocCauSai.Location = new System.Drawing.Point(70, 11);
            this.cboMonHocCauSai.Name = "cboMonHocCauSai";
            this.cboMonHocCauSai.Size = new System.Drawing.Size(250, 23);
            this.cboMonHocCauSai.TabIndex = 1;
            //
            // lblSoLuong
            //
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Location = new System.Drawing.Point(335, 15);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(66, 15);
            this.lblSoLuong.TabIndex = 2;
            this.lblSoLuong.Text = "Số câu xem";
            //
            // nudSoLuong
            //
            this.nudSoLuong.Location = new System.Drawing.Point(407, 11);
            this.nudSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudSoLuong.Name = "nudSoLuong";
            this.nudSoLuong.Size = new System.Drawing.Size(65, 23);
            this.nudSoLuong.TabIndex = 3;
            this.nudSoLuong.Value = new decimal(new int[] { 10, 0, 0, 0 });
            //
            // btnXemCauSai
            //
            this.btnXemCauSai.Location = new System.Drawing.Point(485, 9);
            this.btnXemCauSai.Name = "btnXemCauSai";
            this.btnXemCauSai.Size = new System.Drawing.Size(100, 27);
            this.btnXemCauSai.TabIndex = 4;
            this.btnXemCauSai.Text = "Xem";
            this.btnXemCauSai.UseVisualStyleBackColor = true;
            this.btnXemCauSai.Click += new System.EventHandler(this.btnXemCauSai_Click);
            //
            // dgvCauSai
            //
            this.dgvCauSai.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCauSai.Location = new System.Drawing.Point(10, 45);
            this.dgvCauSai.Name = "dgvCauSai";
            this.dgvCauSai.Size = new System.Drawing.Size(1136, 558);
            this.dgvCauSai.TabIndex = 5;
            //
            // tabChuaThi
            //
            this.tabChuaThi.Controls.Add(this.lblDeChuaThi);
            this.tabChuaThi.Controls.Add(this.cboDeChuaThi);
            this.tabChuaThi.Controls.Add(this.lblLopChuaThi);
            this.tabChuaThi.Controls.Add(this.cboLopChuaThi);
            this.tabChuaThi.Controls.Add(this.btnXemChuaThi);
            this.tabChuaThi.Controls.Add(this.lblTongChuaThi);
            this.tabChuaThi.Controls.Add(this.dgvChuaThi);
            this.tabChuaThi.Location = new System.Drawing.Point(4, 24);
            this.tabChuaThi.Name = "tabChuaThi";
            this.tabChuaThi.Padding = new System.Windows.Forms.Padding(3);
            this.tabChuaThi.Size = new System.Drawing.Size(1156, 613);
            this.tabChuaThi.TabIndex = 3;
            this.tabChuaThi.Text = "Sinh viên chưa thi";
            this.tabChuaThi.UseVisualStyleBackColor = true;
            //
            // lblDeChuaThi
            //
            this.lblDeChuaThi.AutoSize = true;
            this.lblDeChuaThi.Location = new System.Drawing.Point(10, 15);
            this.lblDeChuaThi.Name = "lblDeChuaThi";
            this.lblDeChuaThi.Size = new System.Drawing.Size(39, 15);
            this.lblDeChuaThi.TabIndex = 0;
            this.lblDeChuaThi.Text = "Đề thi";
            //
            // cboDeChuaThi
            //
            this.cboDeChuaThi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDeChuaThi.Location = new System.Drawing.Point(60, 11);
            this.cboDeChuaThi.Name = "cboDeChuaThi";
            this.cboDeChuaThi.Size = new System.Drawing.Size(320, 23);
            this.cboDeChuaThi.TabIndex = 1;
            //
            // lblLopChuaThi
            //
            this.lblLopChuaThi.AutoSize = true;
            this.lblLopChuaThi.Location = new System.Drawing.Point(395, 15);
            this.lblLopChuaThi.Name = "lblLopChuaThi";
            this.lblLopChuaThi.Size = new System.Drawing.Size(27, 15);
            this.lblLopChuaThi.TabIndex = 2;
            this.lblLopChuaThi.Text = "Lớp";
            //
            // cboLopChuaThi
            //
            this.cboLopChuaThi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLopChuaThi.Location = new System.Drawing.Point(430, 11);
            this.cboLopChuaThi.Name = "cboLopChuaThi";
            this.cboLopChuaThi.Size = new System.Drawing.Size(120, 23);
            this.cboLopChuaThi.TabIndex = 3;
            //
            // btnXemChuaThi
            //
            this.btnXemChuaThi.Location = new System.Drawing.Point(565, 9);
            this.btnXemChuaThi.Name = "btnXemChuaThi";
            this.btnXemChuaThi.Size = new System.Drawing.Size(100, 27);
            this.btnXemChuaThi.TabIndex = 4;
            this.btnXemChuaThi.Text = "Xem";
            this.btnXemChuaThi.UseVisualStyleBackColor = true;
            this.btnXemChuaThi.Click += new System.EventHandler(this.btnXemChuaThi_Click);
            //
            // lblTongChuaThi
            //
            this.lblTongChuaThi.AutoSize = true;
            this.lblTongChuaThi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTongChuaThi.Location = new System.Drawing.Point(680, 15);
            this.lblTongChuaThi.Name = "lblTongChuaThi";
            this.lblTongChuaThi.Size = new System.Drawing.Size(0, 15);
            this.lblTongChuaThi.TabIndex = 5;
            //
            // dgvChuaThi
            //
            this.dgvChuaThi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvChuaThi.Location = new System.Drawing.Point(10, 45);
            this.dgvChuaThi.Name = "dgvChuaThi";
            this.dgvChuaThi.Size = new System.Drawing.Size(1136, 558);
            this.dgvChuaThi.TabIndex = 6;
            //
            // frmThongKe
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1164, 641);
            this.Controls.Add(this.tabChinh);
            this.Name = "frmThongKe";
            this.Text = "Kết quả - Thống kê";
            this.Load += new System.EventHandler(this.frmThongKe_Load);
            this.Shown += new System.EventHandler(this.frmThongKe_Shown);
            this.tabChinh.ResumeLayout(false);
            this.tabKetQua.ResumeLayout(false);
            this.tabKetQua.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudDiemTu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDiemDen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvKetQua)).EndInit();
            this.tabTheoDe.ResumeLayout(false);
            this.tabTheoDe.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThongKeDe)).EndInit();
            this.tabCauSai.ResumeLayout(false);
            this.tabCauSai.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCauSai)).EndInit();
            this.tabChuaThi.ResumeLayout(false);
            this.tabChuaThi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChuaThi)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabChinh;
        private System.Windows.Forms.TabPage tabKetQua;
        private System.Windows.Forms.Label lblDeThi;
        private System.Windows.Forms.ComboBox cboDeThi;
        private System.Windows.Forms.Label lblLop;
        private System.Windows.Forms.ComboBox cboLop;
        private System.Windows.Forms.CheckBox chkLocDiem;
        private System.Windows.Forms.NumericUpDown nudDiemTu;
        private System.Windows.Forms.Label lblDenDiem;
        private System.Windows.Forms.NumericUpDown nudDiemDen;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Button btnTimKetQua;
        private System.Windows.Forms.Button btnXemBaiLam;
        private System.Windows.Forms.Label lblTongKetKetQua;
        private System.Windows.Forms.DataGridView dgvKetQua;
        private System.Windows.Forms.TabPage tabTheoDe;
        private System.Windows.Forms.Button btnTaiThongKe;
        private System.Windows.Forms.Label lblHuongDanDe;
        private System.Windows.Forms.DataGridView dgvThongKeDe;
        private ThiTracNghiem.GUI.BieuDoCot bieuDoPhoDiem;
        private System.Windows.Forms.TabPage tabCauSai;
        private System.Windows.Forms.Label lblMonHocCauSai;
        private System.Windows.Forms.ComboBox cboMonHocCauSai;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.NumericUpDown nudSoLuong;
        private System.Windows.Forms.Button btnXemCauSai;
        private System.Windows.Forms.DataGridView dgvCauSai;
        private System.Windows.Forms.TabPage tabChuaThi;
        private System.Windows.Forms.Label lblDeChuaThi;
        private System.Windows.Forms.ComboBox cboDeChuaThi;
        private System.Windows.Forms.Label lblLopChuaThi;
        private System.Windows.Forms.ComboBox cboLopChuaThi;
        private System.Windows.Forms.Button btnXemChuaThi;
        private System.Windows.Forms.Label lblTongChuaThi;
        private System.Windows.Forms.DataGridView dgvChuaThi;
    }
}
