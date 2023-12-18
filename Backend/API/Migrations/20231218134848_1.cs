using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class _1 : Migration
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
                name: "PhieuThanhToanBaoHiem",
                columns: table => new
                {
                    ID_PhieuThanhToan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayThanhToan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HinhThucThanhToan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TinhTrang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ID_HopDong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuThanhToanBaoHiem", x => x.ID_PhieuThanhToan);
                });

            migrationBuilder.CreateTable(
                name: "BenhVien_GoiBaoHiem",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_BenhVien1 = table.Column<int>(type: "int", nullable: false),
                    ID_GoiBaoHiem1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BenhVien_GoiBaoHiem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BenhVien_GoiBaoHiem_BenhVien_ID_BenhVien1",
                        column: x => x.ID_BenhVien1,
                        principalTable: "BenhVien",
                        principalColumn: "ID_BenhVien",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BenhVien_GoiBaoHiem_GoiBaoHiem_ID_GoiBaoHiem1",
                        column: x => x.ID_GoiBaoHiem1,
                        principalTable: "GoiBaoHiem",
                        principalColumn: "ID_GoiBaoHiem",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "YeuCauChiTra",
                columns: table => new
                {
                    ID_YeuCauChiTra = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_KhachHang = table.Column<int>(type: "int", nullable: false),
                    ID_GoiBaoHiem1 = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_YeuCauChiTra_GoiBaoHiem_ID_GoiBaoHiem1",
                        column: x => x.ID_GoiBaoHiem1,
                        principalTable: "GoiBaoHiem",
                        principalColumn: "ID_GoiBaoHiem",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LichSuChiTra",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_YeuCauChiTra1 = table.Column<int>(type: "int", nullable: false),
                    TenBenhVien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThoiGianChiTra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoTienChiTra = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LichSuChiTra", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LichSuChiTra_YeuCauChiTra_ID_YeuCauChiTra1",
                        column: x => x.ID_YeuCauChiTra1,
                        principalTable: "YeuCauChiTra",
                        principalColumn: "ID_YeuCauChiTra",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BenhVien_GoiBaoHiem_ID_BenhVien1",
                table: "BenhVien_GoiBaoHiem",
                column: "ID_BenhVien1");

            migrationBuilder.CreateIndex(
                name: "IX_BenhVien_GoiBaoHiem_ID_GoiBaoHiem1",
                table: "BenhVien_GoiBaoHiem",
                column: "ID_GoiBaoHiem1");

            migrationBuilder.CreateIndex(
                name: "IX_LichSuChiTra_ID_YeuCauChiTra1",
                table: "LichSuChiTra",
                column: "ID_YeuCauChiTra1");

            migrationBuilder.CreateIndex(
                name: "IX_YeuCauChiTra_ID_GoiBaoHiem1",
                table: "YeuCauChiTra",
                column: "ID_GoiBaoHiem1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BenhVien_GoiBaoHiem");

            migrationBuilder.DropTable(
                name: "ChinhSach");

            migrationBuilder.DropTable(
                name: "LichSuChiTra");

            migrationBuilder.DropTable(
                name: "PhieuThanhToanBaoHiem");

            migrationBuilder.DropTable(
                name: "BenhVien");

            migrationBuilder.DropTable(
                name: "YeuCauChiTra");

            migrationBuilder.DropTable(
                name: "GoiBaoHiem");
        }
    }
}
