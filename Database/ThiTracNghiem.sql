/* =====================================================================
   HỆ THỐNG THI TRẮC NGHIỆM TRÊN MÁY TÍNH
   Script tạo CSDL, bảng, ràng buộc, view, stored procedure và dữ liệu mẫu.

   - Chạy được trên một máy chủ SQL Server mới (đã thử trên SQL Server 2025 LocalDB).
   - CẢNH BÁO: nếu CSDL ThiTracNghiem đã tồn tại, script sẽ XÓA và tạo lại.
   - Mật khẩu được lưu dạng băm SHA-256 (chuỗi HEX in hoa, 64 ký tự).
     Tất cả tài khoản mẫu có mật khẩu: 123456
   - Quy ước đáp án: chuỗi các chữ cái A-D đã sắp xếp, không lặp, không dấu phẩy.
     Ví dụ: 'A' (một đáp án), 'AC' (nhiều đáp án), 'B' (câu Đúng/Sai: A = Đúng, B = Sai).
   ===================================================================== */

USE master;
GO

IF DB_ID(N'ThiTracNghiem') IS NOT NULL
BEGIN
    ALTER DATABASE ThiTracNghiem SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE ThiTracNghiem;
END
GO

CREATE DATABASE ThiTracNghiem;
GO

USE ThiTracNghiem;
GO

/* =====================================================================
   1. BẢNG
   ===================================================================== */

-- 1.1 Tài khoản: dùng chung cho giảng viên và sinh viên
CREATE TABLE dbo.TaiKhoan
(
    MaTK         INT IDENTITY(1,1) NOT NULL,
    TenDangNhap  VARCHAR(30)       NOT NULL,   -- sinh viên: dùng MSSV
    MatKhau      CHAR(64)          NOT NULL,   -- SHA-256 dạng HEX
    HoTen        NVARCHAR(100)     NOT NULL,
    VaiTro       VARCHAR(10)       NOT NULL,
    Lop          NVARCHAR(20)      NULL,       -- giảng viên không có lớp
    Email        VARCHAR(100)      NOT NULL,
    TrangThai    BIT               NOT NULL CONSTRAINT DF_TaiKhoan_TrangThai DEFAULT (1), -- 1: hoạt động, 0: bị khóa
    NgayTao      DATETIME2(0)      NOT NULL CONSTRAINT DF_TaiKhoan_NgayTao DEFAULT (SYSDATETIME()),

    CONSTRAINT PK_TaiKhoan PRIMARY KEY (MaTK),
    CONSTRAINT UQ_TaiKhoan_TenDangNhap UNIQUE (TenDangNhap),
    CONSTRAINT UQ_TaiKhoan_Email UNIQUE (Email),
    CONSTRAINT CK_TaiKhoan_VaiTro CHECK (VaiTro IN ('GiangVien', 'SinhVien')),
    CONSTRAINT CK_TaiKhoan_Lop CHECK (VaiTro = 'GiangVien' OR Lop IS NOT NULL),   -- sinh viên bắt buộc có lớp
    CONSTRAINT CK_TaiKhoan_Email CHECK (Email LIKE '%_@_%._%'),
    CONSTRAINT CK_TaiKhoan_TenDangNhap CHECK (LEN(TenDangNhap) >= 4)
);
GO

-- 1.2 Môn học
CREATE TABLE dbo.MonHoc
(
    MaMH      INT IDENTITY(1,1) NOT NULL,
    TenMH     NVARCHAR(100)     NOT NULL,
    SoTinChi  TINYINT           NOT NULL,
    MoTa      NVARCHAR(500)     NULL,

    CONSTRAINT PK_MonHoc PRIMARY KEY (MaMH),
    CONSTRAINT UQ_MonHoc_TenMH UNIQUE (TenMH),
    CONSTRAINT CK_MonHoc_SoTinChi CHECK (SoTinChi BETWEEN 1 AND 10)
);
GO

-- 1.3 Ngân hàng câu hỏi
CREATE TABLE dbo.CauHoi
(
    MaCH        INT IDENTITY(1,1) NOT NULL,
    MaMH        INT               NOT NULL,
    NoiDung     NVARCHAR(1000)    NOT NULL,
    PhuongAnA   NVARCHAR(300)     NOT NULL,
    PhuongAnB   NVARCHAR(300)     NOT NULL,
    PhuongAnC   NVARCHAR(300)     NULL,        -- NULL với câu Đúng/Sai
    PhuongAnD   NVARCHAR(300)     NULL,
    DapAnDung   VARCHAR(4)        NOT NULL,
    LoaiCauHoi  VARCHAR(10)       NOT NULL,
    DoKho       VARCHAR(10)       NOT NULL,
    TrangThai   BIT               NOT NULL CONSTRAINT DF_CauHoi_TrangThai DEFAULT (1), -- 0: đã ẩn (xóa mềm)
    NguoiTao    INT               NOT NULL,
    NgayTao     DATETIME2(0)      NOT NULL CONSTRAINT DF_CauHoi_NgayTao DEFAULT (SYSDATETIME()),

    CONSTRAINT PK_CauHoi PRIMARY KEY (MaCH),
    CONSTRAINT FK_CauHoi_MonHoc FOREIGN KEY (MaMH) REFERENCES dbo.MonHoc (MaMH),
    CONSTRAINT FK_CauHoi_TaiKhoan FOREIGN KEY (NguoiTao) REFERENCES dbo.TaiKhoan (MaTK),
    CONSTRAINT CK_CauHoi_LoaiCauHoi CHECK (LoaiCauHoi IN ('MotDapAn', 'NhieuDapAn', 'DungSai')),
    CONSTRAINT CK_CauHoi_DoKho CHECK (DoKho IN ('De', 'TrungBinh', 'Kho')),
    CONSTRAINT CK_CauHoi_NoiDung CHECK (LEN(LTRIM(NoiDung)) > 0),
    -- Đáp án phải khớp với loại câu hỏi
    CONSTRAINT CK_CauHoi_DapAnTheoLoai CHECK
    (
        (LoaiCauHoi = 'MotDapAn'
            AND DapAnDung IN ('A', 'B', 'C', 'D')
            AND PhuongAnC IS NOT NULL AND PhuongAnD IS NOT NULL)
     OR (LoaiCauHoi = 'NhieuDapAn'
            AND DapAnDung IN ('AB', 'AC', 'AD', 'BC', 'BD', 'CD', 'ABC', 'ABD', 'ACD', 'BCD', 'ABCD')
            AND PhuongAnC IS NOT NULL AND PhuongAnD IS NOT NULL)
     OR (LoaiCauHoi = 'DungSai'
            AND DapAnDung IN ('A', 'B')
            AND PhuongAnC IS NULL AND PhuongAnD IS NULL)
    )
);
GO

