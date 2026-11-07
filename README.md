# HỆ THỐNG THI TRẮC NGHIỆM TRÊN MÁY TÍNH

Bài tập lớn môn **Lập trình hướng đối tượng** - Lớp Đ23HT1 - Đề tài số 10.

Ứng dụng desktop (C# WinForms + SQL Server) quản lý môn học, ngân hàng câu hỏi, đề thi,
cho sinh viên làm bài thi trắc nghiệm có giới hạn thời gian và **chấm điểm tự động**.

---

## 1. Môi trường yêu cầu

| Thành phần | Phiên bản đã dùng để phát triển và kiểm thử |
|---|---|
| Hệ điều hành | Windows 10/11 (64-bit) |
| .NET SDK | **.NET 10** (SDK 10.0.402), ngôn ngữ **C# 14** |
| Giao diện | Windows Forms (`net10.0-windows`) |
| IDE | Visual Studio Community 2026 (workload *.NET desktop development*) |
| Hệ quản trị CSDL | **SQL Server 2025 Express LocalDB** (17.0.4025). Chạy được với SQL Server 2019 trở lên |
| Công cụ quản lý CSDL | SQL Server Management Studio 22 (không bắt buộc) |
| Thư viện NuGet | `Microsoft.Data.SqlClient` 7.1.1, `System.Configuration.ConfigurationManager` 10.0.14 |

Các thư viện NuGet được tự động tải về khi build lần đầu (cần có Internet).

---

## 2. Cấu trúc thư mục nộp bài

```
MSSV_HoTen_TenDeTai/
├── BaoCao/                         Báo cáo (.docx và .pdf)
├── Database/
│   └── ThiTracNghiem.sql           Tạo CSDL, bảng, ràng buộc, view, stored procedure, dữ liệu mẫu
├── SourceCode/
│   ├── ThiTracNghiem.sln
│   ├── ThiTracNghiem.Models/       Lớp thực thể (CauHoi + 3 lớp con, DeThi, LuotThi...)
│   ├── ThiTracNghiem.DAL/          Truy cập dữ liệu bằng ADO.NET (DbHelper, *DAL)
│   ├── ThiTracNghiem.BLL/          Xử lý nghiệp vụ (*Service)
│   └── ThiTracNghiem.GUI/          Giao diện Windows Forms + App.config
└── README.md
```

Kiến trúc nhiều lớp: **GUI → BLL → DAL → SQL Server**, tất cả dùng chung **Models**.
Project GUI không tham chiếu tới DAL nên form không thể truy cập CSDL trực tiếp.

---

## 3. Cài đặt cơ sở dữ liệu

- **Tên CSDL:** `ThiTracNghiem`
- **Script:** `Database/ThiTracNghiem.sql`

> ⚠️ Nếu CSDL `ThiTracNghiem` đã tồn tại, script sẽ **xóa và tạo lại** (dữ liệu cũ bị mất).
> Thời gian mở/đóng của các đề thi mẫu được tính theo thời điểm chạy script,
> vì vậy **nên chạy lại script trước buổi demo** để luôn có đề đang mở.

### Cách 1 - Dùng SQL Server Management Studio

1. Mở SSMS, kết nối tới server:
   - LocalDB: `(localdb)\MSSQLLocalDB`
   - SQL Server Express: `.\SQLEXPRESS`
   - Authentication: *Windows Authentication*, tick **Trust server certificate**.
2. **File → Open → File...**, chọn `Database/ThiTracNghiem.sql`.
3. Nhấn **Execute** (F5). Thấy thông báo `Tạo CSDL ThiTracNghiem thành công.` là xong.

### Cách 2 - Dùng dòng lệnh `sqlcmd`

Chạy tại thư mục gốc của bài nộp (`-f 65001` để đọc đúng tiếng Việt):

```bash
sqlcmd -S "(localdb)\MSSQLLocalDB" -E -f 65001 -i Database\ThiTracNghiem.sql
```

---

## 4. Cấu hình chuỗi kết nối

Chuỗi kết nối nằm trong `SourceCode/ThiTracNghiem.GUI/App.config`:

```xml
<add name="ThiTracNghiemDb"
     connectionString="Server=(localdb)\MSSQLLocalDB;Database=ThiTracNghiem;Integrated Security=True;TrustServerCertificate=True"
     providerName="Microsoft.Data.SqlClient" />
```

Nếu máy chạy SQL Server khác LocalDB, **chỉ cần sửa phần `Server=`**:

| Trường hợp | Giá trị `Server=` |
|---|---|
| SQL Server Express (mặc định) | `Server=.\SQLEXPRESS` |
| SQL Server bản đầy đủ / Developer | `Server=.` hoặc `Server=TEN_MAY` |
| Đăng nhập bằng tài khoản SQL (thay cho Windows) | thay `Integrated Security=True` bằng `User ID=sa;Password=...` |

Sau khi build, file cấu hình được chép thành
`ThiTracNghiem.GUI/bin/Debug/net10.0-windows/ThiTracNghiem.GUI.dll.config`
(có thể sửa trực tiếp file này khi chỉ chạy file .exe).

---

## 5. Chạy chương trình

### Bằng Visual Studio

1. Mở `SourceCode/ThiTracNghiem.sln`.
2. Trong **Solution Explorer**, chuột phải **ThiTracNghiem.GUI** → **Set as Startup Project**.
3. Nhấn **F5** (hoặc Ctrl + F5).

### Bằng dòng lệnh

```bash
dotnet run --project SourceCode\ThiTracNghiem.GUI
```

---

## 6. Tài khoản demo

Mật khẩu của **tất cả** tài khoản mẫu: **`123456`**

| Tên đăng nhập | Họ tên | Vai trò | Lớp | Ghi chú |
|---|---|---|---|---|
| `gv01` | Lê Hữu Nhơn | Giảng viên | | Quản lý toàn bộ, xem thống kê |
| `gv02` | Trần Thị Mai | Giảng viên | | |
| `2380001` | Nguyễn Văn An | Sinh viên | Đ23HT1 | Đã thi một số đề |
| `2380002` ... `2380005` | | Sinh viên | Đ23HT1 / Đ23HT2 | Đã có kết quả thi mẫu |
| `2380006` | Đặng Thu Hà | Sinh viên | Đ23HT2 | **Chưa thi đề giữa kỳ OOP** - dùng để demo làm bài |
| `2380007` | Bùi Quốc Khánh | Sinh viên | Đ23HT1 | **Tài khoản bị khóa** - dùng để demo đăng nhập bị từ chối |

### Dữ liệu mẫu

- 4 môn học (môn *Phân tích thiết kế hệ thống* không có câu hỏi - dùng để kiểm thử tạo đề thiếu câu).
- 39 câu hỏi đang dùng + 1 câu đã ẩn, gồm 3 loại: một đáp án, nhiều đáp án, Đúng/Sai.
- 4 đề thi: 2 đề **đang mở**, 1 đề **đã đóng**, 1 đề **chưa mở**.
- 12 lượt thi đã nộp để có dữ liệu thống kê.

---

## 7. Kịch bản demo gợi ý

1. Đăng nhập `2380007` → bị từ chối vì tài khoản bị khóa.
2. Đăng nhập `gv01`:
   - **Quản lý → Ngân hàng câu hỏi**: lọc theo môn + loại câu; thêm câu *Nhiều đáp án*; thử lưu câu sai quy tắc để xem thông báo lỗi.
   - **Quản lý → Đề thi**: tạo đề cho môn *Phân tích thiết kế hệ thống* → bị chặn vì ngân hàng không đủ câu.
   - **Kết quả - Thống kê**: tra cứu theo lớp + khoảng điểm; tab *Thống kê theo đề* xem biểu đồ phổ điểm.
3. Đăng xuất, đăng nhập `2380006`:
   - **Phòng thi** → chọn *Kiểm tra giữa kỳ - Lập trình hướng đối tượng* → **Vào thi**.
   - Làm vài câu, đóng cửa sổ giữa chừng rồi vào lại → bài được mở tiếp, đáp án vẫn còn.
   - **Nộp bài** → xem điểm và xem lại từng câu đúng/sai.

---

## 8. Xử lý sự cố thường gặp

| Hiện tượng | Cách khắc phục |
|---|---|
| Mở chương trình báo *"Không kết nối được tới cơ sở dữ liệu"* | Kiểm tra đã chạy file `.sql` chưa và phần `Server=` trong App.config có đúng tên SQL Server của máy không |
| LocalDB không khởi động | Chạy lệnh `sqllocaldb start MSSQLLocalDB` |
| Lỗi chứng chỉ khi kết nối (*certificate chain*) | Giữ `TrustServerCertificate=True` trong chuỗi kết nối |
| Tiếng Việt bị lỗi khi chạy script bằng `sqlcmd` | Thêm tham số `-f 65001` |
| Sinh viên không vào được đề nào (đề đều *Đã đóng*) | Chạy lại `ThiTracNghiem.sql` để làm mới thời gian mở/đóng đề |
| Build báo lỗi không tải được gói NuGet | Kiểm tra kết nối Internet rồi build lại (Visual Studio: chuột phải Solution → *Restore NuGet Packages*) |

---

## 9. Ghi chú về công cụ hỗ trợ

> *(Sinh viên tự điền theo đúng thực tế, theo yêu cầu về tính trung thực học thuật của học phần.)*
>
> Phạm vi sử dụng công cụ AI / mã nguồn tham khảo: ...
