USE master;
GO

-- 1. Xóa Database cũ nếu có để làm sạch từ đầu
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'QuanLyChamCong_Simple')
    DROP DATABASE QuanLyChamCong_Simple;
GO

CREATE DATABASE QuanLyChamCong_Simple;
GO
USE QuanLyChamCong_Simple;
GO

-- =============================================
-- 2. TẠO BẢNG
-- =============================================

-- Bảng Phòng Ban
CREATE TABLE PhongBan (
    MaPhong INT IDENTITY(1,1) PRIMARY KEY,
    TenPhong NVARCHAR(100) NOT NULL
);

-- Bảng Nhân Viên
CREATE TABLE NhanVien (
    MaNV INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE NOT NULL,
    GioiTinh NVARCHAR(10) DEFAULT N'Nam',
    SDT VARCHAR(15),
    CCCD VARCHAR(20) UNIQUE NOT NULL, -- Căn cước công dân
    ChucVu NVARCHAR(50),
    MaPhong INT,
    AnhDaiDien NVARCHAR(MAX), 
    LuongTheoGio DECIMAL(18,0) DEFAULT 20000, 
    FOREIGN KEY (MaPhong) REFERENCES PhongBan(MaPhong) ON DELETE SET NULL
);

-- Bảng Tài Khoản
CREATE TABLE TaiKhoan (
    TenDangNhap VARCHAR(50) PRIMARY KEY,
    MatKhau VARCHAR(50) NOT NULL,
    Quyen INT DEFAULT 0, -- 1: Admin, 0: Nhân viên
    MaNV INT,            
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV) ON DELETE CASCADE
);

-- Bảng Chấm Công
CREATE TABLE BangChamCong (
    MaCong INT IDENTITY(1,1) PRIMARY KEY,
    MaNV INT NOT NULL,
    NgayLam DATE DEFAULT CAST(GETDATE() AS DATE), 
    GioVao DATETIME,
    AnhMinhChung NVARCHAR(MAX), 
    GioRa DATETIME,
    FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),
    CONSTRAINT UQ_ChamCong_Ngay UNIQUE (MaNV, NgayLam)
);
GO

-- =============================================
-- 3. TẠO DỮ LIỆU MẪU
-- =============================================

-- A. PHÒNG BAN
INSERT INTO PhongBan (TenPhong) VALUES 
(N'Phòng Giám Đốc'),       -- ID: 1
(N'Phòng IT - Phần mềm'),  -- ID: 2
(N'Phòng Kế Toán'),        -- ID: 3
(N'Phòng Nhân Sự'),        -- ID: 4
(N'Phòng Kinh Doanh'),     -- ID: 5
(N'Phòng Bảo Vệ');         -- ID: 6

-- B. NHÂN VIÊN (Dùng IDENTITY_INSERT để ép đúng ID cho 4 thành viên nhóm)
SET IDENTITY_INSERT NhanVien ON;

-- 1. Lý Tiểu Bảo (Admin/Giám đốc) - ID: 1
INSERT INTO NhanVien (MaNV, HoTen, NgaySinh, GioiTinh, SDT, CCCD, ChucVu, MaPhong, AnhDaiDien, LuongTheoGio) 
VALUES (1, N'Lý Tiểu Bảo', '1995-01-01', N'Nam', '0909123456', '079095000001', N'Giám đốc', 1, N'Images/Avatars/admin.jpg', 200000);

-- 2. Đặng Thanh Hòa (Dev) - ID: 2
INSERT INTO NhanVien (MaNV, HoTen, NgaySinh, GioiTinh, SDT, CCCD, ChucVu, MaPhong, AnhDaiDien, LuongTheoGio) 
VALUES (2, N'Đặng Thanh Hòa', '2000-05-20', N'Nam', '0911223344', '079200000002', N'Dev', 2, N'Images/Avatars/hoa.jpg', 50000);