-- 1.4 Đề thi (lưu cấu trúc đề: số câu theo từng độ khó)
CREATE TABLE dbo.DeThi
(
    MaDe           INT IDENTITY(1,1) NOT NULL,
    MaMH           INT               NOT NULL,
    TenDe          NVARCHAR(200)     NOT NULL,
    ThoiGianLam    SMALLINT          NOT NULL,   -- phút
    SoCauDe        TINYINT           NOT NULL CONSTRAINT DF_DeThi_SoCauDe DEFAULT (0),
    SoCauTB        TINYINT           NOT NULL CONSTRAINT DF_DeThi_SoCauTB DEFAULT (0),
    SoCauKho       TINYINT           NOT NULL CONSTRAINT DF_DeThi_SoCauKho DEFAULT (0),
    ThoiGianMo     DATETIME2(0)      NOT NULL,
    ThoiGianDong   DATETIME2(0)      NOT NULL,
    SoLanThiToiDa  TINYINT           NOT NULL CONSTRAINT DF_DeThi_SoLanThiToiDa DEFAULT (1),
    NguoiTao       INT               NOT NULL,
    NgayTao        DATETIME2(0)      NOT NULL CONSTRAINT DF_DeThi_NgayTao DEFAULT (SYSDATETIME()),

    CONSTRAINT PK_DeThi PRIMARY KEY (MaDe),
    CONSTRAINT FK_DeThi_MonHoc FOREIGN KEY (MaMH) REFERENCES dbo.MonHoc (MaMH),
    CONSTRAINT FK_DeThi_TaiKhoan FOREIGN KEY (NguoiTao) REFERENCES dbo.TaiKhoan (MaTK),
    CONSTRAINT CK_DeThi_ThoiGianLam CHECK (ThoiGianLam BETWEEN 5 AND 180),
    CONSTRAINT CK_DeThi_TongSoCau CHECK (SoCauDe + SoCauTB + SoCauKho BETWEEN 1 AND 100),
    CONSTRAINT CK_DeThi_KhoangThoiGian CHECK (ThoiGianDong > ThoiGianMo),
    CONSTRAINT CK_DeThi_SoLanThiToiDa CHECK (SoLanThiToiDa BETWEEN 1 AND 10)
);
GO

-- 1.5 Lượt thi của sinh viên
CREATE TABLE dbo.LuotThi
(
    MaLT            INT IDENTITY(1,1) NOT NULL,
    MaDe            INT               NOT NULL,
    MaTK            INT               NOT NULL,
    ThoiGianBatDau  DATETIME2(0)      NOT NULL CONSTRAINT DF_LuotThi_BatDau DEFAULT (SYSDATETIME()),
    ThoiGianNop     DATETIME2(0)      NULL,
    SoCauDung       TINYINT           NULL,
    Diem            DECIMAL(4,2)      NULL,
    TrangThai       VARCHAR(10)       NOT NULL CONSTRAINT DF_LuotThi_TrangThai DEFAULT ('DangLam'),

    CONSTRAINT PK_LuotThi PRIMARY KEY (MaLT),
    CONSTRAINT FK_LuotThi_DeThi FOREIGN KEY (MaDe) REFERENCES dbo.DeThi (MaDe),
    CONSTRAINT FK_LuotThi_TaiKhoan FOREIGN KEY (MaTK) REFERENCES dbo.TaiKhoan (MaTK),
    CONSTRAINT CK_LuotThi_TrangThai CHECK (TrangThai IN ('DangLam', 'DaNop')),
    CONSTRAINT CK_LuotThi_Diem CHECK (Diem BETWEEN 0 AND 10),
    CONSTRAINT CK_LuotThi_ThoiGianNop CHECK (ThoiGianNop IS NULL OR ThoiGianNop >= ThoiGianBatDau),
    -- Đang làm: chưa có giờ nộp, chưa có điểm. Đã nộp: bắt buộc có giờ nộp và điểm.
    CONSTRAINT CK_LuotThi_NhatQuan CHECK
    (
        (TrangThai = 'DangLam' AND ThoiGianNop IS NULL AND Diem IS NULL AND SoCauDung IS NULL)
     OR (TrangThai = 'DaNop' AND ThoiGianNop IS NOT NULL AND Diem IS NOT NULL AND SoCauDung IS NOT NULL)
    )
);
GO

