using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class BoSTTChinhSach_ThemConstraintUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DieuKienApDung",
                table: "ChiTietChinhSach");

            migrationBuilder.DropColumn(
                name: "HanMucChiTra",
                table: "ChiTietChinhSach");

            migrationBuilder.DropColumn(
                name: "Mota",
                table: "ChiTietChinhSach");

            migrationBuilder.DropColumn(
                name: "STT",
                table: "ChiTietChinhSach");

            migrationBuilder.DropColumn(
                name: "STT",
                table: "ChinhSach");

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayBatDau",
                table: "ChiTietChinhSach",
                type: "date",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayKetThuc",
                table: "ChiTietChinhSach",
                type: "date",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DieuKienApDung",
                table: "ChinhSach",
                type: "nvarchar(50)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "HanMucChiTra",
                table: "ChinhSach",
                type: "decimal(18,2)",
                nullable: true,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Mota",
                table: "ChinhSach",
                type: "nvarchar(100)",
                nullable: true,
                defaultValue: "");

            //////Thêm ràng buộc duy nhất cho một số thuộc tính
            //Bảng CongTy
            migrationBuilder.Sql("ALTER TABLE CongTy ADD CONSTRAINT UC_CongTy_DienThoai UNIQUE(DienThoai)");
            migrationBuilder.Sql("ALTER TABLE CongTy ADD CONSTRAINT UC_CongTy_Email UNIQUE(Email)");

            //Bảng TaiKhoan
            migrationBuilder.Sql("ALTER TABLE TaiKhoan ADD CONSTRAINT UC_TaiKhoan_TenDangNhap UNIQUE(TenDangNhap)");

            //Bảng NhanVien
            migrationBuilder.Sql("ALTER TABLE NhanVien ADD CONSTRAINT UC_NhanVien_CMND UNIQUE(CMND)");
            migrationBuilder.Sql("ALTER TABLE NhanVien ADD CONSTRAINT UC_NhanVien_Email UNIQUE(Email)");
            migrationBuilder.Sql("ALTER TABLE NhanVien ADD CONSTRAINT UC_NhanVien_SoTaiKhoan UNIQUE(SoTaiKhoan)");
            migrationBuilder.Sql("ALTER TABLE NhanVien ADD CONSTRAINT UC_NhanVien_SoDienThoai UNIQUE(SoDienThoai)");

            //Bảng KhachHang
            migrationBuilder.Sql("ALTER TABLE KhachHang ADD CONSTRAINT UC_KhachHang_CMND UNIQUE(CMND)");
            migrationBuilder.Sql("ALTER TABLE KhachHang ADD CONSTRAINT UC_KhachHang_Email UNIQUE(Email)");
            migrationBuilder.Sql("ALTER TABLE KhachHang ADD CONSTRAINT UC_KhachHang_SoTaiKhoan UNIQUE(SoTaiKhoan)");
            migrationBuilder.Sql("ALTER TABLE KhachHang ADD CONSTRAINT UC_KhachHang_SoDienThoai UNIQUE(SoDienThoai)");

            //Bảng BenhVien
            migrationBuilder.Sql("ALTER TABLE BenhVien ADD CONSTRAINT UC_BenhVien_Email UNIQUE(Email)");
            migrationBuilder.Sql("ALTER TABLE BenhVien ADD CONSTRAINT UC_BenhVien_SDT UNIQUE(SDT)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgayBatDau",
                table: "ChiTietChinhSach");

            migrationBuilder.DropColumn(
                name: "NgayKetThuc",
                table: "ChiTietChinhSach");

            migrationBuilder.DropColumn(
                name: "DieuKienApDung",
                table: "ChinhSach");

            migrationBuilder.DropColumn(
                name: "HanMucChiTra",
                table: "ChinhSach");

            migrationBuilder.DropColumn(
                name: "Mota",
                table: "ChinhSach");

            migrationBuilder.AddColumn<string>(
                name: "DieuKienApDung",
                table: "ChiTietChinhSach",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "HanMucChiTra",
                table: "ChiTietChinhSach",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Mota",
                table: "ChiTietChinhSach",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "STT",
                table: "ChiTietChinhSach",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "STT",
                table: "ChinhSach",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
