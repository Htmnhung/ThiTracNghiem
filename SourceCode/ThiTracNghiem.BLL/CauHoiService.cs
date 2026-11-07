using ThiTracNghiem.DAL;
using ThiTracNghiem.Models;

namespace ThiTracNghiem.BLL;

/// <summary>Kết quả khi giảng viên yêu cầu xóa một câu hỏi.</summary>
public enum KetQuaXoaCauHoi
{
    /// <summary>Câu hỏi chưa từng được dùng → đã xóa hẳn.</summary>
    DaXoa,
    /// <summary>Câu hỏi đã nằm trong bài thi → chỉ ẩn đi (xóa mềm) để giữ lịch sử bài làm.</summary>
    DaAn
}

/// <summary>Quản lý ngân hàng câu hỏi (chỉ giảng viên).</summary>
public class CauHoiService : ServiceBase
{
    private readonly CauHoiDAL _cauHoiDal = new();

    public List<CauHoi> TimKiem(int? maMH, DoKho? doKho, LoaiCauHoi? loai, string? tuKhoa, bool hienCauDaAn) =>
        ThucHien(() =>
        {
            YeuCauGiangVien();
            return _cauHoiDal.TimKiem(maMH, doKho, loai, tuKhoa, hienCauDaAn);
        });

    public CauHoi? LayTheoMa(int maCH) =>
        ThucHien(() =>
        {
            YeuCauGiangVien();
            return _cauHoiDal.LayTheoMa(maCH);
        });

    /// <summary>Số câu đang sử dụng theo độ khó của một môn (hiển thị khi tạo đề).</summary>
    public Dictionary<DoKho, int> DemTheoDoKho(int maMH) =>
        ThucHien(() =>
        {
            YeuCauGiangVien();
            return _cauHoiDal.DemTheoDoKho(maMH);
        });

    /// <summary>Tạo đối tượng câu hỏi rỗng đúng loại (giao diện dùng khi người dùng đổi loại câu hỏi).</summary>
    public CauHoi TaoMoi(LoaiCauHoi loai) => CauHoi.TaoTheoLoai(loai);

    public int Them(CauHoi cauHoi) =>
        ThucHien(() =>
        {
            var giangVien = YeuCauGiangVien();

            // KiemTraHopLe là phương thức đa hình: mỗi loại câu hỏi có quy tắc riêng
            BaoLoiNeuCo(cauHoi.KiemTraHopLe());

            cauHoi.NguoiTao = giangVien.MaTK;
            cauHoi.MaCH = _cauHoiDal.Them(cauHoi);
            return cauHoi.MaCH;
        });

    public void CapNhat(CauHoi cauHoi) =>
        ThucHien(() =>
        {
            YeuCauGiangVien();
            var cu = BatBuocTonTai(_cauHoiDal.LayTheoMa(cauHoi.MaCH), "Câu hỏi không tồn tại hoặc đã bị xóa.");

            var loi = cauHoi.KiemTraHopLe();

            // Câu đã có trong bài thi: không được đổi những gì ảnh hưởng tới kết quả chấm của các bài cũ
            if (_cauHoiDal.DaDuocSuDung(cauHoi.MaCH))
            {
                if (cauHoi.Loai != cu.Loai || cauHoi.DapAnDung != cu.DapAnDung || cauHoi.MaMH != cu.MaMH)
                    loi.Add("Câu hỏi đã được dùng trong bài thi nên không được đổi môn học, loại câu hỏi hoặc đáp án đúng. " +
                            "Hãy ẩn câu này và tạo câu hỏi mới.");
            }
            BaoLoiNeuCo(loi);

            _cauHoiDal.CapNhat(cauHoi);
        });

    /// <summary>
    /// Xóa câu hỏi. Nếu câu hỏi đã từng xuất hiện trong bài thi thì chỉ ẩn đi
    /// để không làm mất lịch sử bài làm của sinh viên.
    /// </summary>
    public KetQuaXoaCauHoi Xoa(int maCH) =>
        ThucHien(() =>
        {
            YeuCauGiangVien();
            BatBuocTonTai(_cauHoiDal.LayTheoMa(maCH), "Câu hỏi không tồn tại hoặc đã bị xóa.");

            if (_cauHoiDal.DaDuocSuDung(maCH))
            {
                _cauHoiDal.DatTrangThai(maCH, false);
                return KetQuaXoaCauHoi.DaAn;
            }

            _cauHoiDal.Xoa(maCH);
            return KetQuaXoaCauHoi.DaXoa;
        });

    /// <summary>Ẩn (false) hoặc hiện lại (true) câu hỏi.</summary>
    public void DatTrangThai(int maCH, bool dangSuDung) =>
        ThucHien(() =>
        {
            YeuCauGiangVien();
            if (!_cauHoiDal.DatTrangThai(maCH, dangSuDung))
                throw new NghiepVuException("Câu hỏi không tồn tại hoặc đã bị xóa.");
        });
}
