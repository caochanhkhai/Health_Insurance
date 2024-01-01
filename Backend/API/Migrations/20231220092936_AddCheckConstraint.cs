using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddCheckConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Thêm ràng buộc Check cho thuộc tính
            //Bảng NhanVien
            migrationBuilder.Sql("ALTER TABLE NhanVien ADD CONSTRAINT CK_NhanVien_LoaiNhanVien CHECK (LoaiNhanVien IN (N'Nhân viên', N'Quản trị viên', N'Nhân viên tài chính'))");
            migrationBuilder.Sql("ALTER TABLE NhanVien ADD CONSTRAINT CK_NhanVien_GioiTinh CHECK (GioiTinh IN ('Nam',N'Nữ'))");

            //Bang KhachHang
            migrationBuilder.Sql("ALTER TABLE KhachHang ADD CONSTRAINT CK_KhachHang_GioiTinh CHECK (GioiTinh IN ('Nam',N'Nữ'))");

            //Bang GoiBaoHiem
            migrationBuilder.Sql("ALTER TABLE GoiBaoHiem ADD CONSTRAINT CK_GoiBaoHiem_TinhTrang CHECK (TinhTrang IN (N'Đang phát hành', N'Ngưng phát hành'))");

            //Bang DatLichTuVan
            migrationBuilder.Sql("ALTER TABLE DatLichTuVan ADD CONSTRAINT CK_DatLichTuVan_TinhTrangDuyet CHECK (TinhTrangDuyet IN (N'Chưa Duyệt',N'Đã Duyệt', N'Từ Chối'))");

            //Bang PhieuDangKi
            migrationBuilder.Sql("ALTER TABLE PhieuDangKi ADD CONSTRAINT CK_PhieuDangKi_TinhTrangDuyet CHECK (TinhTrangDuyet IN (N'Chưa Duyệt',N'Đã Duyệt', N'Từ Chối'))");

            //Bang PhieuThanhToanBaoHiem
            migrationBuilder.Sql("ALTER TABLE PhieuThanhToanBaoHiem ADD CONSTRAINT CK_PhieuThanhToanBaoHiem_HinhThucThanhToan CHECK (HinhThucThanhToan IN (N'Chuyển Khoản',N'Tiền mặt'))");
            migrationBuilder.Sql("ALTER TABLE PhieuThanhToanBaoHiem ADD CONSTRAINT CK_PhieuThanhToanBaoHiem_TinhTrang CHECK (TinhTrangDuyet IN (N'Chưa Duyệt',N'Đã Duyệt', N'Từ Chối'))");

            //Bang YeuCauChiTra
            migrationBuilder.Sql("ALTER TABLE YeuCauChiTra ADD CONSTRAINT CK_YeuCauChiTra_TinhTrangDuyet CHECK (TinhTrangDuyet IN (N'Chưa Duyệt',N'Đã Duyệt', N'Từ Chối'))");

            //Bảng TaiKhoan
            migrationBuilder.Sql("ALTER TABLE TaiKhoan ADD CONSTRAINT CK_Taikhoan_LoaiTaiKhoan CHECK (LoaiTaiKhoan IN ('NV','ADMIN','NVTC','KH'))");
            migrationBuilder.Sql("ALTER TABLE TaiKhoan ADD CONSTRAINT CK_Taikhoan_TinhTrang CHECK (TinhTrang IN (N'Hoạt động',N'Đã Khóa'))");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}