namespace ThiTracNghiem.GUI
{
    partial class frmPhongThi
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
            this.tabDeThi = new System.Windows.Forms.TabPage();
            this.lblHuongDan = new System.Windows.Forms.Label();
            this.dgvDeThi = new System.Windows.Forms.DataGridView();
            this.btnVaoThi = new System.Windows.Forms.Button();
            this.btnLamMoiDe = new System.Windows.Forms.Button();
            this.tabLichSu = new System.Windows.Forms.TabPage();
            this.dgvLichSu = new System.Windows.Forms.DataGridView();
            this.btnXemLai = new System.Windows.Forms.Button();
            this.lblTongKet = new System.Windows.Forms.Label();
            this.tabChinh.SuspendLayout();
            this.tabDeThi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeThi)).BeginInit();
            this.tabLichSu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).BeginInit();
            this.SuspendLayout();
            //
            // tabChinh
            //
            this.tabChinh.Controls.Add(this.tabDeThi);
            this.tabChinh.Controls.Add(this.tabLichSu);
            this.tabChinh.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabChinh.Location = new System.Drawing.Point(0, 0);
            this.tabChinh.Name = "tabChinh";
            this.tabChinh.SelectedIndex = 0;
            this.tabChinh.Size = new System.Drawing.Size(1004, 581);
            this.tabChinh.TabIndex = 0;
            this.tabChinh.SelectedIndexChanged += new System.EventHandler(this.tabChinh_SelectedIndexChanged);
            //
            // tabDeThi
            //
            this.tabDeThi.Controls.Add(this.lblHuongDan);
            this.tabDeThi.Controls.Add(this.dgvDeThi);
            this.tabDeThi.Controls.Add(this.btnVaoThi);
            this.tabDeThi.Controls.Add(this.btnLamMoiDe);
            this.tabDeThi.Location = new System.Drawing.Point(4, 24);
            this.tabDeThi.Name = "tabDeThi";
            this.tabDeThi.Padding = new System.Windows.Forms.Padding(3);
            this.tabDeThi.Size = new System.Drawing.Size(996, 553);
            this.tabDeThi.TabIndex = 0;
            this.tabDeThi.Text = "Đề thi";
            this.tabDeThi.UseVisualStyleBackColor = true;
            //
            // lblHuongDan
            //
            this.lblHuongDan.AutoSize = true;
            this.lblHuongDan.Location = new System.Drawing.Point(10, 12);
            this.lblHuongDan.Name = "lblHuongDan";
            this.lblHuongDan.Size = new System.Drawing.Size(420, 15);
            this.lblHuongDan.TabIndex = 0;
            this.lblHuongDan.Text = "Chọn đề thi rồi bấm \"Vào thi\" (hoặc nhấp đúp vào dòng). Đề màu xám hiện không thể vào thi.";
            //
            // dgvDeThi
            //
            this.dgvDeThi.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvDeThi.Location = new System.Drawing.Point(10, 36);
            this.dgvDeThi.Name = "dgvDeThi";
            this.dgvDeThi.Size = new System.Drawing.Size(976, 460);
            this.dgvDeThi.TabIndex = 1;
            this.dgvDeThi.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDeThi_CellDoubleClick);
            //
            // btnVaoThi
            //
            this.btnVaoThi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnVaoThi.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnVaoThi.Location = new System.Drawing.Point(10, 507);
            this.btnVaoThi.Name = "btnVaoThi";
            this.btnVaoThi.Size = new System.Drawing.Size(130, 36);
            this.btnVaoThi.TabIndex = 2;
            this.btnVaoThi.Text = "Vào thi";
            this.btnVaoThi.UseVisualStyleBackColor = true;
            this.btnVaoThi.Click += new System.EventHandler(this.btnVaoThi_Click);
            //
            // btnLamMoiDe
            //
            this.btnLamMoiDe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLamMoiDe.Location = new System.Drawing.Point(150, 507);
            this.btnLamMoiDe.Name = "btnLamMoiDe";
            this.btnLamMoiDe.Size = new System.Drawing.Size(110, 36);
            this.btnLamMoiDe.TabIndex = 3;
            this.btnLamMoiDe.Text = "Làm mới";
            this.btnLamMoiDe.UseVisualStyleBackColor = true;
            this.btnLamMoiDe.Click += new System.EventHandler(this.btnLamMoiDe_Click);
            //
            // tabLichSu
            //
            this.tabLichSu.Controls.Add(this.dgvLichSu);
            this.tabLichSu.Controls.Add(this.btnXemLai);
            this.tabLichSu.Controls.Add(this.lblTongKet);
            this.tabLichSu.Location = new System.Drawing.Point(4, 24);
            this.tabLichSu.Name = "tabLichSu";
            this.tabLichSu.Padding = new System.Windows.Forms.Padding(3);
            this.tabLichSu.Size = new System.Drawing.Size(996, 553);
            this.tabLichSu.TabIndex = 1;
            this.tabLichSu.Text = "Lịch sử thi";
            this.tabLichSu.UseVisualStyleBackColor = true;
            //
            // dgvLichSu
            //
            this.dgvLichSu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvLichSu.Location = new System.Drawing.Point(10, 12);
            this.dgvLichSu.Name = "dgvLichSu";
            this.dgvLichSu.Size = new System.Drawing.Size(976, 484);
            this.dgvLichSu.TabIndex = 0;
            this.dgvLichSu.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvLichSu_CellDoubleClick);
            //
            // btnXemLai
            //
            this.btnXemLai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnXemLai.Location = new System.Drawing.Point(10, 507);
            this.btnXemLai.Name = "btnXemLai";
            this.btnXemLai.Size = new System.Drawing.Size(130, 36);
            this.btnXemLai.TabIndex = 1;
            this.btnXemLai.Text = "Xem lại bài làm";
            this.btnXemLai.UseVisualStyleBackColor = true;
            this.btnXemLai.Click += new System.EventHandler(this.btnXemLai_Click);
            //
            // lblTongKet
            //
            this.lblTongKet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTongKet.AutoSize = true;
            this.lblTongKet.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTongKet.Location = new System.Drawing.Point(155, 517);
            this.lblTongKet.Name = "lblTongKet";
            this.lblTongKet.Size = new System.Drawing.Size(0, 15);
            this.lblTongKet.TabIndex = 2;
            //
            // frmPhongThi
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1004, 581);
            this.Controls.Add(this.tabChinh);
            this.Name = "frmPhongThi";
            this.Text = "Phòng thi";
            this.Load += new System.EventHandler(this.frmPhongThi_Load);
            this.Shown += new System.EventHandler(this.frmPhongThi_Shown);
            this.tabChinh.ResumeLayout(false);
            this.tabDeThi.ResumeLayout(false);
            this.tabDeThi.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDeThi)).EndInit();
            this.tabLichSu.ResumeLayout(false);
            this.tabLichSu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLichSu)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TabControl tabChinh;
        private System.Windows.Forms.TabPage tabDeThi;
        private System.Windows.Forms.Label lblHuongDan;
        private System.Windows.Forms.DataGridView dgvDeThi;
        private System.Windows.Forms.Button btnVaoThi;
        private System.Windows.Forms.Button btnLamMoiDe;
        private System.Windows.Forms.TabPage tabLichSu;
        private System.Windows.Forms.DataGridView dgvLichSu;
        private System.Windows.Forms.Button btnXemLai;
        private System.Windows.Forms.Label lblTongKet;
    }
}
