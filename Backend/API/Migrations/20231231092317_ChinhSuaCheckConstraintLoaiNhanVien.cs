using API.Domain;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Data;
using System.Diagnostics.Metrics;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class ChinhSuaCheckConstraintLoaiNhanVien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Xóa Check Constraint cho LoaiNhanVien trong bảng NhanVien và tạo lại
            migrationBuilder.Sql("ALTER TABLE NhanVien DROP CONSTRAINT CK_NhanVien_LoaiNhanVien");
            migrationBuilder.Sql("ALTER TABLE NhanVien ADD CONSTRAINT CK_NhanVien_LoaiNhanVien CHECK (LoaiNhanVien IN (N'Nhân viên', N'Quản trị viên', N'Nhân viên tài chính'))");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
