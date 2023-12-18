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
            migrationBuilder.DropForeignKey(
                name: "FK_YeuCauChiTra_GoiBaoHiem_ID_GoiBaoHiem1",
                table: "YeuCauChiTra");

            migrationBuilder.DropIndex(
                name: "IX_YeuCauChiTra_ID_GoiBaoHiem1",
                table: "YeuCauChiTra");

            migrationBuilder.DropColumn(
                name: "ID_GoiBaoHiem1",
                table: "YeuCauChiTra");

            migrationBuilder.AddColumn<int>(
                name: "ID_GoiBaoHiem1",
                table: "PhieuDangKi",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ID_GoiBaoHiem1",
                table: "HopDong",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PhieuDangKi_ID_GoiBaoHiem1",
                table: "PhieuDangKi",
                column: "ID_GoiBaoHiem1");

            migrationBuilder.CreateIndex(
                name: "IX_HopDong_ID_GoiBaoHiem1",
                table: "HopDong",
                column: "ID_GoiBaoHiem1");

            migrationBuilder.AddForeignKey(
                name: "FK_HopDong_GoiBaoHiem_ID_GoiBaoHiem1",
                table: "HopDong",
                column: "ID_GoiBaoHiem1",
                principalTable: "GoiBaoHiem",
                principalColumn: "ID_GoiBaoHiem",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_PhieuDangKi_GoiBaoHiem_ID_GoiBaoHiem1",
                table: "PhieuDangKi",
                column: "ID_GoiBaoHiem1",
                principalTable: "GoiBaoHiem",
                principalColumn: "ID_GoiBaoHiem",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HopDong_GoiBaoHiem_ID_GoiBaoHiem1",
                table: "HopDong");

            migrationBuilder.DropForeignKey(
                name: "FK_PhieuDangKi_GoiBaoHiem_ID_GoiBaoHiem1",
                table: "PhieuDangKi");

            migrationBuilder.DropIndex(
                name: "IX_PhieuDangKi_ID_GoiBaoHiem1",
                table: "PhieuDangKi");

            migrationBuilder.DropIndex(
                name: "IX_HopDong_ID_GoiBaoHiem1",
                table: "HopDong");

            migrationBuilder.DropColumn(
                name: "ID_GoiBaoHiem1",
                table: "PhieuDangKi");

            migrationBuilder.DropColumn(
                name: "ID_GoiBaoHiem1",
                table: "HopDong");

            migrationBuilder.AddColumn<int>(
                name: "ID_GoiBaoHiem1",
                table: "YeuCauChiTra",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_YeuCauChiTra_ID_GoiBaoHiem1",
                table: "YeuCauChiTra",
                column: "ID_GoiBaoHiem1");

            migrationBuilder.AddForeignKey(
                name: "FK_YeuCauChiTra_GoiBaoHiem_ID_GoiBaoHiem1",
                table: "YeuCauChiTra",
                column: "ID_GoiBaoHiem1",
                principalTable: "GoiBaoHiem",
                principalColumn: "ID_GoiBaoHiem",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
