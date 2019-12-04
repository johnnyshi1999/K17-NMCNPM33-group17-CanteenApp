USE [QLBH]
GO
INSERT [dbo].[NHAN_VIEN] ([MaNV], [TenNV], [ChucVu], [TimeCreated], [TimeUpdated], [PassWordNV]) VALUES (N'SL01', N'Nguyễn Văn Linh', N'Bán Hàng', NULL, NULL, N'SL01')
INSERT [dbo].[NHAN_VIEN] ([MaNV], [TenNV], [ChucVu], [TimeCreated], [TimeUpdated], [PassWordNV]) VALUES (N'SL02', N'Trần Thị Hường', N'Bán Hàng', CAST(N'2018-12-01T08:01:00.000' AS DateTime), CAST(N'2018-12-01T08:01:00.000' AS DateTime), N'SL02')
INSERT [dbo].[NHAN_VIEN] ([MaNV], [TenNV], [ChucVu], [TimeCreated], [TimeUpdated], [PassWordNV]) VALUES (N'SL03', N'Lê Minh Châu
', N'Bán hàng
', CAST(N'2018-12-02T08:02:00.000' AS DateTime), CAST(N'2018-12-02T08:02:00.000' AS DateTime), N'SL03')
INSERT [dbo].[NHAN_VIEN] ([MaNV], [TenNV], [ChucVu], [TimeCreated], [TimeUpdated], [PassWordNV]) VALUES (N'SL04', N'Hà Thị Khánh Huyền
', N'Bán hàng
', CAST(N'2018-12-03T08:03:00.000' AS DateTime), CAST(N'2018-12-03T08:03:00.000' AS DateTime), N'SL04')
INSERT [dbo].[NHAN_VIEN] ([MaNV], [TenNV], [ChucVu], [TimeCreated], [TimeUpdated], [PassWordNV]) VALUES (N'SL05', N'Mai Hồng Ánh
', N'Bán hàng
', CAST(N'2018-12-04T08:04:00.000' AS DateTime), CAST(N'2018-12-04T08:04:00.000' AS DateTime), N'SL05')
INSERT [dbo].[NHAN_VIEN] ([MaNV], [TenNV], [ChucVu], [TimeCreated], [TimeUpdated], [PassWordNV]) VALUES (N'SL06', N'Phan Đức Nhân
', N'Bán hàng
', CAST(N'2018-12-05T08:05:00.000' AS DateTime), CAST(N'2018-12-05T08:05:00.000' AS DateTime), N'SL06')
INSERT [dbo].[DON_HANG] ([MaDH], [STT], [ThoiGianNhap], [NvNhap], [TongTien], [TienNhan], [TienThoi]) VALUES (N'AA1001', 1, CAST(N'2018-12-01T14:10:00.000' AS DateTime), N'SL01', 25000, 30000, 5000)
INSERT [dbo].[DON_HANG] ([MaDH], [STT], [ThoiGianNhap], [NvNhap], [TongTien], [TienNhan], [TienThoi]) VALUES (N'AA1002', 1, CAST(N'2018-12-02T14:10:00.000' AS DateTime), N'SL02', 20000, 20000, 0)
INSERT [dbo].[DON_HANG] ([MaDH], [STT], [ThoiGianNhap], [NvNhap], [TongTien], [TienNhan], [TienThoi]) VALUES (N'AA1003', 1, CAST(N'2018-12-03T14:10:00.000' AS DateTime), N'SL03', 46000, 50000, 4000)
INSERT [dbo].[DON_HANG] ([MaDH], [STT], [ThoiGianNhap], [NvNhap], [TongTien], [TienNhan], [TienThoi]) VALUES (N'AA1004', 1, CAST(N'2018-12-04T14:10:00.000' AS DateTime), N'SL04', 40000, 50000, 10000)
INSERT [dbo].[DON_HANG] ([MaDH], [STT], [ThoiGianNhap], [NvNhap], [TongTien], [TienNhan], [TienThoi]) VALUES (N'AA1005', 2, CAST(N'2018-12-04T14:10:00.000' AS DateTime), N'SL02', 50000, 50000, 0)
INSERT [dbo].[DON_HANG] ([MaDH], [STT], [ThoiGianNhap], [NvNhap], [TongTien], [TienNhan], [TienThoi]) VALUES (N'AA1006', 1, CAST(N'2018-12-04T14:10:00.000' AS DateTime), N'SL01', 22000, 30000, 8000)
INSERT [dbo].[DON_HANG] ([MaDH], [STT], [ThoiGianNhap], [NvNhap], [TongTien], [TienNhan], [TienThoi]) VALUES (N'AA1007', 2, CAST(N'2018-12-04T14:10:00.000' AS DateTime), N'SL03', 42000, 50000, 8000)
INSERT [dbo].[DON_HANG] ([MaDH], [STT], [ThoiGianNhap], [NvNhap], [TongTien], [TienNhan], [TienThoi]) VALUES (N'AA1008', 1, CAST(N'2018-12-04T14:10:00.000' AS DateTime), N'SL05', 18000, 20000, 2000)
INSERT [dbo].[DON_HANG] ([MaDH], [STT], [ThoiGianNhap], [NvNhap], [TongTien], [TienNhan], [TienThoi]) VALUES (N'AA1009', 1, CAST(N'2018-12-04T14:10:00.000' AS DateTime), N'SL04', 16000, 20000, 4000)
INSERT [dbo].[DON_HANG] ([MaDH], [STT], [ThoiGianNhap], [NvNhap], [TongTien], [TienNhan], [TienThoi]) VALUES (N'AA1010', 2, CAST(N'2018-12-04T14:10:00.000' AS DateTime), N'SL06', 22000, 25000, 3000)
INSERT [dbo].[LOAISP] ([MaLoaiSP], [TenLoaiSP], [KieuSP], [TimeCreated], [TimeUpdated]) VALUES (N'BANHMI', N'Bánh mì
', N'Món ăn
', CAST(N'2018-11-29T14:06:00.000' AS DateTime), CAST(N'2018-11-29T14:06:00.000' AS DateTime))
INSERT [dbo].[LOAISP] ([MaLoaiSP], [TenLoaiSP], [KieuSP], [TimeCreated], [TimeUpdated]) VALUES (N'BCAH
', N'Bánh Canh
', N'Món ăn
', CAST(N'2018-11-29T14:03:00.000' AS DateTime), CAST(N'2018-11-29T14:03:00.000' AS DateTime))
INSERT [dbo].[LOAISP] ([MaLoaiSP], [TenLoaiSP], [KieuSP], [TimeCreated], [TimeUpdated]) VALUES (N'BSNK
', N'Bánh snack
', N'Snack
', CAST(N'2018-11-29T14:10:00.000' AS DateTime), CAST(N'2018-11-29T14:10:00.000' AS DateTime))
INSERT [dbo].[LOAISP] ([MaLoaiSP], [TenLoaiSP], [KieuSP], [TimeCreated], [TimeUpdated]) VALUES (N'BUN
 ', N'Bún
', N'Món ăn
', CAST(N'2018-11-29T14:02:00.000' AS DateTime), CAST(N'2018-11-29T14:02:00.000' AS DateTime))
INSERT [dbo].[LOAISP] ([MaLoaiSP], [TenLoaiSP], [KieuSP], [TimeCreated], [TimeUpdated]) VALUES (N'CAFE
', N'Cà phê
', N'Thức uống
', CAST(N'2018-11-29T14:08:00.000' AS DateTime), CAST(N'2018-11-29T14:08:00.000' AS DateTime))
INSERT [dbo].[LOAISP] ([MaLoaiSP], [TenLoaiSP], [KieuSP], [TimeCreated], [TimeUpdated]) VALUES (N'CHE
 ', N'Chè
', N'Snack
', CAST(N'2018-11-29T14:11:00.000' AS DateTime), CAST(N'2018-11-29T14:11:00.000' AS DateTime))
INSERT [dbo].[LOAISP] ([MaLoaiSP], [TenLoaiSP], [KieuSP], [TimeCreated], [TimeUpdated]) VALUES (N'COM
 ', N'Cơm
', N'Món ăn
', CAST(N'2018-11-29T14:00:00.000' AS DateTime), CAST(N'2018-11-29T14:00:00.000' AS DateTime))
INSERT [dbo].[LOAISP] ([MaLoaiSP], [TenLoaiSP], [KieuSP], [TimeCreated], [TimeUpdated]) VALUES (N'HUTIEU', N'Hủ tiếu
', N'Món ăn
', CAST(N'2018-11-29T14:01:00.000' AS DateTime), CAST(N'2018-11-29T14:01:00.000' AS DateTime))
INSERT [dbo].[LOAISP] ([MaLoaiSP], [TenLoaiSP], [KieuSP], [TimeCreated], [TimeUpdated]) VALUES (N'MI
  ', N'Mì
', N'Món ăn
', CAST(N'2018-11-29T14:05:00.000' AS DateTime), CAST(N'2018-11-29T14:05:00.000' AS DateTime))
INSERT [dbo].[LOAISP] ([MaLoaiSP], [TenLoaiSP], [KieuSP], [TimeCreated], [TimeUpdated]) VALUES (N'NCCAM ', N'Nước cam
', N'Thức uống
', CAST(N'2018-11-29T14:09:00.000' AS DateTime), CAST(N'2018-11-29T14:09:00.000' AS DateTime))
INSERT [dbo].[LOAISP] ([MaLoaiSP], [TenLoaiSP], [KieuSP], [TimeCreated], [TimeUpdated]) VALUES (N'NCG
 ', N'Nước có ga
', N'Thức uống
', CAST(N'2018-11-29T14:12:00.000' AS DateTime), CAST(N'2018-11-29T14:12:00.000' AS DateTime))
INSERT [dbo].[LOAISP] ([MaLoaiSP], [TenLoaiSP], [KieuSP], [TimeCreated], [TimeUpdated]) VALUES (N'PHO
 ', N'Phở
', N'Món ăn
', CAST(N'2018-11-29T14:04:00.000' AS DateTime), CAST(N'2018-11-29T14:04:00.000' AS DateTime))
INSERT [dbo].[LOAISP] ([MaLoaiSP], [TenLoaiSP], [KieuSP], [TimeCreated], [TimeUpdated]) VALUES (N'SUA
 ', N'Sữa
', N'Thức uống
', CAST(N'2018-11-29T14:07:00.000' AS DateTime), CAST(N'2018-11-29T14:07:00.000' AS DateTime))
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [LOAI], [GiaMua], [avatar], [TinhTrang], [isDelete], [isNew], [TimeCreated], [TimeUpdated]) VALUES (N'BCAH001 ', N'Bánh Canh', N'BCAH
', 11000, N'Database/BanhCanh.jpg', N'in stock', 0, 1, CAST(N'2018-11-29T15:07:00.000' AS DateTime), CAST(N'2018-11-29T15:07:00.000' AS DateTime))
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [LOAI], [GiaMua], [avatar], [TinhTrang], [isDelete], [isNew], [TimeCreated], [TimeUpdated]) VALUES (N'BSNK001 ', N'Lay''s Khoai Tây', N'BSNK
', 6000, N'Database/laysKhoaiTay.jpg', N'in stock', 0, 1, CAST(N'2018-11-29T15:15:00.000' AS DateTime), CAST(N'2018-11-29T15:15:00.000' AS DateTime))
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [LOAI], [GiaMua], [avatar], [TinhTrang], [isDelete], [isNew], [TimeCreated], [TimeUpdated]) VALUES (N'BUN001
', N'Bún Bò', N'BUN
 ', 16000, N'Database/BunBo.jpg', N'in stock', 0, 1, CAST(N'2018-11-29T15:03:00.000' AS DateTime), CAST(N'2018-11-29T15:03:00.000' AS DateTime))
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [LOAI], [GiaMua], [avatar], [TinhTrang], [isDelete], [isNew], [TimeCreated], [TimeUpdated]) VALUES (N'CAFE001 ', N'Cà phê Đen', N'CAFE
', 6000, N'Database/cfDen.jpg', N'in stock', 0, 1, CAST(N'2018-11-29T15:10:00.000' AS DateTime), CAST(N'2018-11-29T15:10:00.000' AS DateTime))
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [LOAI], [GiaMua], [avatar], [TinhTrang], [isDelete], [isNew], [TimeCreated], [TimeUpdated]) VALUES (N'CAFE002 ', N'Cà phê Sữa', N'CAFE
', 7000, N'Database/cfSua.jpg', N'in stock', 0, 1, CAST(N'2018-11-29T15:11:00.000' AS DateTime), CAST(N'2018-11-29T15:11:00.000' AS DateTime))
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [LOAI], [GiaMua], [avatar], [TinhTrang], [isDelete], [isNew], [TimeCreated], [TimeUpdated]) VALUES (N'CHE001
', N'Chè thập cẩm', N'CHE
 ', 5000, N'Database/cheThapCam.jpg', N'in stock', 0, 1, CAST(N'2018-11-29T15:12:00.000' AS DateTime), CAST(N'2018-11-29T15:12:00.000' AS DateTime))
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [LOAI], [GiaMua], [avatar], [TinhTrang], [isDelete], [isNew], [TimeCreated], [TimeUpdated]) VALUES (N'COM001
', N'Cơm Gà', N'COM
 ', 15000, N'Database/comGa.jpg', N'in stock', 0, 1, CAST(N'2018-11-29T15:00:00.000' AS DateTime), CAST(N'2018-11-29T15:00:00.000' AS DateTime))
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [LOAI], [GiaMua], [avatar], [TinhTrang], [isDelete], [isNew], [TimeCreated], [TimeUpdated]) VALUES (N'COM002
', N'Cơm Sườn', N'COM
 ', 14000, N'Database/comSuon.jpg', N'in stock', 0, 1, CAST(N'2018-11-29T15:02:00.000' AS DateTime), CAST(N'2018-11-29T15:02:00.000' AS DateTime))
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [LOAI], [GiaMua], [avatar], [TinhTrang], [isDelete], [isNew], [TimeCreated], [TimeUpdated]) VALUES (N'HT001
 ', N'Hủ tiếu Bò', N'HUTIEU', 12000, N'Database/huTieuBo.jpg', N'in stock', 0, 1, CAST(N'2018-11-29T15:01:00.000' AS DateTime), CAST(N'2018-11-29T15:01:00.000' AS DateTime))
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [LOAI], [GiaMua], [avatar], [TinhTrang], [isDelete], [isNew], [TimeCreated], [TimeUpdated]) VALUES (N'MI001
 ', N'Mì Trứng', N'MI
  ', 10000, N'Database/miTrung.jpg', N'in stock', 0, 1, CAST(N'2018-11-29T15:06:00.000' AS DateTime), CAST(N'2018-11-29T15:06:00.000' AS DateTime))
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [LOAI], [GiaMua], [avatar], [TinhTrang], [isDelete], [isNew], [TimeCreated], [TimeUpdated]) VALUES (N'NCCAM001', N'Nước Cam', N'NCCAM ', 7000, N'Database/nuocCam.jpg', N'in stock', 0, 1, CAST(N'2018-11-29T15:09:00.000' AS DateTime), CAST(N'2018-11-29T15:09:00.000' AS DateTime))
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [LOAI], [GiaMua], [avatar], [TinhTrang], [isDelete], [isNew], [TimeCreated], [TimeUpdated]) VALUES (N'NCG001
', N'Coca Cola', N'NCG
 ', 7000, N'Database/coca.jpg', N'in stock', 0, 1, CAST(N'2018-11-29T15:13:00.000' AS DateTime), CAST(N'2018-11-29T15:13:00.000' AS DateTime))
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [LOAI], [GiaMua], [avatar], [TinhTrang], [isDelete], [isNew], [TimeCreated], [TimeUpdated]) VALUES (N'NCG002  ', N'Fanta', N'NCG
 ', 7000, N'Database/fanta.jpg', N'out of stock', 0, 1, CAST(N'2018-11-29T15:14:00.000' AS DateTime), CAST(N'2018-11-29T15:14:00.000' AS DateTime))
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [LOAI], [GiaMua], [avatar], [TinhTrang], [isDelete], [isNew], [TimeCreated], [TimeUpdated]) VALUES (N'PHO001  ', N'Phở Bò', N'PHO
 ', 16000, N'Database/phoBo.png', N'in stock', 0, 1, CAST(N'2018-11-29T15:04:00.000' AS DateTime), CAST(N'2018-11-29T15:04:00.000' AS DateTime))
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [LOAI], [GiaMua], [avatar], [TinhTrang], [isDelete], [isNew], [TimeCreated], [TimeUpdated]) VALUES (N'PHO002  ', N'Phở Gà', N'PHO
 ', 14000, N'Database/phoGa.jpg', N'in stock', 0, 1, CAST(N'2018-11-29T15:05:00.000' AS DateTime), CAST(N'2018-11-29T15:05:00.000' AS DateTime))
INSERT [dbo].[SANPHAM] ([MaSP], [TenSP], [LOAI], [GiaMua], [avatar], [TinhTrang], [isDelete], [isNew], [TimeCreated], [TimeUpdated]) VALUES (N'SUA001  ', N'Sữa Long Thành', N'SUA
 ', 10000, N'Database/suaLongThanh.jped', N'out of stock', 0, 1, CAST(N'2018-11-29T15:08:00.000' AS DateTime), CAST(N'2018-11-29T15:08:00.000' AS DateTime))
INSERT [dbo].[DONHANG_SP] ([MaDH], [MaSP], [GiaBan], [SoLuong]) VALUES (N'AA1001', N'COM001
', NULL, 1)
INSERT [dbo].[DONHANG_SP] ([MaDH], [MaSP], [GiaBan], [SoLuong]) VALUES (N'AA1002', N'HT001
 ', NULL, 1)
INSERT [dbo].[DONHANG_SP] ([MaDH], [MaSP], [GiaBan], [SoLuong]) VALUES (N'AA1003', N'BUN001
', NULL, 1)
INSERT [dbo].[DONHANG_SP] ([MaDH], [MaSP], [GiaBan], [SoLuong]) VALUES (N'AA1004', N'BSNK001 ', NULL, 2)
INSERT [dbo].[DONHANG_SP] ([MaDH], [MaSP], [GiaBan], [SoLuong]) VALUES (N'AA1005', N'COM002
', NULL, 2)
INSERT [dbo].[DONHANG_SP] ([MaDH], [MaSP], [GiaBan], [SoLuong]) VALUES (N'AA1006', N'CAFE002 ', NULL, 2)
INSERT [dbo].[DONHANG_SP] ([MaDH], [MaSP], [GiaBan], [SoLuong]) VALUES (N'AA1007', N'NCCAM001', NULL, 3)
INSERT [dbo].[DONHANG_SP] ([MaDH], [MaSP], [GiaBan], [SoLuong]) VALUES (N'AA1008', N'MI001
 ', NULL, 1)
INSERT [dbo].[DONHANG_SP] ([MaDH], [MaSP], [GiaBan], [SoLuong]) VALUES (N'AA1009', N'CHE001
', 8000, 2)
INSERT [dbo].[DONHANG_SP] ([MaDH], [MaSP], [GiaBan], [SoLuong]) VALUES (N'AA1010', N'NCG001
', NULL, 2)
INSERT [dbo].[SP_TrungBay] ([MaSP], [GiaBan], [TinhTrang], [TimeCreated], [TimeUpdated]) VALUES (N'BSNK001 ', 10000, N'true', CAST(N'2018-11-29T15:35:00.000' AS DateTime), CAST(N'2018-11-29T15:35:00.000' AS DateTime))
INSERT [dbo].[SP_TrungBay] ([MaSP], [GiaBan], [TinhTrang], [TimeCreated], [TimeUpdated]) VALUES (N'BUN001
', 30000, N'true', CAST(N'2018-11-29T15:31:00.000' AS DateTime), CAST(N'2018-11-29T15:31:00.000' AS DateTime))
INSERT [dbo].[SP_TrungBay] ([MaSP], [GiaBan], [TinhTrang], [TimeCreated], [TimeUpdated]) VALUES (N'CAFE002 ', 10000, N'true', CAST(N'2018-11-29T15:40:00.000' AS DateTime), CAST(N'2018-11-29T15:40:00.000' AS DateTime))
INSERT [dbo].[SP_TrungBay] ([MaSP], [GiaBan], [TinhTrang], [TimeCreated], [TimeUpdated]) VALUES (N'CHE001
', 7000, N'true', CAST(N'2018-11-29T15:33:00.000' AS DateTime), CAST(N'2018-11-29T15:33:00.000' AS DateTime))
INSERT [dbo].[SP_TrungBay] ([MaSP], [GiaBan], [TinhTrang], [TimeCreated], [TimeUpdated]) VALUES (N'COM001
', 25000, N'true', CAST(N'2018-11-29T15:30:00.000' AS DateTime), CAST(N'2018-11-29T15:30:00.000' AS DateTime))
INSERT [dbo].[SP_TrungBay] ([MaSP], [GiaBan], [TinhTrang], [TimeCreated], [TimeUpdated]) VALUES (N'COM002
', 20000, N'true', CAST(N'2018-11-29T15:39:00.000' AS DateTime), CAST(N'2018-11-29T15:39:00.000' AS DateTime))
INSERT [dbo].[SP_TrungBay] ([MaSP], [GiaBan], [TinhTrang], [TimeCreated], [TimeUpdated]) VALUES (N'HT001
 ', 13000, N'true', CAST(N'2018-11-29T15:36:00.000' AS DateTime), CAST(N'2018-11-29T15:36:00.000' AS DateTime))
INSERT [dbo].[SP_TrungBay] ([MaSP], [GiaBan], [TinhTrang], [TimeCreated], [TimeUpdated]) VALUES (N'MI001
 ', 15000, N'true', CAST(N'2018-11-29T15:32:00.000' AS DateTime), CAST(N'2018-11-29T15:32:00.000' AS DateTime))
INSERT [dbo].[SP_TrungBay] ([MaSP], [GiaBan], [TinhTrang], [TimeCreated], [TimeUpdated]) VALUES (N'NCCAM001', 10000, N'true', CAST(N'2018-11-29T15:37:00.000' AS DateTime), CAST(N'2018-11-29T15:37:00.000' AS DateTime))
INSERT [dbo].[SP_TrungBay] ([MaSP], [GiaBan], [TinhTrang], [TimeCreated], [TimeUpdated]) VALUES (N'NCG001
', 11000, N'true', CAST(N'2018-11-29T15:34:00.000' AS DateTime), CAST(N'2018-11-29T15:34:00.000' AS DateTime))
INSERT [dbo].[SP_TrungBay] ([MaSP], [GiaBan], [TinhTrang], [TimeCreated], [TimeUpdated]) VALUES (N'PHO002  ', 16000, N'true', CAST(N'2018-11-29T15:38:00.000' AS DateTime), CAST(N'2018-11-29T15:38:00.000' AS DateTime))
