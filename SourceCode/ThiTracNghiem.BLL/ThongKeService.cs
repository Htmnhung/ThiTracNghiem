using System.Data;
using ThiTracNghiem.DAL;

namespace ThiTracNghiem.BLL;

/// <summary>Tra cứu kết quả và thống kê (chỉ giảng viên).</summary>
public class ThongKeService : ServiceBase
{
    private readonly ThongKeDAL _thongKeDal = new();
    private readonly LuotThiDAL _luotThiDal = new();

    /// <summary>Tra cứu kết quả thi theo nhiều tiêu chí kết hợp. Tham số null = bỏ qua.</summary>
    public DataTable TimKiemKetQua(int? maDe, string? lop, decimal? diemTu, decimal? diemDen,
        DateTime? tuNgay, DateTime? denNgay) =>
        ThucHien(() =>
        {
            YeuCauGiangVien();

            var loi = new List<string>();
            if (diemTu is < 0 or > 10 || diemDen is < 0 or > 10)
                loi.Add("Điểm phải nằm trong khoảng 0 đến 10.");
            if (diemTu > diemDen)
                loi.Add("\"Điểm từ\" không được lớn hơn \"điểm đến\".");
            if (tuNgay?.Date > denNgay?.Date)
                loi.Add("\"Từ ngày\" không được sau \"đến ngày\".");
            BaoLoiNeuCo(loi);

            return _luotThiDal.TimKiemKetQua(maDe, null, string.IsNullOrWhiteSpace(lop) ? null : lop.Trim(),
                diemTu, diemDen, tuNgay, denNgay);
        });

    public DataTable ThongKeDiemTheoDe(int? maDe = null) =>
        ThucHien(() =>
        {
            YeuCauGiangVien();
            return _thongKeDal.ThongKeDiemTheoDe(maDe);
        });

    public DataTable PhoDiem(int maDe) =>
        ThucHien(() =>
        {
            YeuCauGiangVien();
            return _thongKeDal.PhoDiem(maDe);
        });

    public DataTable TopCauHoiSaiNhieu(int? maMH = null, int soLuong = 10) =>
        ThucHien(() =>
        {
            YeuCauGiangVien();
            if (soLuong is < 1 or > 100)
                throw new NghiepVuException("Số lượng câu hỏi hiển thị phải từ 1 đến 100.");
            return _thongKeDal.TopCauHoiSaiNhieu(maMH, soLuong);
        });

    public DataTable SinhVienChuaThi(int maDe, string? lop = null) =>
        ThucHien(() =>
        {
            YeuCauGiangVien();
            return _thongKeDal.SinhVienChuaThi(maDe, string.IsNullOrWhiteSpace(lop) ? null : lop.Trim());
        });
}
