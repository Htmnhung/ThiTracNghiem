using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;

namespace ThiTracNghiem.DAL;

/// <summary>
/// Lớp tiện ích dùng chung cho tầng DAL: tạo kết nối, thực thi câu lệnh có tham số,
/// quản lý transaction và chuyển SqlException thành <see cref="DataAccessException"/>.
/// </summary>
public static class DbHelper
{
    private const string TenChuoiKetNoi = "ThiTracNghiemDb";

    private const string ThongBaoMatKetNoi =
        "Không kết nối được tới SQL Server. Hãy kiểm tra SQL Server đã chạy, CSDL ThiTracNghiem đã được tạo " +
        "và chuỗi kết nối trong App.config.";

    // Mã lỗi SqlException liên quan tới kết nối (mất mạng, timeout, không mở được CSDL, sai đăng nhập...)
    private static readonly int[] MaLoiKetNoi = { -2, -1, 2, 53, 121, 233, 4060, 18456, 10053, 10054, 10060, 10061, 11001 };

    private static readonly Regex MauTenRangBuoc =
        new(@"constraint\s+['""](?<ten>[^'""]+)['""]", RegexOptions.IgnoreCase | RegexOptions.Compiled);

    /// <summary>Gán chuỗi kết nối trực tiếp (ví dụ khi kiểm thử). null = đọc từ App.config.</summary>
    public static string? ChuoiKetNoiTuyChinh { get; set; }

    public static string ChuoiKetNoi =>
        ChuoiKetNoiTuyChinh
        ?? ConfigurationManager.ConnectionStrings[TenChuoiKetNoi]?.ConnectionString
        ?? throw new DataAccessException(LoaiLoiDuLieu.KhongKetNoiDuoc,
            $"Không tìm thấy chuỗi kết nối \"{TenChuoiKetNoi}\" trong App.config.");

    // ===================== Kết nối và câu lệnh =====================

    public static SqlConnection TaoKetNoi()
    {
        try
        {
            return new SqlConnection(ChuoiKetNoi);
        }
        catch (ArgumentException ex)
        {
            throw new DataAccessException(LoaiLoiDuLieu.KhongKetNoiDuoc, "Chuỗi kết nối trong App.config không hợp lệ.", null, ex);
        }
    }

    /// <summary>Mở kết nối; lỗi ở bước này luôn được xem là lỗi kết nối.</summary>
    internal static void MoKetNoi(SqlConnection ketNoi)
    {
        try
        {
            ketNoi.Open();
        }
        catch (SqlException ex)
        {
            throw new DataAccessException(LoaiLoiDuLieu.KhongKetNoiDuoc, ThongBaoMatKetNoi, null, ex);
        }
    }

    internal static SqlCommand TaoLenh(SqlConnection ketNoi, string sql, CommandType loai,
        SqlParameter[] thamSo, SqlTransaction? giaoTac = null)
    {
        var lenh = new SqlCommand(sql, ketNoi, giaoTac) { CommandType = loai };
        lenh.Parameters.AddRange(thamSo);
        return lenh;
    }

    /// <summary>Tạo tham số; giá trị null được chuyển thành DBNull.</summary>
    public static SqlParameter ThamSo(string ten, object? giaTri) => new(ten, giaTri ?? DBNull.Value);

