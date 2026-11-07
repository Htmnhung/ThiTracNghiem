using ThiTracNghiem.Models;

namespace ThiTracNghiem.BLL;

/// <summary>Bài thi đang làm (hoặc đã nộp): gồm đề thi và lượt thi của sinh viên. Giao diện làm bài giữ đối tượng này.</summary>
public class BaiThi
{
    public BaiThi(DeThi deThi, LuotThi luotThi, bool laBaiLamTiep = false)
    {
        DeThi = deThi;
        LuotThi = luotThi;
        LaBaiLamTiep = laBaiLamTiep;
    }

    public DeThi DeThi { get; }
    public LuotThi LuotThi { get; }

    /// <summary>true: đây là lượt thi đang làm dở trước đó (ví dụ ứng dụng bị tắt giữa chừng), được mở lại.</summary>
    public bool LaBaiLamTiep { get; }

    /// <summary>Thời điểm hết giờ làm bài.</summary>
    public DateTime HanNop => LuotThi.ThoiGianBatDau.AddMinutes(DeThi.ThoiGianLam);
}

/// <summary>Một dòng trong danh sách đề thi hiển thị cho sinh viên.</summary>
public record DeThiChoSinhVien(
    DeThi DeThi,
    string TrangThai,
    int SoLanDaThi,
    bool CoBaiDangLam,
    bool CoTheVaoThi)
{
    public string SoLanThiHienThi => $"{SoLanDaThi}/{DeThi.SoLanThiToiDa}";
}