-- 1.6 Chi tiết lượt thi: các câu được bốc cho lượt thi và đáp án sinh viên chọn
CREATE TABLE dbo.ChiTietLuotThi
(
    MaLT       INT         NOT NULL,
    MaCH       INT         NOT NULL,
    ThuTu      SMALLINT    NOT NULL,      -- thứ tự câu hiển thị trong bài
    DapAnChon  VARCHAR(4)  NULL,          -- NULL: bỏ trống
    LaDung     BIT         NULL,          -- NULL: chưa chấm

    CONSTRAINT PK_ChiTietLuotThi PRIMARY KEY (MaLT, MaCH),
    -- Composition: xóa lượt thi thì xóa luôn chi tiết
    CONSTRAINT FK_ChiTietLuotThi_LuotThi FOREIGN KEY (MaLT) REFERENCES dbo.LuotThi (MaLT) ON DELETE CASCADE,
    -- Không cho xóa câu hỏi đã được dùng trong bài thi (phải ẩn thay vì xóa)
    CONSTRAINT FK_ChiTietLuotThi_CauHoi FOREIGN KEY (MaCH) REFERENCES dbo.CauHoi (MaCH),
    CONSTRAINT UQ_ChiTietLuotThi_ThuTu UNIQUE (MaLT, ThuTu),
    CONSTRAINT CK_ChiTietLuotThi_ThuTu CHECK (ThuTu >= 1),
    CONSTRAINT CK_ChiTietLuotThi_DapAnChon CHECK
    (
        DapAnChon IS NULL OR DapAnChon IN
        ('A', 'B', 'C', 'D', 'AB', 'AC', 'AD', 'BC', 'BD', 'CD', 'ABC', 'ABD', 'ACD', 'BCD', 'ABCD')
    )
);
GO

-- Chỉ mục hỗ trợ bốc câu hỏi và thống kê
CREATE INDEX IX_CauHoi_MonHoc_DoKho ON dbo.CauHoi (MaMH, DoKho, TrangThai);
CREATE INDEX IX_LuotThi_DeThi_TaiKhoan ON dbo.LuotThi (MaDe, MaTK);
CREATE INDEX IX_ChiTietLuotThi_CauHoi ON dbo.ChiTietLuotThi (MaCH);
GO

/* =====================================================================
   2. VIEW
   ===================================================================== */

-- Kết quả thi đầy đủ (join 4 bảng), dùng cho màn hình kết quả và báo cáo
CREATE VIEW dbo.vw_KetQuaThi
AS
SELECT
    lt.MaLT,
    lt.MaDe,
    dt.TenDe,
    mh.MaMH,
    mh.TenMH,
    tk.MaTK,
    tk.TenDangNhap                                        AS MaSV,
    tk.HoTen,
    tk.Lop,
    lt.ThoiGianBatDau,
    lt.ThoiGianNop,
    CAST(DATEDIFF(SECOND, lt.ThoiGianBatDau, lt.ThoiGianNop) / 60.0 AS DECIMAL(6,1)) AS SoPhutLam,
    lt.SoCauDung,
    (SELECT COUNT(*) FROM dbo.ChiTietLuotThi ct WHERE ct.MaLT = lt.MaLT) AS TongSoCau,
    lt.Diem,
    lt.TrangThai,
    CASE WHEN lt.TrangThai = 'DaNop' AND lt.Diem >= 5 THEN N'Đạt'
         WHEN lt.TrangThai = 'DaNop'                  THEN N'Chưa đạt'
         ELSE N'Đang làm' END                           AS KetQua
FROM dbo.LuotThi lt
JOIN dbo.DeThi   dt ON dt.MaDe = lt.MaDe
JOIN dbo.MonHoc  mh ON mh.MaMH = dt.MaMH
JOIN dbo.TaiKhoan tk ON tk.MaTK = lt.MaTK;
GO

/* =====================================================================
   3. STORED PROCEDURE
   ===================================================================== */

-- 3.1 Tạo lượt thi mới: bốc ngẫu nhiên câu hỏi theo cấu trúc đề, trong một transaction.
--     Các quy tắc thời gian mở đề / số lần thi được kiểm tra ở tầng BLL trước khi gọi.
--     @ThoiGianBatDau do ứng dụng truyền vào để giờ bắt đầu và đồng hồ đếm ngược dùng chung một nguồn thời gian
--     (tránh lệch giờ giữa máy chạy ứng dụng và máy chủ CSDL). NULL = dùng giờ của máy chủ.
CREATE PROCEDURE dbo.sp_TaoLuotThi
    @MaDe INT,
    @MaTK INT,
    @ThoiGianBatDau DATETIME2(0) = NULL,
    @MaLT INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;   -- có lỗi thì tự rollback toàn bộ

    DECLARE @MaMH INT, @SoCauDe INT, @SoCauTB INT, @SoCauKho INT;

    SELECT @MaMH = MaMH, @SoCauDe = SoCauDe, @SoCauTB = SoCauTB, @SoCauKho = SoCauKho
    FROM dbo.DeThi
    WHERE MaDe = @MaDe;

    IF @MaMH IS NULL
        THROW 50001, N'Đề thi không tồn tại.', 1;

    IF (SELECT COUNT(*) FROM dbo.CauHoi WHERE MaMH = @MaMH AND TrangThai = 1 AND DoKho = 'De')        < @SoCauDe
    OR (SELECT COUNT(*) FROM dbo.CauHoi WHERE MaMH = @MaMH AND TrangThai = 1 AND DoKho = 'TrungBinh') < @SoCauTB
    OR (SELECT COUNT(*) FROM dbo.CauHoi WHERE MaMH = @MaMH AND TrangThai = 1 AND DoKho = 'Kho')       < @SoCauKho
        THROW 50002, N'Ngân hàng câu hỏi không đủ số câu theo cấu trúc đề.', 1;

    BEGIN TRANSACTION;

        INSERT INTO dbo.LuotThi (MaDe, MaTK, ThoiGianBatDau)
        VALUES (@MaDe, @MaTK, COALESCE(@ThoiGianBatDau, SYSDATETIME()));
        SET @MaLT = CAST(SCOPE_IDENTITY() AS INT);

        INSERT INTO dbo.ChiTietLuotThi (MaLT, MaCH, ThuTu)
        SELECT @MaLT, x.MaCH, ROW_NUMBER() OVER (ORDER BY NEWID())   -- trộn thứ tự câu
        FROM
        (
            SELECT MaCH FROM (SELECT TOP (@SoCauDe)  MaCH FROM dbo.CauHoi
                              WHERE MaMH = @MaMH AND TrangThai = 1 AND DoKho = 'De'        ORDER BY NEWID()) d
            UNION ALL
            SELECT MaCH FROM (SELECT TOP (@SoCauTB)  MaCH FROM dbo.CauHoi
                              WHERE MaMH = @MaMH AND TrangThai = 1 AND DoKho = 'TrungBinh' ORDER BY NEWID()) t
            UNION ALL
            SELECT MaCH FROM (SELECT TOP (@SoCauKho) MaCH FROM dbo.CauHoi
                              WHERE MaMH = @MaMH AND TrangThai = 1 AND DoKho = 'Kho'       ORDER BY NEWID()) k
        ) x;

    COMMIT TRANSACTION;
