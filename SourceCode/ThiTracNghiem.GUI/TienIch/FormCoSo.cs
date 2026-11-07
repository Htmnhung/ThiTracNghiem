using ThiTracNghiem.BLL;

namespace ThiTracNghiem.GUI;

/// <summary>
/// Lớp cha của mọi form trong ứng dụng: thống nhất cách hiển thị thông báo,
/// xử lý ngoại lệ (không để chương trình dừng đột ngột) và định dạng lưới dữ liệu.
/// </summary>
public class FormCoSo : Form
{
    protected const string TieuDeUngDung = "Thi trắc nghiệm";

    // ===================== Thông báo =====================

    protected static void HienThongBao(string noiDung) =>
        MessageBox.Show(noiDung, TieuDeUngDung, MessageBoxButtons.OK, MessageBoxIcon.Information);

    protected static void HienCanhBao(string noiDung) =>
        MessageBox.Show(noiDung, TieuDeUngDung, MessageBoxButtons.OK, MessageBoxIcon.Warning);

    protected static void HienLoi(string noiDung) =>
        MessageBox.Show(noiDung, TieuDeUngDung, MessageBoxButtons.OK, MessageBoxIcon.Error);

    /// <summary>Hỏi xác nhận; nút mặc định là "No" để tránh bấm nhầm khi xóa / hủy.</summary>
    protected static bool XacNhan(string noiDung) =>
        MessageBox.Show(noiDung, TieuDeUngDung, MessageBoxButtons.YesNo, MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button2) == DialogResult.Yes;

    /// <summary>Lỗi không lường trước (lỗi lập trình) - dùng cho bộ bắt lỗi toàn cục trong Program.</summary>
    public static void HienLoiKhongMongMuon(Exception ex) =>
        MessageBox.Show("Đã xảy ra lỗi không mong muốn:\n" + ex.Message + "\n\nVui lòng thử lại thao tác.",
            TieuDeUngDung, MessageBoxButtons.OK, MessageBoxIcon.Error);

    // ===================== Xử lý lỗi =====================

    /// <summary>
    /// Gọi tầng BLL an toàn: lỗi nghiệp vụ / lỗi kết nối được hiển thị thành thông báo dễ hiểu.
    /// Trả về true nếu thao tác thành công.
    /// </summary>
    protected static bool ThucHien(Action thaoTac)
    {
        try
        {
            thaoTac();
            return true;
        }
        catch (NghiepVuException ex)
        {
            if (ex.LaLoiKetNoi)
                HienLoi(ex.Message);
            else
                HienCanhBao(ex.Message);
            return false;
        }
        catch (Exception ex)
        {
            HienLoiKhongMongMuon(ex);
            return false;
        }
    }

    // ===================== Lưới dữ liệu =====================

    protected static void CaiDatLuoi(DataGridView luoi)
    {
        luoi.ReadOnly = true;
        luoi.AllowUserToAddRows = false;
        luoi.AllowUserToDeleteRows = false;
        luoi.AllowUserToResizeRows = false;
        luoi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        luoi.MultiSelect = false;
        luoi.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        luoi.RowHeadersVisible = false;
        luoi.BackgroundColor = Color.White;
        luoi.BorderStyle = BorderStyle.FixedSingle;
        luoi.EnableHeadersVisualStyles = false;
        luoi.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(225, 233, 245);
        luoi.ColumnHeadersDefaultCellStyle.Font = new Font(luoi.Font, FontStyle.Bold);
        luoi.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        luoi.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(246, 249, 253);
    }

    /// <summary>Đặt tiêu đề, tỉ lệ độ rộng và định dạng hiển thị cho một cột (nếu cột tồn tại).</summary>
    protected static void DatCot(DataGridView luoi, string tenCot, string tieuDe, float tiLeRong = 100, string? dinhDang = null)
    {
        if (luoi.Columns[tenCot] is not { } cot)
            return;

        cot.HeaderText = tieuDe;
        cot.FillWeight = tiLeRong;
        if (dinhDang != null)
            cot.DefaultCellStyle.Format = dinhDang;
    }

    protected static void AnCot(DataGridView luoi, params string[] cacCot)
    {
        foreach (var tenCot in cacCot)
            if (luoi.Columns[tenCot] is { } cot)
                cot.Visible = false;
    }

    /// <summary>Giá trị (kiểu int) của một cột ở dòng đang chọn; null nếu chưa chọn dòng nào.</summary>
    protected static int? LayMaDongChon(DataGridView luoi, string tenCot)
    {
        if (luoi.SelectedRows.Count == 0)
            return null;

        var giaTri = luoi.SelectedRows[0].Cells[tenCot].Value;
        return giaTri is null or DBNull ? null : Convert.ToInt32(giaTri);
    }

    /// <summary>Chọn dòng có giá trị cột bằng mã cho trước; không tìm thấy thì bỏ chọn.</summary>
    protected static void ChonDong(DataGridView luoi, string tenCot, int ma)
    {
        foreach (DataGridViewRow dong in luoi.Rows)
        {
            if (dong.Cells[tenCot].Value is { } giaTri && giaTri is not DBNull && Convert.ToInt32(giaTri) == ma)
            {
                var cotDauTien = luoi.Columns.GetFirstColumn(DataGridViewElementStates.Visible);
                if (cotDauTien != null)
                    luoi.CurrentCell = dong.Cells[cotDauTien.Index];
                dong.Selected = true;
                return;
            }
        }

        luoi.ClearSelection();
    }
}
