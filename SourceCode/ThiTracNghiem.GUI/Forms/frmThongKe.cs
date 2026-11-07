using System.Data;
using ThiTracNghiem.BLL;
using ThiTracNghiem.Models;

namespace ThiTracNghiem.GUI;

/// <summary>Màn hình giảng viên: tra cứu kết quả và các báo cáo thống kê.</summary>
public partial class frmThongKe : FormCoSo
{
    private readonly ThongKeService _thongKeService = new();
    private readonly ThiService _thiService = new();
    private readonly DeThiService _deThiService = new();
    private readonly MonHocService _monHocService = new();
    private readonly TaiKhoanService _taiKhoanService = new();

    private bool _dangNapThongKeDe;

    public frmThongKe()
    {
        InitializeComponent();
    }

    private void frmThongKe_Load(object sender, EventArgs e)
    {
        foreach (var luoi in new[] { dgvKetQua, dgvThongKeDe, dgvCauSai, dgvChuaThi })
            CaiDatLuoi(luoi);

        NapBoLoc();
    }

    private void frmThongKe_Shown(object sender, EventArgs e) => TimKetQua();

    private void NapBoLoc()
    {
        List<DeThi> dsDe = new();
        List<MonHoc> dsMon = new();
        List<string> dsLop = new();

        bool thanhCong = ThucHien(() =>
        {
            dsDe = _deThiService.TimKiem(null, null);
            dsMon = _monHocService.LayTatCa();
            dsLop = _taiKhoanService.LayDanhSachLop();
        });
        if (!thanhCong)
            return;

        cboDeThi.NapDuLieu(MucChon.TuDanhSach(dsDe, d => d.MaDe, d => d.TenDe, coMucTatCa: true));
        cboDeChuaThi.NapDuLieu(MucChon.TuDanhSach(dsDe, d => d.MaDe, d => d.TenDe));
        cboMonHocCauSai.NapDuLieu(MucChon.TuDanhSach(dsMon, m => m.MaMH, m => m.TenMH, coMucTatCa: true));
        cboLop.NapDuLieu(MucChon.TuDanhSach(dsLop, l => l, l => l, coMucTatCa: true));
        cboLopChuaThi.NapDuLieu(MucChon.TuDanhSach(dsLop, l => l, l => l, coMucTatCa: true));
    }

