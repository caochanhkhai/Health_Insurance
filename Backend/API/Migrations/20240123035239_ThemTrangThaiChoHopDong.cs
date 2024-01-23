using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class ThemTrangThaiChoHopDong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrangThai",
                table: "HopDong",
                type: "nvarchar(12)",
                nullable: true);

            //Thêm ràng buộc Check cho thuộc tính Trạng thái
            //Bang HopDong
            migrationBuilder.Sql("ALTER TABLE HopDong ADD CONSTRAINT CK_HopDong_TrangThai CHECK (TrangThai IN (N'Dự Thảo', N'Hiệu Lực', N'Hết Hiệu Lực'))");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrangThai",
                table: "HopDong");
        }
    }
}
