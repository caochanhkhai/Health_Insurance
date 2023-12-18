using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class _2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChiTietChinhSach",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_GoiBaoHiem1 = table.Column<int>(type: "int", nullable: false),
                    ID_ChinhSach1 = table.Column<int>(type: "int", nullable: false),
                    STT = table.Column<int>(type: "int", nullable: false),
                    HanMucChiTra = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DieuKienApDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mota = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietChinhSach", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ChiTietChinhSach_ChinhSach_ID_ChinhSach1",
                        column: x => x.ID_ChinhSach1,
                        principalTable: "ChinhSach",
                        principalColumn: "ID_ChinhSach",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChiTietChinhSach_GoiBaoHiem_ID_GoiBaoHiem1",
                        column: x => x.ID_GoiBaoHiem1,
                        principalTable: "GoiBaoHiem",
                        principalColumn: "ID_GoiBaoHiem",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuanLyBaoHiem",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ID_KhachHang = table.Column<int>(type: "int", nullable: false),
                    ID_GoiBaoHiem1 = table.Column<int>(type: "int", nullable: false),
                    ThoiGianBatDau = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ThoiGianKetThuc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HanMucDaSuDung = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuanLyBaoHiem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_QuanLyBaoHiem_GoiBaoHiem_ID_GoiBaoHiem1",
                        column: x => x.ID_GoiBaoHiem1,
                        principalTable: "GoiBaoHiem",
                        principalColumn: "ID_GoiBaoHiem",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietChinhSach_ID_ChinhSach1",
                table: "ChiTietChinhSach",
                column: "ID_ChinhSach1");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietChinhSach_ID_GoiBaoHiem1",
                table: "ChiTietChinhSach",
                column: "ID_GoiBaoHiem1");

            migrationBuilder.CreateIndex(
                name: "IX_QuanLyBaoHiem_ID_GoiBaoHiem1",
                table: "QuanLyBaoHiem",
                column: "ID_GoiBaoHiem1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietChinhSach");

            migrationBuilder.DropTable(
                name: "QuanLyBaoHiem");
        }
    }
}