-- 3. Nguyễn Thành Trung (Dev) - ID: 3
INSERT INTO NhanVien (MaNV, HoTen, NgaySinh, GioiTinh, SDT, CCCD, ChucVu, MaPhong, AnhDaiDien, LuongTheoGio) 
VALUES (3, N'Nguyễn Thành Trung', '2000-06-15', N'Nam', '0922334455', '079200000003', N'Dev', 2, N'Images/Avatars/trung.jpg', 50000);

-- 4. Trần Võ Vĩnh Thuận (Dev) - ID: 4
INSERT INTO NhanVien (MaNV, HoTen, NgaySinh, GioiTinh, SDT, CCCD, ChucVu, MaPhong, AnhDaiDien, LuongTheoGio) 
VALUES (4, N'Trần Võ Vĩnh Thuận', '2000-07-20', N'Nam', '0933445566', '079200000004', N'Dev', 2, N'Images/Avatars/thuan.jpg', 50000);

-- --- THÊM 6 NHÂN VIÊN KHÁC ĐỂ TEST BÁO CÁO ---

-- 5. Kế toán trưởng
INSERT INTO NhanVien (MaNV, HoTen, NgaySinh, GioiTinh, SDT, CCCD, ChucVu, MaPhong, AnhDaiDien, LuongTheoGio) 
VALUES (5, N'Phạm Thị Hồng', '1998-03-12', N'Nữ', '0905111222', '079198000005', N'Kế Toán Trưởng', 3, N'Images/Avatars/hong.jpg', 60000);

-- 6. Nhân sự
INSERT INTO NhanVien (MaNV, HoTen, NgaySinh, GioiTinh, SDT, CCCD, ChucVu, MaPhong, AnhDaiDien, LuongTheoGio) 
VALUES (6, N'Lê Minh Khôi', '1995-08-22', N'Nam', '0905111333', '079195000006', N'Chuyên viên NS', 4, N'Images/Avatars/khoi.jpg', 45000);

-- 7. Sales
INSERT INTO NhanVien (MaNV, HoTen, NgaySinh, GioiTinh, SDT, CCCD, ChucVu, MaPhong, AnhDaiDien, LuongTheoGio) 
VALUES (7, N'Vũ Thị Thảo', '2001-11-05', N'Nữ', '0905111444', '079201000007', N'NV Kinh Doanh', 5, N'Images/Avatars/thao_sales.jpg', 40000);

-- 8. Bảo vệ 1
INSERT INTO NhanVien (MaNV, HoTen, NgaySinh, GioiTinh, SDT, CCCD, ChucVu, MaPhong, AnhDaiDien, LuongTheoGio) 
VALUES (8, N'Bùi Văn Hùng', '1985-01-01', N'Nam', '0905111888', '079185000011', N'Bảo vệ', 6, N'Images/Avatars/hung.jpg', 25000);

-- 9. Bảo vệ 2
INSERT INTO NhanVien (MaNV, HoTen, NgaySinh, GioiTinh, SDT, CCCD, ChucVu, MaPhong, AnhDaiDien, LuongTheoGio) 
VALUES (9, N'Đỗ Đức Thắng', '1997-12-12', N'Nam', '0905111777', '079197000010', N'Bảo vệ', 6, N'Images/Avatars/thang.jpg', 25000);

-- 10. Thực tập sinh IT
INSERT INTO NhanVien (MaNV, HoTen, NgaySinh, GioiTinh, SDT, CCCD, ChucVu, MaPhong, AnhDaiDien, LuongTheoGio) 
VALUES (10, N'Hồ Thị Tuyết', '2003-09-09', N'Nữ', '0905111999', '079203000012', N'Thực tập sinh', 2, N'Images/Avatars/tuyet.jpg', 20000);

SET IDENTITY_INSERT NhanVien OFF;
GO
DBCC CHECKIDENT ('NhanVien', RESEED, 10); -- Reset bộ đếm để thêm mới bắt đầu từ 11

