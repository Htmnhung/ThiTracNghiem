using ThiTracNghiem.DAL;
using ThiTracNghiem.Models;

namespace ThiTracNghiem.BLL;

/// <summary>Đăng nhập, đổi mật khẩu và quản lý tài khoản (giảng viên quản lý).</summary>
public class TaiKhoanService : ServiceBase
{
    private readonly TaiKhoanDAL _taiKhoanDal = new();

    public bool KiemTraKetNoi() => DbHelper.KiemTraKetNoi();

    // ===================== Đăng nhập =====================

    public TaiKhoan DangNhap(string? tenDangNhap, string? matKhau) =>
        ThucHien(() =>
        {
            var loi = new List<string>();
            if (string.IsNullOrWhiteSpace(tenDangNhap))
                loi.Add("Vui lòng nhập tên đăng nhập.");
            if (string.IsNullOrEmpty(matKhau))
                loi.Add("Vui lòng nhập mật khẩu.");
            BaoLoiNeuCo(loi);

            var taiKhoan = _taiKhoanDal.LayTheoTenDangNhap(tenDangNhap!.Trim());

            // Không cho biết cụ thể sai tên đăng nhập hay sai mật khẩu
            if (taiKhoan == null || !MatKhauHelper.KiemTra(matKhau!, taiKhoan.MatKhauHash))
                throw new NghiepVuException("Tên đăng nhập hoặc mật khẩu không đúng.");

            if (!taiKhoan.DangHoatDong)
                throw new NghiepVuException("Tài khoản đã bị khóa. Vui lòng liên hệ giảng viên.");

            PhienDangNhap.BatDau(taiKhoan);
            return taiKhoan;
        });

    public void DangXuat() => PhienDangNhap.KetThuc();

    public void DoiMatKhau(string? matKhauCu, string? matKhauMoi, string? xacNhanMatKhau) =>
        ThucHien(() =>
        {
            var nguoiDung = YeuCauDangNhap();
            var loi = new List<string>();

            if (string.IsNullOrEmpty(matKhauCu) || !MatKhauHelper.KiemTra(matKhauCu, nguoiDung.MatKhauHash))
                loi.Add("Mật khẩu hiện tại không đúng.");

            if (MatKhauHelper.KiemTraDoManh(matKhauMoi) is string loiMatKhau)
                loi.Add(loiMatKhau);
            else if (matKhauMoi == matKhauCu)
                loi.Add("Mật khẩu mới phải khác mật khẩu hiện tại.");

            if (matKhauMoi != xacNhanMatKhau)
                loi.Add("Xác nhận mật khẩu không khớp.");

            BaoLoiNeuCo(loi);

            var hash = MatKhauHelper.BamMatKhau(matKhauMoi!);
            _taiKhoanDal.DoiMatKhau(nguoiDung.MaTK, hash);
            nguoiDung.MatKhauHash = hash;
        });

    // ===================== Quản lý tài khoản (giảng viên) =====================

    public List<TaiKhoan> TimKiem(VaiTro? vaiTro, string? lop, string? tuKhoa) =>
        ThucHien(() =>
        {
            YeuCauGiangVien();
            return _taiKhoanDal.TimKiem(vaiTro, string.IsNullOrWhiteSpace(lop) ? null : lop.Trim(), tuKhoa);
        });

    public List<string> LayDanhSachLop() => ThucHien(() => _taiKhoanDal.LayDanhSachLop());

    public TaiKhoan? LayTheoMa(int maTK) =>
        ThucHien(() =>
        {
            YeuCauGiangVien();
            return _taiKhoanDal.LayTheoMa(maTK);
        });

    public int Them(TaiKhoan taiKhoan, string? matKhau) =>
        ThucHien(() =>
        {
            YeuCauGiangVien();

            var loi = taiKhoan.KiemTraHopLe();
            if (MatKhauHelper.KiemTraDoManh(matKhau) is string loiMatKhau)
                loi.Add(loiMatKhau);
            KiemTraTrung(taiKhoan, loi);
            BaoLoiNeuCo(loi);

            taiKhoan.MatKhauHash = MatKhauHelper.BamMatKhau(matKhau!);
            taiKhoan.MaTK = _taiKhoanDal.Them(taiKhoan);
            return taiKhoan.MaTK;
        });

    public void CapNhat(TaiKhoan taiKhoan) =>
        ThucHien(() =>
        {
            var nguoiDung = YeuCauGiangVien();
            var cu = BatBuocTonTai(_taiKhoanDal.LayTheoMa(taiKhoan.MaTK), "Tài khoản không tồn tại hoặc đã bị xóa.");

            var loi = taiKhoan.KiemTraHopLe();
            KiemTraTrung(taiKhoan, loi);

            if (cu.MaTK == nguoiDung.MaTK)
            {
                if (taiKhoan.VaiTro != cu.VaiTro)
                    loi.Add("Không thể tự thay đổi vai trò của chính mình.");
                if (!taiKhoan.DangHoatDong)
                    loi.Add("Không thể tự khóa tài khoản của chính mình.");
            }
            BaoLoiNeuCo(loi);

            _taiKhoanDal.CapNhat(taiKhoan);
        });

    public void DatLaiMatKhau(int maTK, string? matKhauMoi) =>
        ThucHien(() =>
        {
            YeuCauGiangVien();
            if (MatKhauHelper.KiemTraDoManh(matKhauMoi) is string loi)
                throw new NghiepVuException(loi);

            if (!_taiKhoanDal.DoiMatKhau(maTK, MatKhauHelper.BamMatKhau(matKhauMoi!)))
                throw new NghiepVuException("Tài khoản không tồn tại hoặc đã bị xóa.");
        });

    /// <summary>Khóa (false) hoặc mở khóa (true) tài khoản.</summary>
    public void DatTrangThai(int maTK, bool dangHoatDong) =>
        ThucHien(() =>
        {
            var nguoiDung = YeuCauGiangVien();
            if (maTK == nguoiDung.MaTK && !dangHoatDong)
                throw new NghiepVuException("Không thể tự khóa tài khoản của chính mình.");

            if (!_taiKhoanDal.DatTrangThai(maTK, dangHoatDong))
                throw new NghiepVuException("Tài khoản không tồn tại hoặc đã bị xóa.");
        });

    public void Xoa(int maTK) =>
        ThucHien(() =>
        {
            var nguoiDung = YeuCauGiangVien();
            if (maTK == nguoiDung.MaTK)
                throw new NghiepVuException("Không thể xóa tài khoản đang đăng nhập.");

            if (_taiKhoanDal.CoDuLieuLienQuan(maTK))
                throw new NghiepVuException(
                    "Tài khoản đã có bài thi, câu hỏi hoặc đề thi liên quan nên không thể xóa. Hãy khóa tài khoản thay vì xóa.");

            if (!_taiKhoanDal.Xoa(maTK))
                throw new NghiepVuException("Tài khoản không tồn tại hoặc đã bị xóa.");
        });

    private void KiemTraTrung(TaiKhoan taiKhoan, List<string> loi)
    {
        if (_taiKhoanDal.TenDangNhapDaTonTai(taiKhoan.TenDangNhap, taiKhoan.MaTK))
            loi.Add($"Tên đăng nhập \"{taiKhoan.TenDangNhap}\" đã tồn tại.");
        if (_taiKhoanDal.EmailDaTonTai(taiKhoan.Email, taiKhoan.MaTK))
            loi.Add($"Email \"{taiKhoan.Email}\" đã được sử dụng.");
    }
}
