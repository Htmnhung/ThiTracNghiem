using ThiTracNghiem.DAL;
using ThiTracNghiem.Models;

namespace ThiTracNghiem.BLL;

/// <summary>Bộ lọc trạng thái đề thi trên màn hình quản lý đề.</summary>
public enum LocTrangThaiDe
{
    TatCa,
    ChuaMo,
    DangMo,
    DaDong
}

/// <summary>Quản lý đề thi (chỉ giảng viên).</summary>
public class DeThiService : ServiceBase
{
    private readonly DeThiDAL _deThiDal = new();
    private readonly CauHoiDAL _cauHoiDal = new();

    public List<DeThi> TimKiem(int? maMH, string? tuKhoa, LocTrangThaiDe trangThai = LocTrangThaiDe.TatCa) =>
        ThucHien(() =>
        {
            YeuCauGiangVien();
            var bayGio = BayGio;

            return _deThiDal.TimKiem(maMH, tuKhoa)
                .Where(de => trangThai switch
                {
                    LocTrangThaiDe.ChuaMo => bayGio < de.ThoiGianMo,
                    LocTrangThaiDe.DangMo => de.DangMo(bayGio),
                    LocTrangThaiDe.DaDong => bayGio > de.ThoiGianDong,
                    _ => true
                })
                .ToList();
        });

    public DeThi? LayTheoMa(int maDe) =>
        ThucHien(() =>
        {
            YeuCauGiangVien();
            return _deThiDal.LayTheoMa(maDe);
        });

    /// <summary>Đề đã có người thi thì giao diện chỉ cho sửa tên, thời gian đóng và số lần thi.</summary>
    public bool DaCoNguoiThi(int maDe) => ThucHien(() => _deThiDal.CoLuotThi(maDe));

    public int Them(DeThi deThi) =>
        ThucHien(() =>
        {
            var giangVien = YeuCauGiangVien();
            KiemTra(deThi);

            deThi.NguoiTao = giangVien.MaTK;
            deThi.MaDe = _deThiDal.Them(deThi);
            return deThi.MaDe;
        });

    public void CapNhat(DeThi deThi) =>
        ThucHien(() =>
        {
            YeuCauGiangVien();
            var cu = BatBuocTonTai(_deThiDal.LayTheoMa(deThi.MaDe), "Đề thi không tồn tại hoặc đã bị xóa.");

            if (_deThiDal.CoLuotThi(deThi.MaDe)
                && (deThi.MaMH != cu.MaMH || deThi.ThoiGianLam != cu.ThoiGianLam
                    || deThi.SoCauDe != cu.SoCauDe || deThi.SoCauTB != cu.SoCauTB || deThi.SoCauKho != cu.SoCauKho))
            {
                throw new NghiepVuException(
                    "Đề thi đã có sinh viên làm bài nên không được đổi môn học, thời gian làm bài và số câu. " +
                    "Chỉ được sửa tên đề, thời gian mở/đóng và số lần thi tối đa.");
            }

            KiemTra(deThi);
            _deThiDal.CapNhat(deThi);
        });

    public void Xoa(int maDe) =>
        ThucHien(() =>
        {
            YeuCauGiangVien();
            if (_deThiDal.CoLuotThi(maDe))
                throw new NghiepVuException("Đề thi đã có sinh viên làm bài, không thể xóa. Có thể đóng đề bằng cách sửa thời gian đóng.");

            if (!_deThiDal.Xoa(maDe))
                throw new NghiepVuException("Đề thi không tồn tại hoặc đã bị xóa.");
        });

    /// <summary>Kiểm tra dữ liệu đề và kiểm tra ngân hàng câu hỏi có đủ câu theo từng độ khó.</summary>
    private void KiemTra(DeThi deThi)
    {
        var loi = deThi.KiemTraHopLe();

        if (deThi.MaMH > 0)
        {
            var soCauHienCo = _cauHoiDal.DemTheoDoKho(deThi.MaMH);
            foreach (var doKho in Enum.GetValues<DoKho>())
            {
                int canLay = deThi.SoCauTheoDoKho(doKho);
                if (canLay > soCauHienCo[doKho])
                    loi.Add($"Ngân hàng câu hỏi của môn chỉ có {soCauHienCo[doKho]} câu {doKho.HienThi().ToLower()}, " +
                            $"đề yêu cầu {canLay} câu.");
            }
        }

        BaoLoiNeuCo(loi);
    }
}
