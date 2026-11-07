using System.Data;
using Microsoft.Data.SqlClient;

namespace ThiTracNghiem.DAL;

/// <summary>Các thao tác CRUD chung cho một thực thể kiểu T.</summary>
public interface IRepository<T> where T : class
{
    List<T> LayTatCa();
    T? LayTheoMa(int ma);

    /// <summary>Thêm mới và trả về mã (khóa chính tự tăng) vừa được tạo.</summary>
    int Them(T doiTuong);

    bool CapNhat(T doiTuong);
    bool Xoa(int ma);
}

/// <summary>
/// Lớp cơ sở cho các DAL có CRUD. Phần đọc dữ liệu dùng chung được cài đặt sẵn;
/// mỗi lớp con chỉ khai báo câu SELECT, cột khóa và cách ánh xạ một dòng thành đối tượng.
/// </summary>
public abstract class RepositoryBase<T> : IRepository<T> where T : class
{
    /// <summary>Câu SELECT ... FROM ... (chưa có WHERE / ORDER BY).</summary>
    protected abstract string CauLenhChon { get; }

    /// <summary>Tên cột khóa chính (kèm bí danh bảng nếu có join, ví dụ "dt.MaDe").</summary>
    protected abstract string CotKhoa { get; }

    /// <summary>Chuyển một dòng dữ liệu thành đối tượng.</summary>
    protected abstract T AnhXa(IDataRecord dong);

    public virtual List<T> LayTatCa() => DocDanhSach($"{CauLenhChon} ORDER BY {CotKhoa}");

    public virtual T? LayTheoMa(int ma) =>
        DocDanhSach($"{CauLenhChon} WHERE {CotKhoa} = @Ma", DbHelper.ThamSo("@Ma", ma)).FirstOrDefault();

    public abstract int Them(T doiTuong);
    public abstract bool CapNhat(T doiTuong);
    public abstract bool Xoa(int ma);

    protected List<T> DocDanhSach(string sql, params SqlParameter[] thamSo) =>
        DbHelper.ExecuteReader(sql, CommandType.Text, AnhXa, thamSo);

    protected static bool TonTai(string sql, params SqlParameter[] thamSo) =>
        Convert.ToInt32(DbHelper.ExecuteScalar(sql, thamSo)) > 0;
}
