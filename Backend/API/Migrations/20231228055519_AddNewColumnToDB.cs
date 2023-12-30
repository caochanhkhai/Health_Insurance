using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumnToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NhanVienID_NhanVien",
                table: "PhieuDangKi",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "XacThuc",
                table: "KhachHang",
                type: "nvarchar(13)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HinhAnh",
                table: "GoiBaoHiem",
                type: "varchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuDangKi_NhanVienID_NhanVien",
                table: "PhieuDangKi",
                column: "NhanVienID_NhanVien");

            migrationBuilder.AddForeignKey(
                name: "FK_PhieuDangKi_NhanVien_NhanVienID_NhanVien",
                table: "PhieuDangKi",
                column: "NhanVienID_NhanVien",
                principalTable: "NhanVien",
                principalColumn: "ID_NhanVien",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhieuDangKi_NhanVien_NhanVienID_NhanVien",
                table: "PhieuDangKi");

            migrationBuilder.DropIndex(
                name: "IX_PhieuDangKi_NhanVienID_NhanVien",
                table: "PhieuDangKi");

            migrationBuilder.DropColumn(
                name: "NhanVienID_NhanVien",
                table: "PhieuDangKi");

            migrationBuilder.DropColumn(
                name: "XacThuc",
                table: "KhachHang");

            migrationBuilder.DropColumn(
                name: "HinhAnh",
                table: "GoiBaoHiem");
        }
    }
}