END
GO

-- 3.2 Tìm kiếm câu hỏi theo nhiều tiêu chí (tham số NULL = bỏ qua tiêu chí đó)
CREATE PROCEDURE dbo.sp_TimKiemCauHoi
    @MaMH        INT           = NULL,
    @DoKho       VARCHAR(10)   = NULL,
    @LoaiCauHoi  VARCHAR(10)   = NULL,
    @TuKhoa      NVARCHAR(200) = NULL,
    @HienCauDaAn BIT           = 0
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ch.MaCH, ch.MaMH, mh.TenMH, ch.NoiDung,
           ch.PhuongAnA, ch.PhuongAnB, ch.PhuongAnC, ch.PhuongAnD,
           ch.DapAnDung, ch.LoaiCauHoi, ch.DoKho, ch.TrangThai, ch.NguoiTao, ch.NgayTao
    FROM dbo.CauHoi ch
    JOIN dbo.MonHoc mh ON mh.MaMH = ch.MaMH
    WHERE (@MaMH IS NULL       OR ch.MaMH = @MaMH)
      AND (@DoKho IS NULL      OR ch.DoKho = @DoKho)
      AND (@LoaiCauHoi IS NULL OR ch.LoaiCauHoi = @LoaiCauHoi)
      AND (@TuKhoa IS NULL     OR ch.NoiDung LIKE N'%' + @TuKhoa + N'%')
      AND (@HienCauDaAn = 1    OR ch.TrangThai = 1)
    ORDER BY ch.MaMH, ch.MaCH
    OPTION (RECOMPILE);
END
GO

-- 3.3 Thống kê điểm theo đề thi (chỉ tính lượt đã nộp)
CREATE PROCEDURE dbo.sp_ThongKeDiemTheoDe
    @MaDe INT = NULL     -- NULL: tất cả đề
AS
BEGIN
    SET NOCOUNT ON;

    SELECT dt.MaDe,
           dt.TenDe,
           mh.TenMH,
           COUNT(lt.MaLT)                                          AS SoLuotThi,
           COUNT(DISTINCT lt.MaTK)                                 AS SoSinhVien,
           CAST(AVG(lt.Diem) AS DECIMAL(4,2))                      AS DiemTrungBinh,
           MAX(lt.Diem)                                            AS DiemCaoNhat,
           MIN(lt.Diem)                                            AS DiemThapNhat,
           SUM(CASE WHEN lt.Diem >= 5 THEN 1 ELSE 0 END)           AS SoLuotDat,
           CAST(CASE WHEN COUNT(lt.MaLT) = 0 THEN 0
                     ELSE 100.0 * SUM(CASE WHEN lt.Diem >= 5 THEN 1 ELSE 0 END) / COUNT(lt.MaLT)
                END AS DECIMAL(5,2))                               AS TiLeDat
    FROM dbo.DeThi dt
    JOIN dbo.MonHoc mh ON mh.MaMH = dt.MaMH
    LEFT JOIN dbo.LuotThi lt ON lt.MaDe = dt.MaDe AND lt.TrangThai = 'DaNop'
    WHERE @MaDe IS NULL OR dt.MaDe = @MaDe
    GROUP BY dt.MaDe, dt.TenDe, mh.TenMH
    ORDER BY dt.MaDe;
END
GO

-- 3.4 Phổ điểm của một đề (số lượt thi theo từng khoảng điểm) - dùng để vẽ biểu đồ
CREATE PROCEDURE dbo.sp_PhoDiem
    @MaDe INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT k.ThuTu, k.KhoangDiem, COUNT(lt.MaLT) AS SoLuot
    FROM (VALUES (1, N'0 - dưới 2',   0.0,  2.0),
                 (2, N'2 - dưới 4',   2.0,  4.0),
                 (3, N'4 - dưới 5',   4.0,  5.0),
                 (4, N'5 - dưới 6.5', 5.0,  6.5),
                 (5, N'6.5 - dưới 8', 6.5,  8.0),
                 (6, N'8 - 10',       8.0, 10.01)) AS k (ThuTu, KhoangDiem, TuDiem, DenDiem)
    LEFT JOIN dbo.LuotThi lt
           ON lt.MaDe = @MaDe AND lt.TrangThai = 'DaNop'
          AND lt.Diem >= k.TuDiem AND lt.Diem < k.DenDiem
    GROUP BY k.ThuTu, k.KhoangDiem
    ORDER BY k.ThuTu;
END
GO

-- 3.5 Các câu hỏi bị làm sai nhiều nhất
CREATE PROCEDURE dbo.sp_TopCauHoiSaiNhieu
    @MaMH    INT = NULL,
    @SoLuong INT = 10
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP (@SoLuong)
           ch.MaCH,
           mh.TenMH,
           ch.NoiDung,
           ch.DoKho,
           COUNT(*)                                     AS SoLanXuatHien,
           SUM(CASE WHEN ct.LaDung = 0 THEN 1 ELSE 0 END) AS SoLanSai,
           CAST(100.0 * SUM(CASE WHEN ct.LaDung = 0 THEN 1 ELSE 0 END) / COUNT(*) AS DECIMAL(5,2)) AS TiLeSai
    FROM dbo.ChiTietLuotThi ct
    JOIN dbo.LuotThi lt ON lt.MaLT = ct.MaLT AND lt.TrangThai = 'DaNop'
    JOIN dbo.CauHoi  ch ON ch.MaCH = ct.MaCH
    JOIN dbo.MonHoc  mh ON mh.MaMH = ch.MaMH
    WHERE @MaMH IS NULL OR ch.MaMH = @MaMH
    GROUP BY ch.MaCH, mh.TenMH, ch.NoiDung, ch.DoKho
    ORDER BY TiLeSai DESC, SoLanSai DESC, ch.MaCH;
