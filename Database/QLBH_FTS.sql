use QLBH

CREATE FUNCTION non_unicode_convert(@inputVar NVARCHAR(MAX))
RETURNS NVARCHAR(MAX)
AS
BEGIN    
    IF (@inputVar IS NULL OR @inputVar = '')  RETURN ''
   
    DECLARE @RT NVARCHAR(MAX)
    DECLARE @SIGN_CHARS NCHAR(256)
    DECLARE @UNSIGN_CHARS NCHAR (256)
 
    SET @SIGN_CHARS = N'ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệếìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵýĂÂĐÊÔƠƯÀẢÃẠÁẰẲẴẶẮẦẨẪẬẤÈẺẼẸÉỀỂỄỆẾÌỈĨỊÍÒỎÕỌÓỒỔỖỘỐỜỞỠỢỚÙỦŨỤÚỪỬỮỰỨỲỶỸỴÝ' + NCHAR(272) + NCHAR(208)
    SET @UNSIGN_CHARS = N'aadeoouaaaaaaaaaaaaaaaeeeeeeeeeeiiiiiooooooooooooooouuuuuuuuuuyyyyyAADEOOUAAAAAAAAAAAAAAAEEEEEEEEEEIIIIIOOOOOOOOOOOOOOOUUUUUUUUUUYYYYYDD'
 
    DECLARE @COUNTER int
    DECLARE @COUNTER1 int
   
    SET @COUNTER = 1
    WHILE (@COUNTER <= LEN(@inputVar))
    BEGIN  
        SET @COUNTER1 = 1
        WHILE (@COUNTER1 <= LEN(@SIGN_CHARS) + 1)
        BEGIN
            IF UNICODE(SUBSTRING(@SIGN_CHARS, @COUNTER1,1)) = UNICODE(SUBSTRING(@inputVar,@COUNTER ,1))
            BEGIN          
                IF @COUNTER = 1
                    SET @inputVar = SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@inputVar, @COUNTER+1,LEN(@inputVar)-1)      
                ELSE
                    SET @inputVar = SUBSTRING(@inputVar, 1, @COUNTER-1) +SUBSTRING(@UNSIGN_CHARS, @COUNTER1,1) + SUBSTRING(@inputVar, @COUNTER+1,LEN(@inputVar)- @COUNTER)
                BREAK
            END
            SET @COUNTER1 = @COUNTER1 +1
        END
        SET @COUNTER = @COUNTER +1
    END
    -- SET @inputVar = replace(@inputVar,' ','-')
    RETURN @inputVar
END

CREATE TABLE SANPHAM_KODAU
(
	MaSP NVARCHAR(8) PRIMARY KEY,
	TenSP VARCHAR(100)
)

INSERT [dbo].[SANPHAM_KODAU] ([MaSP], [TenSP]) VALUES (N'BCAH001', 'Banh Canh')
INSERT [dbo].[SANPHAM_KODAU] ([MaSP], [TenSP]) VALUES (N'BSNK001', 'Lay''s Khoai Tay')
INSERT [dbo].[SANPHAM_KODAU] ([MaSP], [TenSP]) VALUES (N'BUN001', 'Bun Bo')
INSERT [dbo].[SANPHAM_KODAU] ([MaSP], [TenSP]) VALUES (N'CAFE001', 'Ca phe Den')
INSERT [dbo].[SANPHAM_KODAU] ([MaSP], [TenSP]) VALUES (N'CAFE002', 'Ca phe Sua')
INSERT [dbo].[SANPHAM_KODAU] ([MaSP], [TenSP]) VALUES (N'CHE001', 'Che thap cam')
INSERT [dbo].[SANPHAM_KODAU] ([MaSP], [TenSP]) VALUES (N'COM001', 'Com Ga')
INSERT [dbo].[SANPHAM_KODAU] ([MaSP], [TenSP]) VALUES (N'COM002', 'Com Suon')
INSERT [dbo].[SANPHAM_KODAU] ([MaSP], [TenSP]) VALUES (N'HT001', 'Hu tieu Bo')
INSERT [dbo].[SANPHAM_KODAU] ([MaSP], [TenSP]) VALUES (N'MI001', 'Mi Trung')
INSERT [dbo].[SANPHAM_KODAU] ([MaSP], [TenSP]) VALUES (N'NCCAM001', 'Nuoc Cam')
INSERT [dbo].[SANPHAM_KODAU] ([MaSP], [TenSP]) VALUES (N'NCG001', 'Coca Cola')
INSERT [dbo].[SANPHAM_KODAU] ([MaSP], [TenSP]) VALUES (N'NCG002', 'Fanta')
INSERT [dbo].[SANPHAM_KODAU] ([MaSP], [TenSP]) VALUES (N'PHO001', 'Pho Bo')
INSERT [dbo].[SANPHAM_KODAU] ([MaSP], [TenSP]) VALUES (N'PHO002', 'Pho Ga')
INSERT [dbo].[SANPHAM_KODAU] ([MaSP], [TenSP]) VALUES (N'SUA001', 'Sua Long Thanh')

CREATE TRIGGER trgSANPHAM
ON SANPHAM
FOR INSERT, UPDATE, DELETE
AS
BEGIN	
	INSERT SANPHAM_KODAU (MaSP, TenSP)
	SELECT MaSP, dbo.non_unicode_convert(TenSP) as TenSP
	FROM inserted;
	
	DELETE FROM SANPHAM_KODAU
	WHERE EXISTS
	(SELECT * FROM deleted WHERE deleted.MaSP = SANPHAM_KODAU.MaSP)
	
END

CREATE FUNCTION FT_DSTenSP_non_unicode ()
RETURNS table
AS
	RETURN
	(SELECT SANPHAM.MaSP, dbo.non_unicode_convert(SANPHAM.TenSP) as TenSP
	FROM SANPHAM)

CREATE FUNCTION FT_DanhSachSP ()
RETURNS table
AS
	RETURN
	(SELECT SANPHAM.MaSP, SANPHAM.TenSP, LOAISP.KieuSP, SP_TrungBay.GiaBan, SANPHAM.avatar
	FROM ((SANPHAM JOIN SP_TrungBay ON SANPHAM.MASP = SP_TrungBay.MaSP)
	JOIN LOAISP ON SANPHAM.LOAI = LOAISP.MaLoaiSP))


--select serverproperty('isfulltextinstalled')
--select fulltextserviceproperty('isfulltextinstalled')

create fulltext catalog CatalogProductName

--drop fulltext catalog CatalogProductName
--drop fulltext index on SANPHAM

create fulltext index on dbo.SANPHAM_KODAU
(
	TenSP language 1066
)
key index PK__SANPHAM___2725081C11A4E31F
on CatalogProductName
with change_tracking auto;


--select * from freetexttable(SANPHAM, TenSP, 'Bò') as t
--join dbo.FT_DanhSachSP() sp on t.[key] = sp.MaSP


CREATE PROCEDURE SP_TimKiemSP @str_query nvarchar(100)
as
	DECLARE @nonUnicode VARCHAR(100)
	SET @nonUnicode = dbo.non_unicode_convert(@str_query)
	SELECT * 
	FROM dbo.FT_DanhSachSP() AS dssp join SANPHAM_KODAU spkd
	ON (dssp.MaSP = spkd.MaSP)
	WHERE FREETEXT(spkd.TenSP, @nonUnicode)

EXEC dbo.SP_TimKiemSP 'che thap cam'
select * from dbo.FT_DanhSachSP()

EXEC dbo.SP_DanhSachSP