    private void tabChinh_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (tabChinh.SelectedTab == tabTheoDe)
            TaiThongKeTheoDe();
        else if (tabChinh.SelectedTab == tabCauSai)
            TaiCauSaiNhieu();
        else if (tabChinh.SelectedTab == tabChuaThi)
            TaiSinhVienChuaThi();
    }

    // ===================== Tra cứu kết quả =====================

    private void TimKetQua()
    {
        DataTable ketQua = new();
        bool thanhCong = ThucHien(() => ketQua = _thongKeService.TimKiemKetQua(
            cboDeThi.LayGiaTri<int>(),
            cboLop.GiaTriChon() as string,
            chkLocDiem.Checked ? nudDiemTu.Value : null,
            chkLocDiem.Checked ? nudDiemDen.Value : null,
            dtpTuNgay.Checked ? dtpTuNgay.Value.Date : null,
            dtpDenNgay.Checked ? dtpDenNgay.Value.Date : null));

        if (!thanhCong)
            return;

        dgvKetQua.DataSource = ketQua;
        AnCot(dgvKetQua, "MaLT", "MaDe", "MaTK", "ThoiGianBatDau");
        DatCot(dgvKetQua, "TenDe", "Đề thi", 85);
        DatCot(dgvKetQua, "TenMH", "Môn học", 55);
        DatCot(dgvKetQua, "MaSV", "MSSV", 28);
        DatCot(dgvKetQua, "HoTen", "Họ tên", 50);
        DatCot(dgvKetQua, "Lop", "Lớp", 22);
        DatCot(dgvKetQua, "ThoiGianNop", "Nộp lúc", 38, "dd/MM/yyyy HH:mm");
        DatCot(dgvKetQua, "SoPhutLam", "Số phút", 20);
        DatCot(dgvKetQua, "SoCauDung", "Câu đúng", 20);
        DatCot(dgvKetQua, "TongSoCau", "Tổng câu", 20);
        DatCot(dgvKetQua, "Diem", "Điểm", 18, "0.00");
        DatCot(dgvKetQua, "KetQua", "Kết quả", 22);

        var cacDiem = ketQua.AsEnumerable().Select(r => r.Field<decimal>("Diem")).ToList();
        lblTongKetKetQua.Text = cacDiem.Count == 0
            ? "Không có kết quả phù hợp."
            : $"Tìm thấy {cacDiem.Count} lượt thi  •  Điểm trung bình: {cacDiem.Average():0.00}  •  " +
              $"Đạt (≥ 5): {cacDiem.Count(d => d >= 5)}/{cacDiem.Count}";
    }

    private void btnTimKetQua_Click(object sender, EventArgs e) => TimKetQua();

    private void XemBaiLam()
    {
        if (LayMaDongChon(dgvKetQua, "MaLT") is not int maLT)
        {
            HienCanhBao("Hãy chọn một lượt thi trong danh sách.");
            return;
        }

        BaiThi? baiThi = null;
        if (ThucHien(() => baiThi = _thiService.XemLaiBai(maLT)))
        {
            using var form = new frmLamBai(baiThi!, cheDoXemLai: true);
            form.ShowDialog(this);
        }
    }

    private void btnXemBaiLam_Click(object sender, EventArgs e) => XemBaiLam();

    private void dgvKetQua_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
            XemBaiLam();
    }

    // ===================== Thống kê theo đề + phổ điểm =====================

    private void TaiThongKeTheoDe()
    {
        DataTable bang = new();
        if (!ThucHien(() => bang = _thongKeService.ThongKeDiemTheoDe()))
            return;

        _dangNapThongKeDe = true;
        dgvThongKeDe.DataSource = bang;
        AnCot(dgvThongKeDe, "MaDe");
        DatCot(dgvThongKeDe, "TenDe", "Đề thi", 90);
        DatCot(dgvThongKeDe, "TenMH", "Môn học", 55);
        DatCot(dgvThongKeDe, "SoLuotThi", "Số lượt thi", 22);
        DatCot(dgvThongKeDe, "SoSinhVien", "Số SV", 18);
        DatCot(dgvThongKeDe, "DiemTrungBinh", "Điểm TB", 20, "0.00");
        DatCot(dgvThongKeDe, "DiemCaoNhat", "Cao nhất", 20, "0.00");
        DatCot(dgvThongKeDe, "DiemThapNhat", "Thấp nhất", 20, "0.00");
        DatCot(dgvThongKeDe, "SoLuotDat", "Lượt đạt", 20);
        DatCot(dgvThongKeDe, "TiLeDat", "Tỉ lệ đạt (%)", 22, "0.00");
        _dangNapThongKeDe = false;

        VePhoDiem();
    }

    private void dgvThongKeDe_SelectionChanged(object sender, EventArgs e)
    {
        if (!_dangNapThongKeDe)
            VePhoDiem();
    }

    private void VePhoDiem()
    {
        if (LayMaDongChon(dgvThongKeDe, "MaDe") is not int maDe)
        {
            bieuDoPhoDiem.TieuDe = "Phổ điểm";
            bieuDoPhoDiem.DatDuLieu(Array.Empty<(string, double)>());
            return;
        }

        DataTable phoDiem = new();
        if (!ThucHien(() => phoDiem = _thongKeService.PhoDiem(maDe)))
            return;

        string tenDe = dgvThongKeDe.SelectedRows[0].Cells["TenDe"].Value?.ToString() ?? string.Empty;
        bieuDoPhoDiem.TieuDe = $"Phổ điểm - {tenDe} (số lượt thi theo khoảng điểm)";
        bieuDoPhoDiem.DatDuLieu(phoDiem.AsEnumerable()
            .Select(r => (r.Field<string>("KhoangDiem")!, (double)r.Field<int>("SoLuot"))));
    }

    private void btnTaiThongKe_Click(object sender, EventArgs e) => TaiThongKeTheoDe();

    // ===================== Câu hỏi sai nhiều =====================

    private void TaiCauSaiNhieu()
    {
        DataTable bang = new();
        if (!ThucHien(() => bang = _thongKeService.TopCauHoiSaiNhieu(cboMonHocCauSai.LayGiaTri<int>(), (int)nudSoLuong.Value)))
            return;

        dgvCauSai.DataSource = bang;
        DatCot(dgvCauSai, "MaCH", "Mã câu", 15);
        DatCot(dgvCauSai, "TenMH", "Môn học", 45);
        DatCot(dgvCauSai, "NoiDung", "Nội dung câu hỏi", 140);
        DatCot(dgvCauSai, "DoKho", "Độ khó", 20);
        DatCot(dgvCauSai, "SoLanXuatHien", "Số lần xuất hiện", 25);
        DatCot(dgvCauSai, "SoLanSai", "Số lần sai", 20);
        DatCot(dgvCauSai, "TiLeSai", "Tỉ lệ sai (%)", 22, "0.00");
    }

    private void btnXemCauSai_Click(object sender, EventArgs e) => TaiCauSaiNhieu();

    // ===================== Sinh viên chưa thi =====================

    private void TaiSinhVienChuaThi()
    {
        if (cboDeChuaThi.LayGiaTri<int>() is not int maDe)
            return;

        DataTable bang = new();
        if (!ThucHien(() => bang = _thongKeService.SinhVienChuaThi(maDe, cboLopChuaThi.GiaTriChon() as string)))
            return;

        dgvChuaThi.DataSource = bang;
        AnCot(dgvChuaThi, "MaTK");
        DatCot(dgvChuaThi, "MaSV", "MSSV", 25);
        DatCot(dgvChuaThi, "HoTen", "Họ tên", 50);
        DatCot(dgvChuaThi, "Lop", "Lớp", 20);
        DatCot(dgvChuaThi, "Email", "Email", 60);
        lblTongChuaThi.Text = $"Có {bang.Rows.Count} sinh viên chưa nộp bài đề này.";
    }

    private void btnXemChuaThi_Click(object sender, EventArgs e) => TaiSinhVienChuaThi();
}