END
GO

/* =====================================================================
   4. DỮ LIỆU MẪU
   ===================================================================== */

-- 4.1 Tài khoản (mật khẩu mặc định: 123456)
DECLARE @MatKhauMacDinh CHAR(64) = CONVERT(CHAR(64), HASHBYTES('SHA2_256', CAST('123456' AS VARCHAR(100))), 2);

INSERT INTO dbo.TaiKhoan (TenDangNhap, MatKhau, HoTen, VaiTro, Lop, Email) VALUES
('gv01',       @MatKhauMacDinh, N'Lê Hữu Nhơn',        'GiangVien', NULL,     'gv01@ldxh.edu.vn'),
('gv02',       @MatKhauMacDinh, N'Trần Thị Mai',       'GiangVien', NULL,     'gv02@ldxh.edu.vn'),
('2380001',    @MatKhauMacDinh, N'Nguyễn Văn An',      'SinhVien',  N'Đ23HT1', '2380001@sv.ldxh.edu.vn'),
('2380002',    @MatKhauMacDinh, N'Trần Thị Bình',      'SinhVien',  N'Đ23HT1', '2380002@sv.ldxh.edu.vn'),
('2380003',    @MatKhauMacDinh, N'Lê Hoàng Cường',     'SinhVien',  N'Đ23HT1', '2380003@sv.ldxh.edu.vn'),
('2380004',    @MatKhauMacDinh, N'Phạm Ngọc Dung',     'SinhVien',  N'Đ23HT2', '2380004@sv.ldxh.edu.vn'),
('2380005',    @MatKhauMacDinh, N'Võ Minh Đức',        'SinhVien',  N'Đ23HT2', '2380005@sv.ldxh.edu.vn'),
('2380006',    @MatKhauMacDinh, N'Đặng Thu Hà',        'SinhVien',  N'Đ23HT2', '2380006@sv.ldxh.edu.vn');

-- Một tài khoản bị khóa để kiểm thử đăng nhập
INSERT INTO dbo.TaiKhoan (TenDangNhap, MatKhau, HoTen, VaiTro, Lop, Email, TrangThai) VALUES
('2380007',    @MatKhauMacDinh, N'Bùi Quốc Khánh',     'SinhVien',  N'Đ23HT1', '2380007@sv.ldxh.edu.vn', 0);
GO

-- 4.2 Môn học
INSERT INTO dbo.MonHoc (TenMH, SoTinChi, MoTa) VALUES
(N'Lập trình hướng đối tượng', 3, N'C#, lớp và đối tượng, kế thừa, đa hình, interface'),
(N'Cơ sở dữ liệu',              3, N'Mô hình quan hệ, SQL, chuẩn hóa, giao tác'),
(N'Mạng máy tính',              3, N'Mô hình OSI, TCP/IP, địa chỉ IP, các giao thức cơ bản'),
(N'Phân tích thiết kế hệ thống', 3, N'Chưa có câu hỏi - dùng để kiểm thử khi tạo đề');
GO

-- 4.3 Câu hỏi
DECLARE @GV1 INT = (SELECT MaTK FROM dbo.TaiKhoan WHERE TenDangNhap = 'gv01');
DECLARE @GV2 INT = (SELECT MaTK FROM dbo.TaiKhoan WHERE TenDangNhap = 'gv02');
DECLARE @OOP INT = (SELECT MaMH FROM dbo.MonHoc WHERE TenMH = N'Lập trình hướng đối tượng');
DECLARE @CSDL INT = (SELECT MaMH FROM dbo.MonHoc WHERE TenMH = N'Cơ sở dữ liệu');
DECLARE @MANG INT = (SELECT MaMH FROM dbo.MonHoc WHERE TenMH = N'Mạng máy tính');

INSERT INTO dbo.CauHoi (MaMH, NoiDung, PhuongAnA, PhuongAnB, PhuongAnC, PhuongAnD, DapAnDung, LoaiCauHoi, DoKho, NguoiTao) VALUES
-- ---------- Lập trình hướng đối tượng: 6 dễ, 6 trung bình, 4 khó ----------
(@OOP, N'Từ khóa nào dùng để khai báo một lớp trong C#?',
       N'class', N'struct', N'object', N'new', 'A', 'MotDapAn', 'De', @GV1),
(@OOP, N'Một lớp trong C# có thể kế thừa trực tiếp từ nhiều lớp cha.',
       N'Đúng', N'Sai', NULL, NULL, 'B', 'DungSai', 'De', @GV1),
(@OOP, N'Phạm vi truy cập nào chỉ cho phép truy cập bên trong chính lớp đó?',
       N'public', N'private', N'protected', N'internal', 'B', 'MotDapAn', 'De', @GV1),
(@OOP, N'Phương thức khởi tạo (constructor) trong C# có đặc điểm nào?',
       N'Trùng tên lớp và không có kiểu trả về', N'Luôn trả về void', N'Bắt buộc phải là static', N'Bắt buộc phải có tham số', 'A', 'MotDapAn', 'De', @GV1),
(@OOP, N'Property trong C# có thể chỉ có get mà không có set.',
       N'Đúng', N'Sai', NULL, NULL, 'A', 'DungSai', 'De', @GV1),
(@OOP, N'Những tính chất nào là tính chất cơ bản của lập trình hướng đối tượng?',
       N'Đóng gói', N'Kế thừa', N'Biên dịch', N'Đa hình', 'ABD', 'NhieuDapAn', 'De', @GV1),

(@OOP, N'Từ khóa nào cho phép lớp con ghi đè phương thức virtual của lớp cha?',
       N'new', N'override', N'sealed', N'static', 'B', 'MotDapAn', 'TrungBinh', @GV1),
