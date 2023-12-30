using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AcceptNull1SoColumnFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DatLichTuVan_NhanVien_NhanVien1ID_NhanVien",
                table: "DatLichTuVan");

            migrationBuilder.DropForeignKey(
                name: "FK_DatLichTuVan_NhanVien_NhanVien2ID_NhanVien",
                table: "DatLichTuVan");

            migrationBuilder.DropForeignKey(
                name: "FK_KhachHang_CongTy_CongTyID_CongTy",
                table: "KhachHang");

            migrationBuilder.DropForeignKey(
                name: "FK_PhieuDangKi_NhanVien_NhanVienID_NhanVien",
                table: "PhieuDangKi");

            migrationBuilder.AlterColumn<int>(
                name: "NhanVienID_NhanVien",
                table: "PhieuDangKi",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CongTyID_CongTy",
                table: "KhachHang",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NhanVien2ID_NhanVien",
                table: "DatLichTuVan",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NhanVien1ID_NhanVien",
                table: "DatLichTuVan",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_DatLichTuVan_NhanVien_NhanVien1ID_NhanVien",
                table: "DatLichTuVan",
                column: "NhanVien1ID_NhanVien",
                principalTable: "NhanVien",
                principalColumn: "ID_NhanVien");

            migrationBuilder.AddForeignKey(
                name: "FK_DatLichTuVan_NhanVien_NhanVien2ID_NhanVien",
                table: "DatLichTuVan",
                column: "NhanVien2ID_NhanVien",
                principalTable: "NhanVien",
                principalColumn: "ID_NhanVien");

            migrationBuilder.AddForeignKey(
                name: "FK_KhachHang_CongTy_CongTyID_CongTy",
                table: "KhachHang",
                column: "CongTyID_CongTy",
                principalTable: "CongTy",
                principalColumn: "ID_CongTy");

            migrationBuilder.AddForeignKey(
                name: "FK_PhieuDangKi_NhanVien_NhanVienID_NhanVien",
                table: "PhieuDangKi",
                column: "NhanVienID_NhanVien",
                principalTable: "NhanVien",
                principalColumn: "ID_NhanVien");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DatLichTuVan_NhanVien_NhanVien1ID_NhanVien",
                table: "DatLichTuVan");

            migrationBuilder.DropForeignKey(
                name: "FK_DatLichTuVan_NhanVien_NhanVien2ID_NhanVien",
                table: "DatLichTuVan");

            migrationBuilder.DropForeignKey(
                name: "FK_KhachHang_CongTy_CongTyID_CongTy",
                table: "KhachHang");

            migrationBuilder.DropForeignKey(
                name: "FK_PhieuDangKi_NhanVien_NhanVienID_NhanVien",
                table: "PhieuDangKi");

            migrationBuilder.AlterColumn<int>(
                name: "NhanVienID_NhanVien",
                table: "PhieuDangKi",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CongTyID_CongTy",
                table: "KhachHang",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NhanVien2ID_NhanVien",
                table: "DatLichTuVan",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NhanVien1ID_NhanVien",
                table: "DatLichTuVan",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DatLichTuVan_NhanVien_NhanVien1ID_NhanVien",
                table: "DatLichTuVan",
                column: "NhanVien1ID_NhanVien",
                principalTable: "NhanVien",
                principalColumn: "ID_NhanVien",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_DatLichTuVan_NhanVien_NhanVien2ID_NhanVien",
                table: "DatLichTuVan",
                column: "NhanVien2ID_NhanVien",
                principalTable: "NhanVien",
                principalColumn: "ID_NhanVien",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_KhachHang_CongTy_CongTyID_CongTy",
                table: "KhachHang",
                column: "CongTyID_CongTy",
                principalTable: "CongTy",
                principalColumn: "ID_CongTy",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhieuDangKi_NhanVien_NhanVienID_NhanVien",
                table: "PhieuDangKi",
                column: "NhanVienID_NhanVien",
                principalTable: "NhanVien",
                principalColumn: "ID_NhanVien",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
