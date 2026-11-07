using System.Data;
using ThiTracNghiem.DAL;
using ThiTracNghiem.Models;

namespace ThiTracNghiem.BLL;

/// <summary>Nghiệp vụ làm bài thi của sinh viên: vào thi, chọn đáp án, nộp bài, xem lại bài.</summary>
public class ThiService : ServiceBase
{
    /// <summary>Số giây cho phép trễ khi lưu đáp án / nộp bài (bù độ trễ của đồng hồ và mạng).</summary>
    public const int ThoiGianAnHanGiay = 10;

    private readonly DeThiDAL _deThiDal = new();
    private readonly LuotThiDAL _luotThiDal = new();

    /// <summary>Danh sách đề thi kèm trạng thái và số lần sinh viên đã thi; đề đang mở hiển thị trước.</summary>
    public List<DeThiChoSinhVien> LayDanhSachDeThi() =>
        ThucHien(() =>
        {
            var sinhVien = YeuCauSinhVien();
            var bayGio = BayGio;

            return _deThiDal.LayTatCa()
                .Select(de =>
                {
                    int soLanDaThi = _luotThiDal.DemSoLanThi(de.MaDe, sinhVien.MaTK);
                    bool coBaiDangLam = _luotThiDal.LayMaLuotDangLam(de.MaDe, sinhVien.MaTK) != null;
                    bool coTheVaoThi = coBaiDangLam || (de.DangMo(bayGio) && soLanDaThi < de.SoLanThiToiDa);

                    return new DeThiChoSinhVien(de, de.TrangThaiHienThi(bayGio), soLanDaThi, coBaiDangLam, coTheVaoThi);
                })
                .OrderByDescending(d => d.CoTheVaoThi)
                .ThenBy(d => d.DeThi.ThoiGianDong)
                .ToList();
        });

    /// <summary>
    /// Vào thi. Nếu còn lượt đang làm dở và chưa hết giờ thì mở lại lượt đó;
    /// nếu đã hết giờ thì nộp tự động lượt đó rồi xét tạo lượt mới.
    /// </summary>
    public BaiThi BatDauThi(int maDe) =>
        ThucHien(() =>
        {
            var sinhVien = YeuCauSinhVien();
            var deThi = BatBuocTonTai(_deThiDal.LayTheoMa(maDe), "Đề thi không tồn tại.");

            if (_luotThiDal.LayMaLuotDangLam(maDe, sinhVien.MaTK) is int maLuotDangLam)
            {
                var luotDangLam = _luotThiDal.LayTheoMa(maLuotDangLam)!;
                var baiDangLam = new BaiThi(deThi, luotDangLam, laBaiLamTiep: true);

                if (BayGio <= baiDangLam.HanNop)
                    return baiDangLam;

                NopVaLuu(baiDangLam);   // hết giờ khi đang làm dở → nộp tự động với các đáp án đã lưu
            }

            var bayGio = BayGio;
            if (bayGio < deThi.ThoiGianMo)
                throw new NghiepVuException($"Đề thi chưa mở. Thời gian mở: {deThi.ThoiGianMo:dd/MM/yyyy HH:mm}.");
            if (bayGio > deThi.ThoiGianDong)
                throw new NghiepVuException($"Đề thi đã đóng lúc {deThi.ThoiGianDong:dd/MM/yyyy HH:mm}.");

            int soLanDaThi = _luotThiDal.DemSoLanThi(maDe, sinhVien.MaTK);
            if (soLanDaThi >= deThi.SoLanThiToiDa)
                throw new NghiepVuException($"Bạn đã thi {soLanDaThi}/{deThi.SoLanThiToiDa} lần, không thể thi thêm.");

            // Bốc câu hỏi ngẫu nhiên + tạo lượt thi trong một transaction (stored procedure)
            int maLT = _luotThiDal.TaoLuotThi(maDe, sinhVien.MaTK, bayGio);
            return new BaiThi(deThi, _luotThiDal.LayTheoMa(maLT)!);
        });