(@OOP, N'Lớp abstract trong C# có đặc điểm nào?',
       N'Có thể tạo đối tượng trực tiếp bằng new', N'Không thể tạo đối tượng trực tiếp', N'Không được chứa phương thức có thân', N'Không được có constructor', 'B', 'MotDapAn', 'TrungBinh', @GV1),
(@OOP, N'Những kiểu nào là kiểu tham chiếu (reference type) trong C#?',
       N'class', N'int', N'string', N'struct', 'AC', 'NhieuDapAn', 'TrungBinh', @GV1),
(@OOP, N'Một lớp trong C# có thể cài đặt (implement) nhiều interface.',
       N'Đúng', N'Sai', NULL, NULL, 'A', 'DungSai', 'TrungBinh', @GV1),
(@OOP, N'Lớp List<T> thuộc namespace nào?',
       N'System.Collections.Generic', N'System.Linq', N'System.IO', N'System.Data', 'A', 'MotDapAn', 'TrungBinh', @GV1),
(@OOP, N'Những khối nào có thể đi kèm ngay sau khối try?',
       N'catch', N'finally', N'else', N'default', 'AB', 'NhieuDapAn', 'TrungBinh', @GV1),

(@OOP, N'Quan hệ composition khác aggregation ở điểm nào?',
       N'Đối tượng thành phần không tồn tại độc lập khi đối tượng tổng thể bị hủy', N'Composition luôn là quan hệ n-n',
       N'Aggregation không cho phép đối tượng thành phần được chia sẻ', N'Hai quan hệ hoàn toàn giống nhau', 'A', 'MotDapAn', 'Kho', @GV1),
(@OOP, N'Những phát biểu nào đúng về đa hình (polymorphism)?',
       N'Cùng một lời gọi phương thức có thể thực thi các cài đặt khác nhau', N'Có thể đạt được thông qua override',
       N'Có thể đạt được thông qua interface', N'Bắt buộc phải dùng từ khóa static', 'ABC', 'NhieuDapAn', 'Kho', @GV1),
(@OOP, N'Gọi một phương thức virtual qua biến kiểu lớp cha đang tham chiếu tới đối tượng lớp con (lớp con có override). Phương thức nào được thực thi?',
       N'Phương thức của lớp cha', N'Phương thức của lớp con', N'Báo lỗi biên dịch', N'Chọn ngẫu nhiên lúc chạy', 'B', 'MotDapAn', 'Kho', @GV1),
(@OOP, N'Lớp được đánh dấu sealed vẫn có thể được kế thừa nếu lớp con nằm cùng assembly.',
       N'Đúng', N'Sai', NULL, NULL, 'B', 'DungSai', 'Kho', @GV1),

-- ---------- Cơ sở dữ liệu: 5 dễ, 5 trung bình, 3 khó ----------
(@CSDL, N'Ràng buộc nào đảm bảo mỗi dòng trong bảng là duy nhất và không được NULL?',
        N'FOREIGN KEY', N'PRIMARY KEY', N'CHECK', N'DEFAULT', 'B', 'MotDapAn', 'De', @GV2),
(@CSDL, N'Câu lệnh SQL nào dùng để truy vấn (lấy) dữ liệu?',
        N'INSERT', N'UPDATE', N'SELECT', N'DELETE', 'C', 'MotDapAn', 'De', @GV2),
(@CSDL, N'Một bảng có thể có nhiều khóa ngoại.',
        N'Đúng', N'Sai', NULL, NULL, 'A', 'DungSai', 'De', @GV2),
(@CSDL, N'Mệnh đề nào dùng để lọc các nhóm sau khi GROUP BY?',
        N'WHERE', N'HAVING', N'ORDER BY', N'FROM', 'B', 'MotDapAn', 'De', @GV2),
(@CSDL, N'Những câu lệnh nào thuộc nhóm thao tác dữ liệu (DML)?',
        N'INSERT', N'CREATE TABLE', N'UPDATE', N'DROP TABLE', 'AC', 'NhieuDapAn', 'De', @GV2),

(@CSDL, N'Kiểu JOIN nào trả về tất cả các dòng của bảng bên trái, kể cả khi không có dòng khớp ở bảng bên phải?',
        N'INNER JOIN', N'LEFT JOIN', N'RIGHT JOIN', N'CROSS JOIN', 'B', 'MotDapAn', 'TrungBinh', @GV2),
(@CSDL, N'Những hàm nào là hàm tổng hợp (aggregate function)?',
        N'COUNT', N'SUM', N'UPPER', N'AVG', 'ABD', 'NhieuDapAn', 'TrungBinh', @GV2),
(@CSDL, N'Trong SQL Server, cột có ràng buộc UNIQUE được phép chứa giá trị NULL (tối đa một dòng).',
        N'Đúng', N'Sai', NULL, NULL, 'A', 'DungSai', 'TrungBinh', @GV2),
(@CSDL, N'Dạng chuẩn 1 (1NF) yêu cầu điều gì?',
        N'Mọi thuộc tính đều có giá trị nguyên tố', N'Không có phụ thuộc bắc cầu', N'Không có phụ thuộc bộ phận vào khóa', N'Mọi bảng phải có khóa ngoại', 'A', 'MotDapAn', 'TrungBinh', @GV2),
(@CSDL, N'Sử dụng tham số trong SqlCommand giúp phòng chống loại tấn công nào?',
        N'Cross-site scripting (XSS)', N'SQL Injection', N'Từ chối dịch vụ (DDoS)', N'Lừa đảo (Phishing)', 'B', 'MotDapAn', 'TrungBinh', @GV2),

(@CSDL, N'So với dạng chuẩn 2 (2NF), dạng chuẩn 3 (3NF) loại bỏ thêm loại phụ thuộc nào?',
        N'Phụ thuộc bộ phận', N'Phụ thuộc bắc cầu', N'Phụ thuộc đa trị', N'Phụ thuộc hàm đầy đủ', 'B', 'MotDapAn', 'Kho', @GV2),
