namespace ThiTracNghiem.GUI;

/// <summary>
/// Một mục trong ComboBox: giá trị thực (mã, enum...) và tên hiển thị.
/// GiaTri = null dùng cho mục "(Tất cả)" của các bộ lọc.
/// </summary>
public sealed record MucChon(object? GiaTri, string Ten)
{
    public const string TatCa = "(Tất cả)";

    public override string ToString() => Ten;

    public static List<MucChon> TuEnum<TEnum>(Func<TEnum, string> tenHienThi, bool coMucTatCa = false)
        where TEnum : struct, Enum =>
        TuDanhSach(Enum.GetValues<TEnum>(), e => e, tenHienThi, coMucTatCa);

    public static List<MucChon> TuDanhSach<T>(IEnumerable<T> danhSach, Func<T, object?> giaTri,
        Func<T, string> tenHienThi, bool coMucTatCa = false)
    {
        var ketQua = new List<MucChon>();
        if (coMucTatCa)
            ketQua.Add(new MucChon(null, TatCa));
        ketQua.AddRange(danhSach.Select(x => new MucChon(giaTri(x), tenHienThi(x))));
        return ketQua;
    }
}

public static class ComboBoxExtensions
{
    public static void NapDuLieu(this ComboBox comboBox, IEnumerable<MucChon> danhSach)
    {
        comboBox.BeginUpdate();
        comboBox.Items.Clear();
        foreach (var muc in danhSach)
            comboBox.Items.Add(muc);
        comboBox.EndUpdate();

        if (comboBox.Items.Count > 0)
            comboBox.SelectedIndex = 0;
    }

    public static object? GiaTriChon(this ComboBox comboBox) => (comboBox.SelectedItem as MucChon)?.GiaTri;

    /// <summary>Giá trị kiểu struct (int, enum) của mục đang chọn; null nếu chọn "(Tất cả)".</summary>
    public static T? LayGiaTri<T>(this ComboBox comboBox) where T : struct =>
        comboBox.GiaTriChon() is T giaTri ? giaTri : null;

    public static void ChonGiaTri(this ComboBox comboBox, object? giaTri)
    {
        for (int i = 0; i < comboBox.Items.Count; i++)
        {
            if (comboBox.Items[i] is MucChon muc && Equals(muc.GiaTri, giaTri))
            {
                comboBox.SelectedIndex = i;
                return;
            }
        }
    }
}
