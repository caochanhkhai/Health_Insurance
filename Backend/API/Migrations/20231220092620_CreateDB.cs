using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class CreateDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BenhVien",
                columns: table => new
                {
                    ID_BenhVien = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenBenhVien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BenhVien", x => x.ID_BenhVien);
                });

            migrationBuilder.CreateTable(
                name: "ChinhSach",
                columns: table => new
                {
                    ID_ChinhSach = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    STT = table.Column<int>(type: "int", nullable: false),
                    TenChinhSach = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoiGianPhatHanh = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChinhSach", x => x.ID_ChinhSach);
                });

            migrationBuilder.CreateTable(
                name: "CongTy",
                columns: table => new
                {
                    ID_CongTy = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenCongTy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoNhaTenDuong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhuongXa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuanHuyen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThanhPho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CongTy", x => x.ID_CongTy);
                });

            migrationBuilder.CreateTable(
                name: "GoiBaoHiem",
                columns: table => new
                {
                    ID_GoiBaoHiem = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenBaoHiem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenGoi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GiaTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ThoiHan = table.Column<int>(type: "int", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayPhatHanh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TinhTrang = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoiBaoHiem", x => x.ID_GoiBaoHiem);
                });

            migrationBuilder.CreateTable(
                name: "TaiKhoan",
                columns: table => new
                {
                    ID_TaiKhoan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenDangNhap = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoaiTaiKhoan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TinhTrang = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaiKhoan", x => x.ID_TaiKhoan);
                });

            migrationBuilder.CreateTable(
                name: "BenhVien_GoiBaoHiem",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BenhVienID_BenhVien = table.Column<int>(type: "int", nullable: false),
                    GoiBaoHiemID_GoiBaoHiem = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BenhVien_GoiBaoHiem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BenhVien_GoiBaoHiem_BenhVien_BenhVienID_BenhVien",
                        column: x => x.BenhVienID_BenhVien,
                        principalTable: "BenhVien",
                        principalColumn: "ID_BenhVien",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BenhVien_GoiBaoHiem_GoiBaoHiem_GoiBaoHiemID_GoiBaoHiem",
                        column: x => x.GoiBaoHiemID_GoiBaoHiem,
                        principalTable: "GoiBaoHiem",
                        principalColumn: "ID_GoiBaoHiem",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ChiTietChinhSach",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoiBaoHiemID_GoiBaoHiem = table.Column<int>(type: "int", nullable: false),
                    ChinhSachID_ChinhSach = table.Column<int>(type: "int", nullable: false),
                    STT = table.Column<int>(type: "int", nullable: false),
                    HanMucChiTra = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DieuKienApDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietChinhSach", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ChiTietChinhSach_ChinhSach_ChinhSachID_ChinhSach",
                        column: x => x.ChinhSachID_ChinhSach,
                        principalTable: "ChinhSach",
                        principalColumn: "ID_ChinhSach",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietChinhSach_GoiBaoHiem_GoiBaoHiemID_GoiBaoHiem",
                        column: x => x.GoiBaoHiemID_GoiBaoHiem,
                        principalTable: "GoiBaoHiem",
                        principalColumn: "ID_GoiBaoHiem",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "KhachHang",
                columns: table => new
                {
                    ID_KhachHang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuocTich = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ChieuCao = table.Column<int>(type: "int", nullable: false),
                    CanNang = table.Column<int>(type: "int", nullable: false),
                    SoNhaTenDuong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhuongXa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuanHuyen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThanhPho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CMND = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgheNghiep = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChiTietCongViec = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThuNhap = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoTaiKhoan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NganHang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CongTyID_CongTy = table.Column<int>(type: "int", nullable: false),
                    TaiKhoanID_TaiKhoan = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHang", x => x.ID_KhachHang);
                    table.ForeignKey(
                        name: "FK_KhachHang_CongTy_CongTyID_CongTy",
                        column: x => x.CongTyID_CongTy,
                        principalTable: "CongTy",
                        principalColumn: "ID_CongTy",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KhachHang_TaiKhoan_TaiKhoanID_TaiKhoan",
                        column: x => x.TaiKhoanID_TaiKhoan,
                        principalTable: "TaiKhoan",
                        principalColumn: "ID_TaiKhoan",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "NhanVien",
                columns: table => new
                {
                    ID_NhanVien = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GioiTinh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuocTich = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgaySinh = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CMND = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoNhaTenDuong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhuongXa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuanHuyen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThanhPho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LoaiNhanVien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoTaiKhoan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NganHang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaiKhoanID_TaiKhoan = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanVien", x => x.ID_NhanVien);
                    table.ForeignKey(
                        name: "FK_NhanVien_TaiKhoan_TaiKhoanID_TaiKhoan",
                        column: x => x.TaiKhoanID_TaiKhoan,
                        principalTable: "TaiKhoan",
                        principalColumn: "ID_TaiKhoan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhieuDangKi",
                columns: table => new
                {
                    ID_PhieuDangKi = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TinhTrangDuyet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaDiemKiKet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoiGianKiKet = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToKhaiSucKhoe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KhachHangID_KhachHang = table.Column<int>(type: "int", nullable: false),
                    GoiBaoHiemID_GoiBaoHiem = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuDangKi", x => x.ID_PhieuDangKi);
                    table.ForeignKey(
                        name: "FK_PhieuDangKi_GoiBaoHiem_GoiBaoHiemID_GoiBaoHiem",
                        column: x => x.GoiBaoHiemID_GoiBaoHiem,
                        principalTable: "GoiBaoHiem",
                        principalColumn: "ID_GoiBaoHiem",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhieuDangKi_KhachHang_KhachHangID_KhachHang",
                        column: x => x.KhachHangID_KhachHang,
                        principalTable: "KhachHang",
                        principalColumn: "ID_KhachHang",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "QuanLyBaoHiem",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KhachHangID_KhachHang = table.Column<int>(type: "int", nullable: false),
                    GoiBaoHiemID_GoiBaoHiem = table.Column<int>(type: "int", nullable: false),
                    ThoiGianBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HanMucDaSuDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuanLyBaoHiem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuanLyBaoHiem_GoiBaoHiem_GoiBaoHiemID_GoiBaoHiem",
                        column: x => x.GoiBaoHiemID_GoiBaoHiem,
                        principalTable: "GoiBaoHiem",
                        principalColumn: "ID_GoiBaoHiem",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuanLyBaoHiem_KhachHang_KhachHangID_KhachHang",
                        column: x => x.KhachHangID_KhachHang,
                        principalTable: "KhachHang",
                        principalColumn: "ID_KhachHang",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "DatLichTuVan",
                columns: table => new
                {
                    ID_YeuCauTuVan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TinhTrangDuyet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaDiem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoiGian = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KhachHangID_KhachHang = table.Column<int>(type: "int", nullable: false),
                    NhanVien1ID_NhanVien = table.Column<int>(type: "int", nullable: false),
                    NhanVien2ID_NhanVien = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatLichTuVan", x => x.ID_YeuCauTuVan);
                    table.ForeignKey(
                        name: "FK_DatLichTuVan_KhachHang_KhachHangID_KhachHang",
                        column: x => x.KhachHangID_KhachHang,
                        principalTable: "KhachHang",
                        principalColumn: "ID_KhachHang",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DatLichTuVan_NhanVien_NhanVien1ID_NhanVien",
                        column: x => x.NhanVien1ID_NhanVien,
                        principalTable: "NhanVien",
                        principalColumn: "ID_NhanVien",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DatLichTuVan_NhanVien_NhanVien2ID_NhanVien",
                        column: x => x.NhanVien2ID_NhanVien,
                        principalTable: "NhanVien",
                        principalColumn: "ID_NhanVien",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "HopDong",
                columns: table => new
                {
                    ID_HopDong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayKyKet = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiHan = table.Column<int>(type: "int", nullable: false),
                    GiaTriHopDong = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DieuKhoan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HieuLuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhieuDangKiID_PhieuDangKi = table.Column<int>(type: "int", nullable: false),
                    NhanVienID_NhanVien = table.Column<int>(type: "int", nullable: false),
                    KhachHangID_KhachHang = table.Column<int>(type: "int", nullable: false),
                    GoiBaoHiemID_GoiBaoHiem = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HopDong", x => x.ID_HopDong);
                    table.ForeignKey(
                        name: "FK_HopDong_GoiBaoHiem_GoiBaoHiemID_GoiBaoHiem",
                        column: x => x.GoiBaoHiemID_GoiBaoHiem,
                        principalTable: "GoiBaoHiem",
                        principalColumn: "ID_GoiBaoHiem",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HopDong_KhachHang_KhachHangID_KhachHang",
                        column: x => x.KhachHangID_KhachHang,
                        principalTable: "KhachHang",
                        principalColumn: "ID_KhachHang",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_HopDong_NhanVien_NhanVienID_NhanVien",
                        column: x => x.NhanVienID_NhanVien,
                        principalTable: "NhanVien",
                        principalColumn: "ID_NhanVien",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_HopDong_PhieuDangKi_PhieuDangKiID_PhieuDangKi",
                        column: x => x.PhieuDangKiID_PhieuDangKi,
                        principalTable: "PhieuDangKi",
                        principalColumn: "ID_PhieuDangKi",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "YeuCauChiTra",
                columns: table => new
                {
                    ID_YeuCauChiTra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QLBHID = table.Column<int>(type: "int", nullable: false),
                    NguoiYeuCau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoiQuanHe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoTienYeuCauChiTra = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TruongHopChiTra = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayYeuCau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TinhTrangDuyet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoiDieuTri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChanDoan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HauQua = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HinhThucDieuTri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HinhHoaDon = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YeuCauChiTra", x => x.ID_YeuCauChiTra);
                    table.ForeignKey(
                        name: "FK_YeuCauChiTra_QuanLyBaoHiem_QLBHID",
                        column: x => x.QLBHID,
                        principalTable: "QuanLyBaoHiem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhieuThanhToanBaoHiem",
                columns: table => new
                {
                    ID_PhieuThanhToan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayThanhToan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HinhThucThanhToan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TinhTrangDuyet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HopDongID_HopDong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuThanhToanBaoHiem", x => x.ID_PhieuThanhToan);
                    table.ForeignKey(
                        name: "FK_PhieuThanhToanBaoHiem_HopDong_HopDongID_HopDong",
                        column: x => x.HopDongID_HopDong,
                        principalTable: "HopDong",
                        principalColumn: "ID_HopDong",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LichSuChiTra",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YeuCauChiTraID_YeuCauChiTra = table.Column<int>(type: "int", nullable: false),
                    TenBenhVien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoiGianChiTra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoTienChiTra = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichSuChiTra", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LichSuChiTra_YeuCauChiTra_YeuCauChiTraID_YeuCauChiTra",
                        column: x => x.YeuCauChiTraID_YeuCauChiTra,
                        principalTable: "YeuCauChiTra",
                        principalColumn: "ID_YeuCauChiTra",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BenhVien_GoiBaoHiem_BenhVienID_BenhVien",
                table: "BenhVien_GoiBaoHiem",
                column: "BenhVienID_BenhVien");

            migrationBuilder.CreateIndex(
                name: "IX_BenhVien_GoiBaoHiem_GoiBaoHiemID_GoiBaoHiem",
                table: "BenhVien_GoiBaoHiem",
                column: "GoiBaoHiemID_GoiBaoHiem");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietChinhSach_ChinhSachID_ChinhSach",
                table: "ChiTietChinhSach",
                column: "ChinhSachID_ChinhSach");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietChinhSach_GoiBaoHiemID_GoiBaoHiem",
                table: "ChiTietChinhSach",
                column: "GoiBaoHiemID_GoiBaoHiem");

            migrationBuilder.CreateIndex(
                name: "IX_DatLichTuVan_KhachHangID_KhachHang",
                table: "DatLichTuVan",
                column: "KhachHangID_KhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_DatLichTuVan_NhanVien1ID_NhanVien",
                table: "DatLichTuVan",
                column: "NhanVien1ID_NhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_DatLichTuVan_NhanVien2ID_NhanVien",
                table: "DatLichTuVan",
                column: "NhanVien2ID_NhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_HopDong_GoiBaoHiemID_GoiBaoHiem",
                table: "HopDong",
                column: "GoiBaoHiemID_GoiBaoHiem");

            migrationBuilder.CreateIndex(
                name: "IX_HopDong_KhachHangID_KhachHang",
                table: "HopDong",
                column: "KhachHangID_KhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_HopDong_NhanVienID_NhanVien",
                table: "HopDong",
                column: "NhanVienID_NhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_HopDong_PhieuDangKiID_PhieuDangKi",
                table: "HopDong",
                column: "PhieuDangKiID_PhieuDangKi");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHang_CongTyID_CongTy",
                table: "KhachHang",
                column: "CongTyID_CongTy");

            migrationBuilder.CreateIndex(
                name: "IX_KhachHang_TaiKhoanID_TaiKhoan",
                table: "KhachHang",
                column: "TaiKhoanID_TaiKhoan");

            migrationBuilder.CreateIndex(
                name: "IX_LichSuChiTra_YeuCauChiTraID_YeuCauChiTra",
                table: "LichSuChiTra",
                column: "YeuCauChiTraID_YeuCauChiTra");

            migrationBuilder.CreateIndex(
                name: "IX_NhanVien_TaiKhoanID_TaiKhoan",
                table: "NhanVien",
                column: "TaiKhoanID_TaiKhoan");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuDangKi_GoiBaoHiemID_GoiBaoHiem",
                table: "PhieuDangKi",
                column: "GoiBaoHiemID_GoiBaoHiem");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuDangKi_KhachHangID_KhachHang",
                table: "PhieuDangKi",
                column: "KhachHangID_KhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuThanhToanBaoHiem_HopDongID_HopDong",
                table: "PhieuThanhToanBaoHiem",
                column: "HopDongID_HopDong");

            migrationBuilder.CreateIndex(
                name: "IX_QuanLyBaoHiem_GoiBaoHiemID_GoiBaoHiem",
                table: "QuanLyBaoHiem",
                column: "GoiBaoHiemID_GoiBaoHiem");

            migrationBuilder.CreateIndex(
                name: "IX_QuanLyBaoHiem_KhachHangID_KhachHang",
                table: "QuanLyBaoHiem",
                column: "KhachHangID_KhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_YeuCauChiTra_QLBHID",
                table: "YeuCauChiTra",
                column: "QLBHID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BenhVien_GoiBaoHiem");

            migrationBuilder.DropTable(
                name: "ChiTietChinhSach");

            migrationBuilder.DropTable(
                name: "DatLichTuVan");

            migrationBuilder.DropTable(
                name: "LichSuChiTra");

            migrationBuilder.DropTable(
                name: "PhieuThanhToanBaoHiem");

            migrationBuilder.DropTable(
                name: "BenhVien");

            migrationBuilder.DropTable(
                name: "ChinhSach");

            migrationBuilder.DropTable(
                name: "YeuCauChiTra");

            migrationBuilder.DropTable(
                name: "HopDong");

            migrationBuilder.DropTable(
                name: "QuanLyBaoHiem");

            migrationBuilder.DropTable(
                name: "NhanVien");

            migrationBuilder.DropTable(
                name: "PhieuDangKi");

            migrationBuilder.DropTable(
                name: "GoiBaoHiem");

            migrationBuilder.DropTable(
                name: "KhachHang");

            migrationBuilder.DropTable(
                name: "CongTy");

            migrationBuilder.DropTable(
                name: "TaiKhoan");
        }
    }
}