(@CSDL, N'Những tính chất nào thuộc nhóm ACID của giao tác (transaction)?',
        N'Atomicity', N'Consistency', N'Integrity', N'Durability', 'ABD', 'NhieuDapAn', 'Kho', @GV2),
(@CSDL, N'Tùy chọn ON DELETE CASCADE trên khóa ngoại có tác dụng gì?',
        N'Xóa dòng ở bảng cha thì tự động xóa các dòng liên quan ở bảng con', N'Ngăn không cho xóa dòng ở bảng cha',
        N'Đặt giá trị khóa ngoại ở bảng con về NULL', N'Xóa toàn bộ bảng con', 'A', 'MotDapAn', 'Kho', @GV2),

-- ---------- Mạng máy tính: 4 dễ, 4 trung bình, 2 khó ----------
(@MANG, N'Giao thức nào được dùng để truyền tải trang web?',
        N'HTTP', N'SMTP', N'FTP', N'SNMP', 'A', 'MotDapAn', 'De', @GV2),
(@MANG, N'Địa chỉ IPv4 có độ dài 32 bit.',
        N'Đúng', N'Sai', NULL, NULL, 'A', 'DungSai', 'De', @GV2),
(@MANG, N'Thiết bị nào hoạt động ở tầng 3 (Network) của mô hình OSI?',
        N'Hub', N'Switch', N'Router', N'Repeater', 'C', 'MotDapAn', 'De', @GV2),
(@MANG, N'Mô hình OSI gồm bao nhiêu tầng?',
        N'4', N'5', N'7', N'8', 'C', 'MotDapAn', 'De', @GV2),

(@MANG, N'Cổng (port) mặc định của HTTPS là bao nhiêu?',
        N'21', N'80', N'443', N'3306', 'C', 'MotDapAn', 'TrungBinh', @GV2),
(@MANG, N'Những giao thức nào hoạt động ở tầng Transport?',
        N'TCP', N'UDP', N'IP', N'ICMP', 'AB', 'NhieuDapAn', 'TrungBinh', @GV2),
(@MANG, N'Giao thức UDP đảm bảo dữ liệu đến đích đúng thứ tự.',
        N'Đúng', N'Sai', NULL, NULL, 'B', 'DungSai', 'TrungBinh', @GV2),
(@MANG, N'Dịch vụ DNS dùng để làm gì?',
        N'Phân giải tên miền thành địa chỉ IP', N'Cấp phát địa chỉ IP tự động', N'Mã hóa dữ liệu truyền đi', N'Định tuyến gói tin', 'A', 'MotDapAn', 'TrungBinh', @GV2),

(@MANG, N'Một mạng con có subnet mask /26 có bao nhiêu địa chỉ host khả dụng?',
        N'62', N'64', N'30', N'126', 'A', 'MotDapAn', 'Kho', @GV2),
(@MANG, N'Những dải địa chỉ nào là địa chỉ IP private?',
        N'10.0.0.0/8', N'172.16.0.0/12', N'192.168.0.0/16', N'8.8.8.0/24', 'ABC', 'NhieuDapAn', 'Kho', @GV2);

-- Một câu hỏi đã ẩn (xóa mềm) - không được bốc vào đề
INSERT INTO dbo.CauHoi (MaMH, NoiDung, PhuongAnA, PhuongAnB, PhuongAnC, PhuongAnD, DapAnDung, LoaiCauHoi, DoKho, TrangThai, NguoiTao) VALUES
(@OOP, N'(Câu cũ, đã ẩn) Từ khóa this trong C# dùng để làm gì?',
       N'Tham chiếu tới đối tượng hiện tại', N'Tạo đối tượng mới', N'Gọi lớp cha', N'Khai báo hằng số', 'A', 'MotDapAn', 'De', 0, @GV1);
GO

-- 4.4 Đề thi (thời gian tính theo lúc chạy script để luôn có đề đang mở khi demo)
DECLARE @GV1 INT = (SELECT MaTK FROM dbo.TaiKhoan WHERE TenDangNhap = 'gv01');
DECLARE @GV2 INT = (SELECT MaTK FROM dbo.TaiKhoan WHERE TenDangNhap = 'gv02');
DECLARE @OOP INT = (SELECT MaMH FROM dbo.MonHoc WHERE TenMH = N'Lập trình hướng đối tượng');
DECLARE @CSDL INT = (SELECT MaMH FROM dbo.MonHoc WHERE TenMH = N'Cơ sở dữ liệu');
DECLARE @MANG INT = (SELECT MaMH FROM dbo.MonHoc WHERE TenMH = N'Mạng máy tính');
DECLARE @Nay DATETIME2(0) = SYSDATETIME();

INSERT INTO dbo.DeThi (MaMH, TenDe, ThoiGianLam, SoCauDe, SoCauTB, SoCauKho, ThoiGianMo, ThoiGianDong, SoLanThiToiDa, NguoiTao) VALUES
(@OOP,  N'Kiểm tra giữa kỳ - Lập trình hướng đối tượng', 15, 4, 4, 2, DATEADD(DAY, -7, @Nay),  DATEADD(MONTH, 3, @Nay),  2, @GV1),  -- đang mở
(@CSDL, N'Kiểm tra chương 1-3 - Cơ sở dữ liệu',          10, 3, 3, 2, DATEADD(DAY, -7, @Nay),  DATEADD(MONTH, 3, @Nay),  1, @GV2),  -- đang mở
(@MANG, N'Ôn tập - Mạng máy tính',                       10, 2, 2, 1, DATEADD(DAY, -40, @Nay), DATEADD(DAY, -20, @Nay),  3, @GV2),  -- đã đóng
(@OOP,  N'Thi cuối kỳ - Lập trình hướng đối tượng',      30, 6, 6, 4, DATEADD(MONTH, 2, @Nay), DATEADD(MONTH, 2, DATEADD(HOUR, 2, @Nay)), 1, @GV1); -- chưa mở
GO

