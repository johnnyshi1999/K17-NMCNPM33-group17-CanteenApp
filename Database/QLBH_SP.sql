use master
go
use QLBH
go
--Tạo store procedure
--0. Trả về danh sách sản phẩm
IF OBJECT_ID ('SP_DanhSachSP')
IS NOT NULL
DROP PROCEDURE SP_DanhSachSP 
GO
CREATE PROCEDURE SP_DanhSachSP
AS
	SELECT SANPHAM.MaSP, SANPHAM.TenSP, SP_TrungBay.GiaBan
	FROM (SANPHAM JOIN SP_TrungBay ON SANPHAM.MASP = SP_TrungBay.MaSP)
GO

--1. Tìm sản phẩm theo mã sản phẩm
IF OBJECT_ID ('SP_SanPhamTheoMa')
IS NOT NULL
DROP PROCEDURE SP_SanPhamTheoMa 
GO
CREATE PROCEDURE SP_SanPhamTheoMa @MaSP nvarchar(8)
AS
	SELECT * 
	FROM (SANPHAM JOIN SP_TrungBay ON SANPHAM.MASP = SP_TrungBay.MaSP) WHERE @MaSP = SANPHAM.MaSP
GO
--2. Tìm sản phẩm theo tên sản phẩm
IF OBJECT_ID ('SP_SanPhamTheoTen')
IS NOT NULL
DROP PROCEDURE SP_SanPhamTheoTen 
GO
CREATE PROCEDURE SP_SanPhamTheoTen @TenSP nvarchar(100)
AS
	SELECT * 
	FROM (SANPHAM JOIN SP_TrungBay ON SANPHAM.MASP = SP_TrungBay.MaSP)
	WHERE '%' + @TenSP + '%' like TenSP
GO
--3. Tìm nhân viên theo mã nhân viên
IF OBJECT_ID ('SP_NVTheoMa')
IS NOT NULL
DROP PROCEDURE SP_NVTheoMa 
GO
CREATE PROCEDURE SP_NVTheoMa @MaNV nvarchar(4)
AS
	SELECT * FROM NHAN_VIEN WHERE @MaNV = MaNV
GO
--4. Tìm nhân viên theo tên nhân viên
IF OBJECT_ID ('SP_NVTheoTen')
IS NOT NULL
DROP PROCEDURE SP_NVTheoTen
GO
CREATE PROCEDURE SP_NVTheoTen @TenNV nvarchar(100)
AS
	SELECT * FROM NHAN_VIEN WHERE '%' + @TenNV + '%' like TenNV
GO
--5. Thêm nhân viên
IF OBJECT_ID ('SP_ThemNhanVien')
IS NOT NULL
DROP PROCEDURE SP_ThemNhanVien
GO
CREATE PROCEDURE SP_ThemNhanVien  @MaNV CHAR(4), @TenNV NVARCHAR(100), @ChucVu NVARCHAR(100), @TimeCreated DATETIME, @TimeUpdated DATETIME
AS
	IF @MaNV IN (SELECT MaNV FROM NHAN_VIEN)
	BEGIN
		PRINT N'Mã nhân viên không được trùng!'
		RETURN
	END
	ELSE IF @TimeCreated > @TimeUpdated
	BEGIN
		PRINT N'Ngày tạo không được sau ngày cập nhật!'
		RETURN
	END
	ELSE
	BEGIN
	INSERT INTO NHAN_VIEN
	VALUES (@MaNV, @TenNV, @ChucVu, @TimeCreated, @TimeUpdated)
	END
