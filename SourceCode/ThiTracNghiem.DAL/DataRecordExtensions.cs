using System.Data;

namespace ThiTracNghiem.DAL;

/// <summary>Đọc giá trị từ một dòng dữ liệu, xử lý DBNull và chuyển kiểu (TINYINT, SMALLINT → int...).</summary>
internal static class DataRecordExtensions
{
    public static int LayInt(this IDataRecord dong, string cot) => Convert.ToInt32(dong[cot]);

    public static int? LayIntNull(this IDataRecord dong, string cot) =>
        dong[cot] is DBNull ? null : Convert.ToInt32(dong[cot]);

    public static string LayChuoi(this IDataRecord dong, string cot) =>
        dong[cot] is DBNull ? string.Empty : Convert.ToString(dong[cot])!.Trim();

    public static string? LayChuoiNull(this IDataRecord dong, string cot) =>
        dong[cot] is DBNull ? null : Convert.ToString(dong[cot])!.Trim();

    public static bool LayBool(this IDataRecord dong, string cot) => Convert.ToBoolean(dong[cot]);

    public static bool? LayBoolNull(this IDataRecord dong, string cot) =>
        dong[cot] is DBNull ? null : Convert.ToBoolean(dong[cot]);

    public static DateTime LayNgay(this IDataRecord dong, string cot) => Convert.ToDateTime(dong[cot]);

    public static DateTime? LayNgayNull(this IDataRecord dong, string cot) =>
        dong[cot] is DBNull ? null : Convert.ToDateTime(dong[cot]);

    public static decimal? LayDecimalNull(this IDataRecord dong, string cot) =>
        dong[cot] is DBNull ? null : Convert.ToDecimal(dong[cot]);

    /// <summary>Đọc cột VARCHAR và chuyển thành enum có tên trùng (ví dụ 'TrungBinh' → DoKho.TrungBinh).</summary>
    public static TEnum LayEnum<TEnum>(this IDataRecord dong, string cot) where TEnum : struct, Enum =>
        Enum.Parse<TEnum>(LayChuoi(dong, cot));
}