    /// <summary>Thời gian còn lại để hiển thị đồng hồ đếm ngược.</summary>
    public TimeSpan ThoiGianConLai(BaiThi baiThi) =>
        baiThi.LuotThi.ThoiGianConLai(baiThi.DeThi.ThoiGianLam, BayGio);

    /// <summary>Ghi nhận đáp án một câu và lưu ngay vào CSDL (tránh mất bài khi máy gặp sự cố).</summary>
    public void ChonDapAn(BaiThi baiThi, int maCH, string? dapAnChon) =>
        ThucHien(() =>
        {
            KiemTraChuSoHuu(baiThi);

            if (baiThi.LuotThi.DaNop)
                throw new NghiepVuException("Bài thi đã nộp, không thể thay đổi đáp án.");
            if (BayGio > baiThi.HanNop.AddSeconds(ThoiGianAnHanGiay))
                throw new NghiepVuException("Đã hết giờ làm bài. Đáp án không được ghi nhận.");

            try
            {
                baiThi.LuotThi.ChonDapAn(maCH, dapAnChon);
            }
            catch (Exception ex) when (ex is KeyNotFoundException or ArgumentException)
            {
                throw new NghiepVuException(ex.Message);
            }

            if (!_luotThiDal.LuuDapAn(baiThi.LuotThi.MaLT, maCH, baiThi.LuotThi.ChiTiet.First(ct => ct.MaCH == maCH).DapAnChon))
                throw new NghiepVuException("Không lưu được đáp án: lượt thi đã được nộp hoặc không còn tồn tại.");
        });

    /// <summary>Nộp bài (sinh viên bấm Nộp bài hoặc hết giờ) và trả về điểm.</summary>
    public decimal NopBai(BaiThi baiThi) =>
        ThucHien(() =>
        {
            KiemTraChuSoHuu(baiThi);
            if (baiThi.LuotThi.DaNop)
                throw new NghiepVuException("Bài thi đã được nộp trước đó.");

            return NopVaLuu(baiThi);
        });

    /// <summary>Mở lại một bài đã nộp để xem điểm và đáp án. Sinh viên chỉ xem được bài của mình.</summary>
    public BaiThi XemLaiBai(int maLT) =>
        ThucHien(() =>
        {
            var nguoiDung = YeuCauDangNhap();
            var luotThi = BatBuocTonTai(_luotThiDal.LayTheoMa(maLT), "Lượt thi không tồn tại.");

            if (nguoiDung.LaSinhVien && luotThi.MaTK != nguoiDung.MaTK)
                throw new NghiepVuException("Bạn không có quyền xem bài thi này.");
            if (!luotThi.DaNop)
                throw new NghiepVuException("Bài thi chưa nộp nên chưa thể xem lại.");

            var deThi = BatBuocTonTai(_deThiDal.LayTheoMa(luotThi.MaDe), "Đề thi không tồn tại.");
            return new BaiThi(deThi, luotThi);
        });

    /// <summary>Lịch sử các bài đã nộp của sinh viên đang đăng nhập.</summary>
    public DataTable LayLichSuThi() =>
        ThucHien(() =>
        {
            var sinhVien = YeuCauSinhVien();
            return _luotThiDal.TimKiemKetQua(maTK: sinhVien.MaTK);
        });

    private decimal NopVaLuu(BaiThi baiThi)
    {
        // Nộp sau khi hết giờ (nộp tự động) thì ghi thời điểm nộp đúng bằng hạn nộp
        var thoiGianNop = BayGio > baiThi.HanNop ? baiThi.HanNop : BayGio;

        // Chấm điểm: LuotThi.NopBai gọi phương thức đa hình KiemTraDapAn của từng câu
        decimal diem = baiThi.LuotThi.NopBai(thoiGianNop);
        _luotThiDal.LuuKetQuaNopBai(baiThi.LuotThi);
        return diem;
    }

    private static void KiemTraChuSoHuu(BaiThi baiThi)
    {
        var sinhVien = YeuCauSinhVien();
        if (baiThi.LuotThi.MaTK != sinhVien.MaTK)
            throw new NghiepVuException("Bài thi không thuộc về tài khoản đang đăng nhập.");
    }
}