GO
--7. Thêm loại sản phẩm
IF OBJECT_ID ('SP_ThemLoaiSanPham')
IS NOT NULL
DROP PROCEDURE SP_ThemLoaiSanPham
GO
CREATE PROCEDURE SP_ThemLoaiSanPham @MaLoaiSP NVARCHAR(6), @TenLoaiSP NVARCHAR(100), @KieuSP NVARCHAR(50), @TimeCreated DATETIME, @TimeUpdated DATETIME
AS
	IF @MaLoaiSP IN (SELECT MaLoaiSP FROM LOAISP)
	BEGIN
		PRINT N'Mã loại không được trùng!'
		RETURN
	END
	ELSE IF @TimeCreated > @TimeUpdated
	BEGIN
		PRINT N'Ngày tạo không được sau ngày cập nhật!'
		RETURN
	END
	ELSE
	BEGIN
	INSERT INTO LOAISP
	VALUES ( @MaLoaiSP, @TenLoaiSP, @KieuSP, @TimeCreated, @TimeUpdated)
	END
GO
--8. Thêm sản phẩm
IF OBJECT_ID ('SP_ThemSanPham')
IS NOT NULL
DROP PROCEDURE SP_ThemSanPham
GO
CREATE PROCEDURE SP_ThemSanPham @MaSP NVARCHAR(8), @TenSP NVARCHAR(100), @LOAI NVARCHAR(6), @GiaMua INT, @avatar Nchar(50),@TinhTrang NVARCHAR(20), @isDelete BIT,
    @isNew BIT, @TimeCreated DATETIME, @TimeUpdated DATETIME
AS
	IF @LOAI NOT IN (SELECT MaLoaiSP FROM LOAISP)
	BEGIN
		PRINT N'Không tồn tại loại sản phẩm trên!'
		RETURN
	END
	ELSE IF @TimeCreated > @TimeUpdated
	BEGIN
	PRINT N'Ngày tạo không được sau ngày cập nhật!'
		RETURN
	END
	ELSE
	BEGIN
	SET @avatar = 'Database/' + @avatar
	INSERT INTO SANPHAM
	VALUES ( @MaSP, @TenSP, @LOAI , @GiaMua,  @avatar, @TinhTrang, @isDelete, @isNew, @TimeCreated, @TimeUpdated)
	END
GO
--9. Thêm đơn hàng
IF OBJECT_ID ('SP_ThemDonHang')
IS NOT NULL
DROP PROCEDURE SP_ThemDonHang
GO
CREATE PROCEDURE SP_ThemDonHang @MaDH CHAR(6), @STT INT, @ThoiGianNhap DATETIME, @NvNhap CHAR(4), @TongTien INT,
    @TienNhan INT, @TimeCreated DATETIME
AS
	IF @NvNhap NOT IN (SELECT MaNV FROM NHAN_VIEN)
	BEGIN
		PRINT N'Không tồn tại nhân viên trên!'
		RETURN
	END
	ELSE IF @ThoiGianNhap  > @TimeCreated
	BEGIN
	PRINT N'Ngày nhập không được sau ngày tạo đơn hàng!'
		RETURN
	END
	ELSE
	BEGIN
	DECLARE @TienThoi INT
	SET @TienThoi = @TienNhan - @TongTien
	INSERT INTO DON_HANG
	VALUES ( @MaDH , @STT, @ThoiGianNhap , @NvNhap, @TongTien , @TienNhan, @TienThoi, @TimeCreated)
	END
GO
--10.Thêm chi tiết đơn hàng
IF OBJECT_ID ('SP_ThemChiTietDonHang')
IS NOT NULL
DROP PROCEDURE SP_ThemChiTietDonHang
GO
CREATE PROCEDURE SP_ThemChiTietDonHang @MaDH CHAR(6), @MaSP NVARCHAR(8), @GiaBan INT, @SL INT
AS
	IF @MaDH NOT IN (SELECT MaDH FROM DON_HANG)
	BEGIN
		PRINT N'Không tồn tại đơn hàng!'
		RETURN
	END
	ELSE IF @MaSP NOT IN (SELECT MaSP FROM SANPHAM)
	BEGIN
		PRINT N'Không tồn tại sản phẩm!'
		RETURN
	END
	ELSE
	BEGIN
	INSERT INTO DONHANG_SP
	VALUES ( @MaDH , @MaSP, @GiaBan, @SL)
	END
GO