using ThiTracNghiem.DAL;
using ThiTracNghiem.Models;

namespace ThiTracNghiem.BLL;

public class MonHocService : ServiceBase
{
    private readonly MonHocDAL _monHocDal = new();

    public List<MonHoc> LayTatCa() =>
        ThucHien(() =>
        {
            YeuCauDangNhap();
            return _monHocDal.LayTatCa();
        });

    public List<MonHoc> TimKiem(string? tuKhoa) =>
        ThucHien(() =>
        {
            YeuCauDangNhap();
            return _monHocDal.TimKiem(tuKhoa);
        });

    public int Them(MonHoc monHoc) =>
        ThucHien(() =>
        {
            YeuCauGiangVien();
            KiemTra(monHoc);
            monHoc.MaMH = _monHocDal.Them(monHoc);
            return monHoc.MaMH;
        });

    public void CapNhat(MonHoc monHoc) =>
        ThucHien(() =>
        {
            YeuCauGiangVien();
            BatBuocTonTai(_monHocDal.LayTheoMa(monHoc.MaMH), "Môn học không tồn tại hoặc đã bị xóa.");
            KiemTra(monHoc);
            _monHocDal.CapNhat(monHoc);
        });

    public void Xoa(int maMH) =>
        ThucHien(() =>
        {
            YeuCauGiangVien();

            int soCauHoi = _monHocDal.DemCauHoi(maMH);
            int soDeThi = _monHocDal.DemDeThi(maMH);
            if (soCauHoi > 0 || soDeThi > 0)
                throw new NghiepVuException(
                    $"Không thể xóa: môn học đang có {soCauHoi} câu hỏi và {soDeThi} đề thi.");

            if (!_monHocDal.Xoa(maMH))
                throw new NghiepVuException("Môn học không tồn tại hoặc đã bị xóa.");
        });

    private void KiemTra(MonHoc monHoc)
    {
        var loi = monHoc.KiemTraHopLe();
        if (loi.Count == 0 && _monHocDal.TenDaTonTai(monHoc.TenMH, monHoc.MaMH))
            loi.Add($"Môn học \"{monHoc.TenMH}\" đã tồn tại.");
        BaoLoiNeuCo(loi);
    }
}