    /// <summary>Thoát các ký tự đặc biệt của LIKE ([, %, _) trong từ khóa người dùng nhập.</summary>
    public static string ThoatKyTuLike(string tuKhoa) =>
        tuKhoa.Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]");

    // ===================== Thực thi =====================

    public static DataTable ExecuteQuery(string sql, params SqlParameter[] thamSo) =>
        LayBang(sql, CommandType.Text, thamSo);

    public static DataTable ExecuteProcedure(string tenThuTuc, params SqlParameter[] thamSo) =>
        LayBang(tenThuTuc, CommandType.StoredProcedure, thamSo);

    public static int ExecuteNonQuery(string sql, params SqlParameter[] thamSo) =>
        ThucThi(() =>
        {
            using var ketNoi = TaoKetNoi();
            using var lenh = TaoLenh(ketNoi, sql, CommandType.Text, thamSo);
            MoKetNoi(ketNoi);
            return lenh.ExecuteNonQuery();
        });

    public static int ExecuteProcedureNonQuery(string tenThuTuc, params SqlParameter[] thamSo) =>
        ThucThi(() =>
        {
            using var ketNoi = TaoKetNoi();
            using var lenh = TaoLenh(ketNoi, tenThuTuc, CommandType.StoredProcedure, thamSo);
            MoKetNoi(ketNoi);
            return lenh.ExecuteNonQuery();
        });

    public static object? ExecuteScalar(string sql, params SqlParameter[] thamSo) =>
        ThucThi(() =>
        {
            using var ketNoi = TaoKetNoi();
            using var lenh = TaoLenh(ketNoi, sql, CommandType.Text, thamSo);
            MoKetNoi(ketNoi);
            var ketQua = lenh.ExecuteScalar();
            return ketQua == DBNull.Value ? null : ketQua;
        });

    /// <summary>Đọc dữ liệu và chuyển từng dòng thành đối tượng bằng hàm ánh xạ.</summary>
    public static List<T> ExecuteReader<T>(string sql, CommandType loai, Func<IDataRecord, T> anhXa,
        params SqlParameter[] thamSo) =>
        ThucThi(() =>
        {
            using var ketNoi = TaoKetNoi();
            using var lenh = TaoLenh(ketNoi, sql, loai, thamSo);
            MoKetNoi(ketNoi);
            using var docDuLieu = lenh.ExecuteReader();

            var danhSach = new List<T>();
            while (docDuLieu.Read())
                danhSach.Add(anhXa(docDuLieu));
            return danhSach;
        });

    /// <summary>
    /// Thực hiện nhiều câu lệnh trong cùng một transaction:
    /// tất cả thành công thì COMMIT, có lỗi bất kỳ thì ROLLBACK.
    /// </summary>
    public static void ThucThiGiaoTac(Action<SqlConnection, SqlTransaction> congViec) =>
        ThucThi(() =>
        {
            using var ketNoi = TaoKetNoi();
            MoKetNoi(ketNoi);
            using var giaoTac = ketNoi.BeginTransaction();
            try
            {
                congViec(ketNoi, giaoTac);
                giaoTac.Commit();
            }
            catch
            {
                try { giaoTac.Rollback(); } catch (Exception) { /* kết nối đã hỏng: SQL Server tự hủy transaction */ }
                throw;
            }
            return true;
        });

    /// <summary>Kiểm tra có kết nối được tới CSDL hay không.</summary>
    public static bool KiemTraKetNoi()
    {
        try
        {
            using var ketNoi = TaoKetNoi();
            MoKetNoi(ketNoi);
            return true;
        }
        catch (DataAccessException)
        {
            return false;
        }
    }

    // ===================== Xử lý lỗi =====================

    /// <summary>Chạy một thao tác CSDL và chuyển mọi SqlException thành DataAccessException.</summary>
    internal static T ThucThi<T>(Func<T> thaoTac)
    {
        try
        {
            return thaoTac();
        }
        catch (SqlException ex)
        {
            throw ChuyenDoiLoi(ex);
        }
    }

    private static DataAccessException ChuyenDoiLoi(SqlException ex)
    {
        var khop = MauTenRangBuoc.Match(ex.Message);
        var tenRangBuoc = khop.Success ? khop.Groups["ten"].Value : null;

        if (MaLoiKetNoi.Contains(ex.Number))
            return new DataAccessException(LoaiLoiDuLieu.KhongKetNoiDuoc, ThongBaoMatKetNoi, null, ex);

        return ex.Number switch
        {
            2627 or 2601 => new DataAccessException(LoaiLoiDuLieu.TrungDuLieu,
                "Dữ liệu bị trùng với một bản ghi đã có.", tenRangBuoc, ex),

            547 when tenRangBuoc?.StartsWith("FK_", StringComparison.OrdinalIgnoreCase) == true =>
                new DataAccessException(LoaiLoiDuLieu.ViPhamKhoaNgoai,
                    "Dữ liệu đang được sử dụng ở nơi khác hoặc tham chiếu tới bản ghi không tồn tại.", tenRangBuoc, ex),

            547 => new DataAccessException(LoaiLoiDuLieu.ViPhamRangBuoc,
                "Dữ liệu vi phạm ràng buộc của cơ sở dữ liệu.", tenRangBuoc, ex),

            >= 50000 => new DataAccessException(LoaiLoiDuLieu.LoiNghiepVu, ex.Message, null, ex),

            _ => new DataAccessException(LoaiLoiDuLieu.Khac,
                "Lỗi khi thao tác với cơ sở dữ liệu: " + ex.Message, tenRangBuoc, ex)
        };
    }

    private static DataTable LayBang(string sql, CommandType loai, SqlParameter[] thamSo) =>
        ThucThi(() =>
        {
            using var ketNoi = TaoKetNoi();
            using var lenh = TaoLenh(ketNoi, sql, loai, thamSo);
            MoKetNoi(ketNoi);
            using var boChuyen = new SqlDataAdapter(lenh);
            var bang = new DataTable();
            boChuyen.Fill(bang);
            return bang;
        });
}