-- C. TÀI KHOẢN (Khớp với ID nhân viên)
INSERT INTO TaiKhoan VALUES ('admin', '123', 1, 1);   -- Lý Tiểu Bảo (Admin)
INSERT INTO TaiKhoan VALUES ('hoa', '123', 0, 2);     -- Hòa
INSERT INTO TaiKhoan VALUES ('trung', '123', 0, 3);   -- Trung
INSERT INTO TaiKhoan VALUES ('thuan', '123', 0, 4);   -- Thuận
INSERT INTO TaiKhoan VALUES ('hong', '123', 0, 5);    -- Kế toán
INSERT INTO TaiKhoan VALUES ('khoi', '123', 0, 6);    -- Nhân sự
INSERT INTO TaiKhoan VALUES ('thao', '123', 0, 7);    -- Sales
INSERT INTO TaiKhoan VALUES ('hung', '123', 0, 8);    -- Bảo vệ
INSERT INTO TaiKhoan VALUES ('thang', '123', 0, 9);   -- Bảo vệ
INSERT INTO TaiKhoan VALUES ('tuyet', '123', 0, 10);  -- TTS

-- =============================================
-- 4. DỮ LIỆU CHẤM CÔNG PHONG PHÚ (Để Test Báo Cáo)
-- =============================================
-- Giả lập dữ liệu tháng 11/2025

-- Ngày 01/11: Đi làm nghiêm túc
INSERT INTO BangChamCong (MaNV, NgayLam, GioVao, GioRa) VALUES
(2, '2025-11-01', '2025-11-01 07:55:00', '2025-11-01 17:05:00'), -- Hòa
(3, '2025-11-01', '2025-11-01 08:00:00', '2025-11-01 17:00:00'), -- Trung
(4, '2025-11-01', '2025-11-01 07:45:00', '2025-11-01 17:15:00'), -- Thuận
(8, '2025-11-01', '2025-11-01 06:00:00', '2025-11-01 18:00:00'); -- Bảo vệ làm ca dài

-- Ngày 02/11: Có người đi trễ, về sớm
INSERT INTO BangChamCong (MaNV, NgayLam, GioVao, GioRa) VALUES
(2, '2025-11-02', '2025-11-02 08:30:00', '2025-11-02 17:30:00'), -- Hòa đi trễ 30p
(3, '2025-11-02', '2025-11-02 08:00:00', '2025-11-02 16:00:00'), -- Trung về sớm 1 tiếng
(4, '2025-11-02', '2025-11-02 08:00:00', NULL),                  -- Thuận quên Check-out (Test lỗi NULL)
(8, '2025-11-02', '2025-11-02 06:00:00', '2025-11-02 18:00:00');

-- Ngày 03/11: Tăng ca
INSERT INTO BangChamCong (MaNV, NgayLam, GioVao, GioRa) VALUES
(2, '2025-11-03', '2025-11-03 08:00:00', '2025-11-03 19:00:00'), -- Hòa tăng ca 2 tiếng
(3, '2025-11-03', '2025-11-03 08:00:00', '2025-11-03 19:00:00'), -- Trung tăng ca 2 tiếng
(4, '2025-11-03', '2025-11-03 08:00:00', '2025-11-03 17:00:00');

-- Dữ liệu hôm nay và hôm qua (để test Real-time)
INSERT INTO BangChamCong (MaNV, NgayLam, GioVao, GioRa) VALUES
(2, CAST(GETDATE() AS DATE), DATEADD(HOUR, 8, CAST(CAST(GETDATE() AS DATE) AS DATETIME)), NULL), -- Hòa vừa check-in sáng nay
(1, CAST(GETDATE() AS DATE), DATEADD(HOUR, 9, CAST(CAST(GETDATE() AS DATE) AS DATETIME)), NULL); -- Sếp vừa check-in

GO

-- Kiểm tra kết quả
SELECT * FROM PhongBan;
SELECT * FROM NhanVien;
SELECT * FROM TaiKhoan;
SELECT * FROM BangChamCong;