namespace ThiTracNghiem.GUI
{
    partial class frmLamBai
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
            this.components = new System.ComponentModel.Container();
            this.pnlTren = new System.Windows.Forms.Panel();
            this.lblTenDe = new System.Windows.Forms.Label();
            this.lblThongTin = new System.Windows.Forms.Label();
            this.lblDongHo = new System.Windows.Forms.Label();
            this.pnlTrai = new System.Windows.Forms.Panel();
            this.lblDanhSachCau = new System.Windows.Forms.Label();
            this.flpDanhSachCau = new System.Windows.Forms.FlowLayoutPanel();
            this.lblChuThich = new System.Windows.Forms.Label();
            this.pnlDuoi = new System.Windows.Forms.Panel();
            this.btnCauTruoc = new System.Windows.Forms.Button();
            this.btnCauSau = new System.Windows.Forms.Button();
            this.lblDaTraLoi = new System.Windows.Forms.Label();
            this.btnNopBai = new System.Windows.Forms.Button();
            this.pnlGiua = new System.Windows.Forms.Panel();
            this.lblSoCau = new System.Windows.Forms.Label();
            this.lblLoaiCau = new System.Windows.Forms.Label();
            this.txtNoiDung = new System.Windows.Forms.TextBox();
            this.flpPhuongAn = new System.Windows.Forms.FlowLayoutPanel();
            this.lblKetQuaCau = new System.Windows.Forms.Label();
            this.timerDongHo = new System.Windows.Forms.Timer(this.components);
            this.pnlTren.SuspendLayout();
            this.pnlTrai.SuspendLayout();
            this.pnlDuoi.SuspendLayout();
            this.pnlGiua.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlTren
            //
            this.pnlTren.BackColor = System.Drawing.Color.FromArgb(31, 78, 121);
            this.pnlTren.Controls.Add(this.lblTenDe);
            this.pnlTren.Controls.Add(this.lblThongTin);
            this.pnlTren.Controls.Add(this.lblDongHo);
            this.pnlTren.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTren.Location = new System.Drawing.Point(0, 0);
            this.pnlTren.Name = "pnlTren";
            this.pnlTren.Size = new System.Drawing.Size(1084, 72);
            this.pnlTren.TabIndex = 3;
            //
            // lblTenDe
            //
            this.lblTenDe.AutoSize = true;
            this.lblTenDe.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTenDe.ForeColor = System.Drawing.Color.White;
            this.lblTenDe.Location = new System.Drawing.Point(12, 9);
            this.lblTenDe.Name = "lblTenDe";
            this.lblTenDe.Size = new System.Drawing.Size(70, 25);
            this.lblTenDe.TabIndex = 0;
            this.lblTenDe.Text = "Tên đề";
            //
            // lblThongTin
            //
            this.lblThongTin.AutoSize = true;
            this.lblThongTin.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblThongTin.Location = new System.Drawing.Point(14, 42);
            this.lblThongTin.Name = "lblThongTin";
            this.lblThongTin.Size = new System.Drawing.Size(59, 15);
            this.lblThongTin.TabIndex = 1;
            this.lblThongTin.Text = "Thông tin";
            //
            // lblDongHo
            //
            this.lblDongHo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDongHo.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblDongHo.ForeColor = System.Drawing.Color.White;
            this.lblDongHo.Location = new System.Drawing.Point(764, 12);
            this.lblDongHo.Name = "lblDongHo";
            this.lblDongHo.Size = new System.Drawing.Size(308, 48);
            this.lblDongHo.TabIndex = 2;
            this.lblDongHo.Text = "00:00";
            this.lblDongHo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // pnlTrai
            //
            this.pnlTrai.BackColor = System.Drawing.Color.FromArgb(243, 246, 250);
            this.pnlTrai.Controls.Add(this.lblDanhSachCau);
            this.pnlTrai.Controls.Add(this.flpDanhSachCau);
            this.pnlTrai.Controls.Add(this.lblChuThich);
            this.pnlTrai.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlTrai.Location = new System.Drawing.Point(0, 72);
            this.pnlTrai.Name = "pnlTrai";
            this.pnlTrai.Size = new System.Drawing.Size(200, 529);
            this.pnlTrai.TabIndex = 1;
            //
            // lblDanhSachCau
            //
            this.lblDanhSachCau.AutoSize = true;
            this.lblDanhSachCau.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDanhSachCau.Location = new System.Drawing.Point(10, 10);
            this.lblDanhSachCau.Name = "lblDanhSachCau";
            this.lblDanhSachCau.Size = new System.Drawing.Size(86, 15);
            this.lblDanhSachCau.TabIndex = 0;
            this.lblDanhSachCau.Text = "Danh sách câu";
            //
            // flpDanhSachCau
            //
            this.flpDanhSachCau.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpDanhSachCau.AutoScroll = true;
            this.flpDanhSachCau.Location = new System.Drawing.Point(8, 32);
            this.flpDanhSachCau.Name = "flpDanhSachCau";
            this.flpDanhSachCau.Size = new System.Drawing.Size(184, 440);
            this.flpDanhSachCau.TabIndex = 1;
            //
            // lblChuThich
            //
            this.lblChuThich.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblChuThich.ForeColor = System.Drawing.Color.DimGray;
            this.lblChuThich.Location = new System.Drawing.Point(8, 478);
            this.lblChuThich.Name = "lblChuThich";
            this.lblChuThich.Size = new System.Drawing.Size(184, 45);
            this.lblChuThich.TabIndex = 2;
            this.lblChuThich.Text = "Ô xanh: đã trả lời\r\nÔ trắng: chưa trả lời";
            //
            // pnlDuoi
            //
            this.pnlDuoi.Controls.Add(this.btnCauTruoc);
            this.pnlDuoi.Controls.Add(this.btnCauSau);
            this.pnlDuoi.Controls.Add(this.lblDaTraLoi);
            this.pnlDuoi.Controls.Add(this.btnNopBai);
            this.pnlDuoi.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlDuoi.Location = new System.Drawing.Point(200, 541);
            this.pnlDuoi.Name = "pnlDuoi";
            this.pnlDuoi.Size = new System.Drawing.Size(884, 60);
            this.pnlDuoi.TabIndex = 2;
            //
            // btnCauTruoc
            //
            this.btnCauTruoc.Location = new System.Drawing.Point(16, 12);
            this.btnCauTruoc.Name = "btnCauTruoc";
            this.btnCauTruoc.Size = new System.Drawing.Size(120, 36);
            this.btnCauTruoc.TabIndex = 0;
            this.btnCauTruoc.Text = "◀  Câu trước";
            this.btnCauTruoc.UseVisualStyleBackColor = true;
            this.btnCauTruoc.Click += new System.EventHandler(this.btnCauTruoc_Click);
            //
            // btnCauSau
            //
            this.btnCauSau.Location = new System.Drawing.Point(144, 12);
            this.btnCauSau.Name = "btnCauSau";
            this.btnCauSau.Size = new System.Drawing.Size(120, 36);
            this.btnCauSau.TabIndex = 1;
            this.btnCauSau.Text = "Câu sau  ▶";
            this.btnCauSau.UseVisualStyleBackColor = true;
            this.btnCauSau.Click += new System.EventHandler(this.btnCauSau_Click);
            //
            // lblDaTraLoi
            //
            this.lblDaTraLoi.AutoSize = true;
            this.lblDaTraLoi.Location = new System.Drawing.Point(282, 22);
            this.lblDaTraLoi.Name = "lblDaTraLoi";
            this.lblDaTraLoi.Size = new System.Drawing.Size(75, 15);
            this.lblDaTraLoi.TabIndex = 2;
            this.lblDaTraLoi.Text = "Đã trả lời: 0/0";
            //
            // btnNopBai
            //
            this.btnNopBai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNopBai.BackColor = System.Drawing.Color.FromArgb(198, 40, 40);
            this.btnNopBai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNopBai.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNopBai.ForeColor = System.Drawing.Color.White;
            this.btnNopBai.Location = new System.Drawing.Point(734, 12);
            this.btnNopBai.Name = "btnNopBai";
            this.btnNopBai.Size = new System.Drawing.Size(136, 36);
            this.btnNopBai.TabIndex = 3;
            this.btnNopBai.Text = "Nộp bài";
            this.btnNopBai.UseVisualStyleBackColor = false;
            this.btnNopBai.Click += new System.EventHandler(this.btnNopBai_Click);
            //
            // pnlGiua
            //
            this.pnlGiua.AutoScroll = true;
            this.pnlGiua.Controls.Add(this.lblSoCau);
            this.pnlGiua.Controls.Add(this.lblLoaiCau);
            this.pnlGiua.Controls.Add(this.txtNoiDung);
            this.pnlGiua.Controls.Add(this.flpPhuongAn);
            this.pnlGiua.Controls.Add(this.lblKetQuaCau);
            this.pnlGiua.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGiua.Location = new System.Drawing.Point(200, 72);
            this.pnlGiua.Name = "pnlGiua";
            this.pnlGiua.Size = new System.Drawing.Size(884, 469);
            this.pnlGiua.TabIndex = 0;
            //
            // lblSoCau
            //
            this.lblSoCau.AutoSize = true;
            this.lblSoCau.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSoCau.Location = new System.Drawing.Point(16, 14);
            this.lblSoCau.Name = "lblSoCau";
            this.lblSoCau.Size = new System.Drawing.Size(73, 21);
            this.lblSoCau.TabIndex = 0;
            this.lblSoCau.Text = "Câu 1/10";
            //
            // lblLoaiCau
            //
            this.lblLoaiCau.AutoSize = true;
            this.lblLoaiCau.ForeColor = System.Drawing.Color.DimGray;
            this.lblLoaiCau.Location = new System.Drawing.Point(18, 40);
            this.lblLoaiCau.Name = "lblLoaiCau";
            this.lblLoaiCau.Size = new System.Drawing.Size(61, 15);
            this.lblLoaiCau.TabIndex = 1;
            this.lblLoaiCau.Text = "Loại câu";
            //
            // txtNoiDung
            //
            this.txtNoiDung.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNoiDung.BackColor = System.Drawing.Color.White;
            this.txtNoiDung.Font = new System.Drawing.Font("Segoe UI", 11.5F);
            this.txtNoiDung.Location = new System.Drawing.Point(16, 66);
            this.txtNoiDung.Multiline = true;
            this.txtNoiDung.Name = "txtNoiDung";
            this.txtNoiDung.ReadOnly = true;
            this.txtNoiDung.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNoiDung.Size = new System.Drawing.Size(850, 96);
            this.txtNoiDung.TabIndex = 2;
            this.txtNoiDung.TabStop = false;
            //
            // flpPhuongAn
            //
            this.flpPhuongAn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpPhuongAn.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpPhuongAn.Location = new System.Drawing.Point(16, 172);
            this.flpPhuongAn.Name = "flpPhuongAn";
            this.flpPhuongAn.Size = new System.Drawing.Size(850, 220);
            this.flpPhuongAn.TabIndex = 3;
            this.flpPhuongAn.WrapContents = false;
            //
            // lblKetQuaCau
            //
            this.lblKetQuaCau.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblKetQuaCau.Font = new System.Drawing.Font("Segoe UI", 10.5F, System.Drawing.FontStyle.Bold);
            this.lblKetQuaCau.Location = new System.Drawing.Point(16, 400);
            this.lblKetQuaCau.Name = "lblKetQuaCau";
            this.lblKetQuaCau.Size = new System.Drawing.Size(850, 48);
            this.lblKetQuaCau.TabIndex = 4;
            //
            // timerDongHo
            //
            this.timerDongHo.Interval = 1000;
            this.timerDongHo.Tick += new System.EventHandler(this.timerDongHo_Tick);
            //
            // frmLamBai
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1084, 601);
            this.Controls.Add(this.pnlGiua);
            this.Controls.Add(this.pnlDuoi);
            this.Controls.Add(this.pnlTrai);
            this.Controls.Add(this.pnlTren);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(900, 560);
            this.Name = "frmLamBai";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Làm bài thi";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmLamBai_FormClosing);
            this.Load += new System.EventHandler(this.frmLamBai_Load);
            this.pnlTren.ResumeLayout(false);
            this.pnlTren.PerformLayout();
            this.pnlTrai.ResumeLayout(false);
            this.pnlTrai.PerformLayout();
            this.pnlDuoi.ResumeLayout(false);
            this.pnlDuoi.PerformLayout();
            this.pnlGiua.ResumeLayout(false);
            this.pnlGiua.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlTren;
        private System.Windows.Forms.Label lblTenDe;
        private System.Windows.Forms.Label lblThongTin;
        private System.Windows.Forms.Label lblDongHo;
        private System.Windows.Forms.Panel pnlTrai;
        private System.Windows.Forms.Label lblDanhSachCau;
        private System.Windows.Forms.FlowLayoutPanel flpDanhSachCau;
        private System.Windows.Forms.Label lblChuThich;
        private System.Windows.Forms.Panel pnlDuoi;
        private System.Windows.Forms.Button btnCauTruoc;
        private System.Windows.Forms.Button btnCauSau;
        private System.Windows.Forms.Label lblDaTraLoi;
        private System.Windows.Forms.Button btnNopBai;
        private System.Windows.Forms.Panel pnlGiua;
        private System.Windows.Forms.Label lblSoCau;
        private System.Windows.Forms.Label lblLoaiCau;
        private System.Windows.Forms.TextBox txtNoiDung;
        private System.Windows.Forms.FlowLayoutPanel flpPhuongAn;
        private System.Windows.Forms.Label lblKetQuaCau;
        private System.Windows.Forms.Timer timerDongHo;
    }
}
