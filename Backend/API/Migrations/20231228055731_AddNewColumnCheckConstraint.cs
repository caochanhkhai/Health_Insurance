using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddNewColumnCheckConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Bảng KhachHang
            //Cập nhật giá trị cột XacThuc cho tất cả record trong bảng
            migrationBuilder.Sql("Update KhachHang Set XacThuc = N'Chưa Xác Thực'");
            //Thêm ràng buộc Check cho thuộc tính

            migrationBuilder.Sql("ALTER TABLE KhachHang ADD CONSTRAINT CK_KhachHang_XacThuc CHECK (XacThuc IN (N'Đã Xác Thực', N'Chưa Xác Thực'))");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}