using System.Data;
using ThiTracNghiem.BLL;

namespace ThiTracNghiem.GUI;

/// <summary>Màn hình của sinh viên: danh sách đề thi và lịch sử các bài đã làm.</summary>
public partial class frmPhongThi : FormCoSo
{
    private readonly ThiService _thiService = new();
    private List<DeThiChoSinhVien> _danhSachDe = new();

    public frmPhongThi()
    {
        InitializeComponent();
    }

    private void frmPhongThi_Load(object sender, EventArgs e)
    {
        CaiDatLuoi(dgvDeThi);
        CaiDatLuoi(dgvLichSu);
    }

    private void frmPhongThi_Shown(object sender, EventArgs e) => TaiDanhSachDe();

    // ===================== Đề thi =====================

    private void TaiDanhSachDe()
    {
        if (!ThucHien(() => _danhSachDe = _thiService.LayDanhSachDeThi()))
            return;

        dgvDeThi.DataSource = _danhSachDe.Select(d => new
        {
            d.DeThi.MaDe,
            d.DeThi.TenDe,
            d.DeThi.TenMonHoc,
            d.DeThi.TongSoCau,
            d.DeThi.ThoiGianLam,
            d.DeThi.ThoiGianMo,
            d.DeThi.ThoiGianDong,
            d.TrangThai,
            SoLanThi = d.SoLanThiHienThi,
            GhiChu = d.CoBaiDangLam ? "Đang làm dở - vào để làm tiếp" : d.CoTheVaoThi ? "Có thể vào thi" : string.Empty
        }).ToList();

        AnCot(dgvDeThi, "MaDe");
        DatCot(dgvDeThi, "TenDe", "Tên đề thi", 90);
        DatCot(dgvDeThi, "TenMonHoc", "Môn học", 55);
        DatCot(dgvDeThi, "TongSoCau", "Số câu", 18);
        DatCot(dgvDeThi, "ThoiGianLam", "Phút", 15);
        DatCot(dgvDeThi, "ThoiGianMo", "Mở lúc", 38, "dd/MM/yyyy HH:mm");
        DatCot(dgvDeThi, "ThoiGianDong", "Đóng lúc", 38, "dd/MM/yyyy HH:mm");
        DatCot(dgvDeThi, "TrangThai", "Trạng thái", 25);
        DatCot(dgvDeThi, "SoLanThi", "Đã thi", 18);
        DatCot(dgvDeThi, "GhiChu", "Ghi chú", 55);

        // Đề không thể vào thi hiển thị màu xám
        for (int i = 0; i < _danhSachDe.Count; i++)
        {
            if (!_danhSachDe[i].CoTheVaoThi)
                dgvDeThi.Rows[i].DefaultCellStyle.ForeColor = Color.Gray;
        }
    }

    private void VaoThi()
    {
        if (LayMaDongChon(dgvDeThi, "MaDe") is not int maDe)
        {
            HienCanhBao("Hãy chọn một đề thi.");
            return;
        }

        var muc = _danhSachDe.First(d => d.DeThi.MaDe == maDe);
        var de = muc.DeThi;

        string cauHoi = muc.CoBaiDangLam
            ? $"Bạn đang làm dở đề \"{de.TenDe}\".\nTiếp tục làm bài?"
            : $"Đề: {de.TenDe}\nSố câu: {de.TongSoCau} - Thời gian: {de.ThoiGianLam} phút\n" +
              $"Lần thi: {muc.SoLanDaThi + 1}/{de.SoLanThiToiDa}\n\n" +
              "Thời gian bắt đầu tính ngay khi vào thi. Bắt đầu làm bài?";

        if (!XacNhan(cauHoi))
            return;

        BaiThi? baiThi = null;
        if (ThucHien(() => baiThi = _thiService.BatDauThi(maDe)))
        {
            using var formLamBai = new frmLamBai(baiThi!, cheDoXemLai: false);
            formLamBai.ShowDialog(this);
        }

        TaiDanhSachDe();
    }

    private void btnVaoThi_Click(object sender, EventArgs e) => VaoThi();

    private void dgvDeThi_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
            VaoThi();
    }

    private void btnLamMoiDe_Click(object sender, EventArgs e) => TaiDanhSachDe();

    // ===================== Lịch sử thi =====================

    private void tabChinh_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (tabChinh.SelectedTab == tabLichSu)
            TaiLichSu();
        else
            TaiDanhSachDe();
    }

    private void TaiLichSu()
    {
        DataTable lichSu = new();
        if (!ThucHien(() => lichSu = _thiService.LayLichSuThi()))
            return;

        dgvLichSu.DataSource = lichSu;
        AnCot(dgvLichSu, "MaLT", "MaDe", "MaTK", "MaSV", "HoTen", "Lop", "ThoiGianBatDau");
        DatCot(dgvLichSu, "TenDe", "Đề thi", 90);
        DatCot(dgvLichSu, "TenMH", "Môn học", 55);
        DatCot(dgvLichSu, "ThoiGianNop", "Nộp lúc", 40, "dd/MM/yyyy HH:mm");
        DatCot(dgvLichSu, "SoPhutLam", "Số phút làm", 22);
        DatCot(dgvLichSu, "SoCauDung", "Câu đúng", 20);
        DatCot(dgvLichSu, "TongSoCau", "Tổng câu", 20);
        DatCot(dgvLichSu, "Diem", "Điểm", 18, "0.00");
        DatCot(dgvLichSu, "KetQua", "Kết quả", 22);

        lblTongKet.Text = lichSu.Rows.Count == 0
            ? "Bạn chưa có bài thi nào."
            : $"Số bài đã làm: {lichSu.Rows.Count} - Điểm trung bình: " +
              $"{lichSu.AsEnumerable().Average(r => r.Field<decimal>("Diem")):0.00}";
    }

    private void XemLaiBai()
    {
        if (LayMaDongChon(dgvLichSu, "MaLT") is not int maLT)
        {
            HienCanhBao("Hãy chọn một bài thi trong danh sách.");
            return;
        }

        BaiThi? baiThi = null;
        if (ThucHien(() => baiThi = _thiService.XemLaiBai(maLT)))
        {
            using var formXemLai = new frmLamBai(baiThi!, cheDoXemLai: true);
            formXemLai.ShowDialog(this);
        }
    }

    private void btnXemLai_Click(object sender, EventArgs e) => XemLaiBai();

    private void dgvLichSu_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex >= 0)
            XemLaiBai();
    }
}