-- 4.5 Lượt thi mẫu (đã nộp) để có dữ liệu thống kê.
--     Dữ liệu được sinh tất định bằng HASHBYTES nên mỗi lần chạy script đều cho cùng kết quả.
DECLARE @Nay DATETIME2(0) = SYSDATETIME();

INSERT INTO dbo.LuotThi (MaDe, MaTK, ThoiGianBatDau, ThoiGianNop, SoCauDung, Diem, TrangThai)
SELECT dt.MaDe, tk.MaTK,
       DATEADD(DAY, -v.SoNgayTruoc, @Nay),
       DATEADD(MINUTE, v.SoPhutLam, DATEADD(DAY, -v.SoNgayTruoc, @Nay)),
       0, 0, 'DaNop'          -- điểm tạm, sẽ được tính lại bên dưới
FROM (VALUES
        (N'Kiểm tra giữa kỳ - Lập trình hướng đối tượng', '2380001', 6, 12),
        (N'Kiểm tra giữa kỳ - Lập trình hướng đối tượng', '2380001', 3, 10),   -- thi lần 2
        (N'Kiểm tra giữa kỳ - Lập trình hướng đối tượng', '2380002', 5, 14),
        (N'Kiểm tra giữa kỳ - Lập trình hướng đối tượng', '2380003', 5, 15),
        (N'Kiểm tra giữa kỳ - Lập trình hướng đối tượng', '2380004', 4, 9),
        (N'Kiểm tra giữa kỳ - Lập trình hướng đối tượng', '2380005', 2, 13),
        (N'Kiểm tra chương 1-3 - Cơ sở dữ liệu',          '2380001', 4, 8),
        (N'Kiểm tra chương 1-3 - Cơ sở dữ liệu',          '2380002', 3, 10),
        (N'Kiểm tra chương 1-3 - Cơ sở dữ liệu',          '2380003', 2, 7),
        (N'Kiểm tra chương 1-3 - Cơ sở dữ liệu',          '2380006', 1, 9),
        (N'Ôn tập - Mạng máy tính',                       '2380002', 30, 6),
        (N'Ôn tập - Mạng máy tính',                       '2380004', 30, 8)
     ) AS v (TenDe, TenDangNhap, SoNgayTruoc, SoPhutLam)
JOIN dbo.DeThi   dt ON dt.TenDe = v.TenDe
JOIN dbo.TaiKhoan tk ON tk.TenDangNhap = v.TenDangNhap;

-- Bốc câu hỏi cho từng lượt thi theo cấu trúc đề và sinh đáp án mẫu
;WITH CauDuocBoc AS
(
    SELECT lt.MaLT, ch.MaCH, ch.DapAnDung,
           -- "trình độ" giả lập của từng sinh viên: xác suất (%) trả lời đúng
           CASE tk.TenDangNhap WHEN '2380001' THEN 75 WHEN '2380002' THEN 90 WHEN '2380003' THEN 45
                               WHEN '2380004' THEN 60 WHEN '2380005' THEN 30 ELSE 80 END AS KyNang,
           ROW_NUMBER() OVER (PARTITION BY lt.MaLT, ch.DoKho
                              ORDER BY CONVERT(BIGINT, CAST(HASHBYTES('MD5', CONCAT('chon-', lt.MaLT, '-', ch.MaCH)) AS BINARY(4)))) AS ThuTuTrongNhom,
           CASE ch.DoKho WHEN 'De' THEN dt.SoCauDe WHEN 'TrungBinh' THEN dt.SoCauTB ELSE dt.SoCauKho END AS SoCauCanLay
    FROM dbo.LuotThi lt
    JOIN dbo.DeThi   dt ON dt.MaDe = lt.MaDe
    JOIN dbo.TaiKhoan tk ON tk.MaTK = lt.MaTK
    JOIN dbo.CauHoi  ch ON ch.MaMH = dt.MaMH AND ch.TrangThai = 1
)
INSERT INTO dbo.ChiTietLuotThi (MaLT, MaCH, ThuTu, DapAnChon)
SELECT c.MaLT, c.MaCH,
       ROW_NUMBER() OVER (PARTITION BY c.MaLT
                          ORDER BY CONVERT(BIGINT, CAST(HASHBYTES('MD5', CONCAT('thutu-', c.MaLT, '-', c.MaCH)) AS BINARY(4)))),
       CASE
           WHEN CONVERT(BIGINT, CAST(HASHBYTES('MD5', CONCAT('dung-', c.MaLT, '-', c.MaCH)) AS BINARY(4))) % 100 < c.KyNang
                THEN c.DapAnDung                                                   -- trả lời đúng
           WHEN CONVERT(BIGINT, CAST(HASHBYTES('MD5', CONCAT('trong-', c.MaLT, '-', c.MaCH)) AS BINARY(4))) % 5 = 0
                THEN NULL                                                          -- bỏ trống
           WHEN c.DapAnDung = 'A' THEN 'B' ELSE 'A'                                -- trả lời sai
       END
FROM CauDuocBoc c
WHERE c.ThuTuTrongNhom <= c.SoCauCanLay;

-- Chấm từng câu
UPDATE ct
SET LaDung = CASE WHEN ct.DapAnChon = ch.DapAnDung THEN 1 ELSE 0 END
FROM dbo.ChiTietLuotThi ct
JOIN dbo.CauHoi ch ON ch.MaCH = ct.MaCH;

-- Tính điểm lượt thi: Điểm = Số câu đúng / Tổng số câu x 10
UPDATE lt
SET SoCauDung = kq.SoCauDung,
    Diem      = ROUND(10.0 * kq.SoCauDung / kq.TongSoCau, 2)
FROM dbo.LuotThi lt
JOIN (SELECT MaLT, COUNT(*) AS TongSoCau, SUM(CAST(LaDung AS INT)) AS SoCauDung
      FROM dbo.ChiTietLuotThi
      GROUP BY MaLT) kq ON kq.MaLT = lt.MaLT;
GO

PRINT N'Tạo CSDL ThiTracNghiem thành công.';
GO
