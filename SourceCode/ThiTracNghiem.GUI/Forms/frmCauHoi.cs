using ThiTracNghiem.BLL;
using ThiTracNghiem.Models;

namespace ThiTracNghiem.GUI;

public partial class frmCauHoi : FormCoSo
{
    private readonly CauHoiService _cauHoiService = new();
    private readonly MonHocService _monHocService = new();
    private List<CauHoi> _danhSach = new();

    /// <summary>Mã câu hỏi đang sửa; 0 = đang thêm mới.</summary>
    private int _maDangChon;
    private bool _dangNapLuoi;
    private bool _dangCapNhatDapAn;

    public frmCauHoi()
    {
        InitializeComponent();
    }

    private CheckBox[] CacOChonDapAn => new[] { chkA, chkB, chkC, chkD };

    private LoaiCauHoi LoaiDangChon => cboLoai.LayGiaTri<LoaiCauHoi>() ?? LoaiCauHoi.MotDapAn;

    private void frmCauHoi_Load(object sender, EventArgs e)
    {
        CaiDatLuoi(dgvCauHoi);

        cboDoKhoLoc.NapDuLieu(MucChon.TuEnum<DoKho>(d => d.HienThi(), coMucTatCa: true));
        cboLoaiLoc.NapDuLieu(MucChon.TuEnum<LoaiCauHoi>(l => l.HienThi(), coMucTatCa: true));
        cboDoKho.NapDuLieu(MucChon.TuEnum<DoKho>(d => d.HienThi()));
        cboLoai.NapDuLieu(MucChon.TuEnum<LoaiCauHoi>(l => l.HienThi()));

        List<MonHoc> dsMon = new();
        if (ThucHien(() => dsMon = _monHocService.LayTatCa()))
        {
            cboMonHocLoc.NapDuLieu(MucChon.TuDanhSach(dsMon, m => m.MaMH, m => m.TenMH, coMucTatCa: true));
            cboMonHoc.NapDuLieu(MucChon.TuDanhSach(dsMon, m => m.MaMH, m => m.TenMH));
        }
    }

    private void frmCauHoi_Shown(object sender, EventArgs e) => TaiDuLieu();

    private void TaiDuLieu(int maCanChon = 0)
    {
        bool thanhCong = ThucHien(() => _danhSach = _cauHoiService.TimKiem(
            cboMonHocLoc.LayGiaTri<int>(),
            cboDoKhoLoc.LayGiaTri<DoKho>(),
            cboLoaiLoc.LayGiaTri<LoaiCauHoi>(),
            txtTuKhoa.Text,
            chkHienCauDaAn.Checked));

        if (!thanhCong)
            return;

        _dangNapLuoi = true;
        dgvCauHoi.DataSource = _danhSach.Select(ch => new
        {
            ch.MaCH,
            ch.TenMonHoc,
            ch.NoiDung,
            Loai = ch.Loai.HienThi(),
            DoKho = ch.DoKho.HienThi(),
            ch.DapAnDung,
            TrangThai = ch.DangSuDung ? "Đang dùng" : "Đã ẩn"
        }).ToList();

        DatCot(dgvCauHoi, "MaCH", "Mã", 12);
        DatCot(dgvCauHoi, "TenMonHoc", "Môn học", 40);
        DatCot(dgvCauHoi, "NoiDung", "Nội dung", 110);
        DatCot(dgvCauHoi, "Loai", "Loại", 30);
        DatCot(dgvCauHoi, "DoKho", "Độ khó", 25);
        DatCot(dgvCauHoi, "DapAnDung", "Đáp án", 18);
        DatCot(dgvCauHoi, "TrangThai", "Trạng thái", 25);
        ChonDong(dgvCauHoi, "MaCH", maCanChon);
        _dangNapLuoi = false;

        if (maCanChon == 0)
            LamMoiForm();

        Text = $"Ngân hàng câu hỏi ({_danhSach.Count} câu)";
    }

    private void dgvCauHoi_SelectionChanged(object sender, EventArgs e)
    {
        if (_dangNapLuoi || LayMaDongChon(dgvCauHoi, "MaCH") is not int ma)
            return;

        var cauHoi = _danhSach.First(ch => ch.MaCH == ma);
        _maDangChon = ma;

        cboMonHoc.ChonGiaTri(cauHoi.MaMH);
        cboLoai.ChonGiaTri(cauHoi.Loai);      // gọi CapNhatTheoLoai qua sự kiện
        cboDoKho.ChonGiaTri(cauHoi.DoKho);
        txtNoiDung.Text = cauHoi.NoiDung;
        txtA.Text = cauHoi.PhuongAnA;
        txtB.Text = cauHoi.PhuongAnB;
        txtC.Text = cauHoi.PhuongAnC;
        txtD.Text = cauHoi.PhuongAnD;
        DatDapAn(cauHoi.DapAnDung);
        chkDangSuDung.Checked = cauHoi.DangSuDung;
        lblCheDo.Text = $"Đang sửa câu hỏi mã {ma}";
    }

    private void LamMoiForm()
    {
        _maDangChon = 0;
        txtNoiDung.Clear();
        txtA.Clear();
        txtB.Clear();
        txtC.Clear();
        txtD.Clear();
        DatDapAn(string.Empty);
        chkDangSuDung.Checked = true;
        CapNhatTheoLoai();
        lblCheDo.Text = "Đang thêm câu hỏi mới";
        dgvCauHoi.ClearSelection();
        txtNoiDung.Focus();
    }

