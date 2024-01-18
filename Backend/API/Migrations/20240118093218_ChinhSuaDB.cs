using API.Domain;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Data;
using System.Diagnostics.Metrics;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class ChinhSuaDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Chuyển 3 thuộc tính bên bảng ChiTietChinhSach qua bảng ChinhSach
            migrationBuilder.Sql("ALTER TABLE ChiTietChinhSach DROP COLUMN HanMucChiTra");
            migrationBuilder.Sql("ALTER TABLE ChiTietChinhSach DROP COLUMN DieuKienApDung");
            migrationBuilder.Sql("ALTER TABLE ChiTietChinhSach DROP COLUMN MoTa");

            migrationBuilder.Sql("ALTER TABLE ChinhSach ADD HanMucChiTra DECIMAL(18,2)");
            migrationBuilder.Sql("ALTER TABLE ChinhSach ADD DieuKienApDung NVARCHAR(50)");
            migrationBuilder.Sql("ALTER TABLE ChinhSach ADD MoTa NVARCHAR(100)");

            //Thêm 2 thuộc tính NgayBatDau và NgayKetThuc cho ChiTietChinhSach
            migrationBuilder.Sql("ALTER TABLE ChiTietChinhSach ADD NgayBatDau DATE");
            migrationBuilder.Sql("ALTER TABLE ChiTietChinhSach ADD NgayKetThuc DATE");

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

            //Chỉnh sửa lại khóa chính
            //Bảng ChinhSach
            migrationBuilder.Sql("ALTER TABLE ChiTietChinhSach DROP CONSTRAINT [FK_ChiTietChinhSach_ChinhSach_ChinhSachID_ChinhSach]");
            migrationBuilder.Sql("ALTER TABLE ChinhSach DROP CONSTRAINT [PK_ChinhSach]");
            migrationBuilder.Sql("ALTER TABLE ChinhSach ADD CONSTRAINT New_PK_ChinhSach PRIMARY KEY (ID_ChinhSach, STT);");
            migrationBuilder.Sql("ALTER TABLE ChiTietChinhSach ADD CONSTRAINT [FK_ChiTietChinhSach_ChinhSach] FOREIGN KEY (ChinhSachID_ChinhSach,STT) REFERENCES ChinhSach (ID_ChinhSach,STT)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