    /// <summary>
    /// Thay đổi giao diện theo loại câu hỏi. Dùng thuộc tính đa hình ChoPhepChonNhieu
    /// của đối tượng câu hỏi tương ứng thay vì so sánh loại bằng if/else.
    /// </summary>
    private void CapNhatTheoLoai()
    {
        var mau = _cauHoiService.TaoMoi(LoaiDangChon);
        bool laDungSai = mau.LayCacPhuongAn().Count == 2;

        if (laDungSai)
        {
            txtA.Text = mau.PhuongAnA;
            txtB.Text = mau.PhuongAnB;
            txtC.Clear();
            txtD.Clear();
            chkC.Checked = chkD.Checked = false;
        }
        else if (txtA.ReadOnly)
        {
            // Vừa đổi từ Đúng/Sai sang loại khác: xóa chữ "Đúng"/"Sai" có sẵn
            txtA.Clear();
            txtB.Clear();
        }

        txtA.ReadOnly = txtB.ReadOnly = laDungSai;
        txtC.Enabled = txtD.Enabled = chkC.Enabled = chkD.Enabled = !laDungSai;

        if (!mau.ChoPhepChonNhieu)
            GiuLaiMotDapAn(CacOChonDapAn.FirstOrDefault(o => o.Checked));

        lblGoiYLoai.Text = LoaiDangChon switch
        {
            LoaiCauHoi.MotDapAn => "Một đáp án: nhập đủ 4 phương án và đánh dấu đúng 1 phương án đúng.",
            LoaiCauHoi.NhieuDapAn => "Nhiều đáp án: nhập đủ 4 phương án và đánh dấu từ 2 phương án đúng trở lên. " +
                                     "Sinh viên phải chọn đủ và đúng mới được tính điểm.",
            _ => "Đúng/Sai: A = Đúng, B = Sai. Đánh dấu A nếu phát biểu đúng, B nếu phát biểu sai."
        };
    }

    private void cboLoai_SelectedIndexChanged(object sender, EventArgs e) => CapNhatTheoLoai();

    private void chkDapAn_CheckedChanged(object sender, EventArgs e)
    {
        if (_dangCapNhatDapAn || sender is not CheckBox oChon || !oChon.Checked)
            return;

        if (!_cauHoiService.TaoMoi(LoaiDangChon).ChoPhepChonNhieu)
            GiuLaiMotDapAn(oChon);
    }

    /// <summary>Loại câu chỉ có một đáp án: bỏ đánh dấu các ô khác.</summary>
    private void GiuLaiMotDapAn(CheckBox? oGiuLai)
    {
        _dangCapNhatDapAn = true;
        foreach (var o in CacOChonDapAn)
            o.Checked = o == oGiuLai;
        _dangCapNhatDapAn = false;
    }

    private void DatDapAn(string dapAn)
    {
        _dangCapNhatDapAn = true;
        foreach (var o in CacOChonDapAn)
            o.Checked = dapAn.Contains((string)o.Tag!);
        _dangCapNhatDapAn = false;
    }

    private string LayDapAn() =>
        string.Concat(CacOChonDapAn.Where(o => o.Checked).Select(o => (string)o.Tag!));

    private void btnTim_Click(object sender, EventArgs e) => TaiDuLieu();

    private void btnThemMoi_Click(object sender, EventArgs e) => LamMoiForm();

    private void btnLamMoi_Click(object sender, EventArgs e)
    {
        cboMonHocLoc.SelectedIndex = 0;
        cboDoKhoLoc.SelectedIndex = 0;
        cboLoaiLoc.SelectedIndex = 0;
        txtTuKhoa.Clear();
        chkHienCauDaAn.Checked = false;
        TaiDuLieu();
    }

    private void btnLuu_Click(object sender, EventArgs e)
    {
        // Factory tạo đúng lớp con theo loại câu hỏi đang chọn
        var cauHoi = _cauHoiService.TaoMoi(LoaiDangChon);
        cauHoi.MaCH = _maDangChon;
        cauHoi.MaMH = cboMonHoc.LayGiaTri<int>() ?? 0;
        cauHoi.DoKho = cboDoKho.LayGiaTri<DoKho>() ?? DoKho.De;
        cauHoi.NoiDung = txtNoiDung.Text;
        cauHoi.PhuongAnA = txtA.Text;
        cauHoi.PhuongAnB = txtB.Text;
        cauHoi.PhuongAnC = txtC.Text;
        cauHoi.PhuongAnD = txtD.Text;
        cauHoi.DapAnDung = LayDapAn();
        cauHoi.DangSuDung = chkDangSuDung.Checked;

        bool laThemMoi = _maDangChon == 0;
        bool thanhCong = ThucHien(() =>
        {
            if (laThemMoi)
                _cauHoiService.Them(cauHoi);
            else
                _cauHoiService.CapNhat(cauHoi);
        });

        if (!thanhCong)
            return;

        HienThongBao(laThemMoi ? "Đã thêm câu hỏi." : "Đã cập nhật câu hỏi.");
        TaiDuLieu(cauHoi.MaCH);
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        if (_maDangChon == 0)
        {
            HienCanhBao("Hãy chọn câu hỏi cần xóa trong danh sách.");
            return;
        }

        if (!XacNhan($"Xóa câu hỏi mã {_maDangChon}?"))
            return;

        var ketQua = KetQuaXoaCauHoi.DaXoa;
        if (!ThucHien(() => ketQua = _cauHoiService.Xoa(_maDangChon)))
            return;

        HienThongBao(ketQua == KetQuaXoaCauHoi.DaXoa
            ? "Đã xóa câu hỏi."
            : "Câu hỏi đã được dùng trong bài thi nên chỉ được ẩn đi (không xóa hẳn) để giữ lịch sử bài làm.");
        TaiDuLieu();
    }
}
