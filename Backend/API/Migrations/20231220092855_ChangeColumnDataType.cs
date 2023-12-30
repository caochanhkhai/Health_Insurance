using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class ChangeColumnDataType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Thay đổi kiểu dữ liệu cho các thuộc tính 
            //Bảng TaiKhoan
            migrationBuilder.Sql("ALTER TABLE TaiKhoan ALTER COLUMN TenDangNhap char(10)");
            migrationBuilder.Sql("ALTER TABLE TaiKhoan ALTER COLUMN LoaiTaiKhoan VARCHAR(5)");
            migrationBuilder.Sql("ALTER TABLE TaiKhoan ALTER COLUMN TinhTrang NVARCHAR(9)");

            //Bảng NhanVien
            migrationBuilder.Sql("ALTER TABLE NhanVien ALTER COLUMN HoTen NVARCHAR(30)");
            migrationBuilder.Sql("ALTER TABLE NhanVien ALTER COLUMN GioiTinh NVARCHAR(3)");
            migrationBuilder.Sql("ALTER TABLE NhanVien ALTER COLUMN QuocTich NVARCHAR(30)");
            migrationBuilder.Sql("ALTER TABLE NhanVien ALTER COLUMN NgaySinh DATE");
            migrationBuilder.Sql("ALTER TABLE NhanVien ALTER COLUMN CMND VARCHAR(15)");
            migrationBuilder.Sql("ALTER TABLE NhanVien ALTER COLUMN SoNhaTenDuong NVARCHAR(30)");
            migrationBuilder.Sql("ALTER TABLE NhanVien ALTER COLUMN PhuongXa NVARCHAR(20)");
            migrationBuilder.Sql("ALTER TABLE NhanVien ALTER COLUMN QuanHuyen NVARCHAR(20)");
            migrationBuilder.Sql("ALTER TABLE NhanVien ALTER COLUMN ThanhPho NVARCHAR(20)");
            migrationBuilder.Sql("ALTER TABLE NhanVien ALTER COLUMN Email VARCHAR(30)");
            migrationBuilder.Sql("ALTER TABLE NhanVien ALTER COLUMN LoaiNhanVien NVARCHAR(19)");
            migrationBuilder.Sql("ALTER TABLE NhanVien ALTER COLUMN SoTaiKhoan CHAR(20)");
            migrationBuilder.Sql("ALTER TABLE NhanVien ALTER COLUMN NganHang NVARCHAR(50)");
            migrationBuilder.Sql("ALTER TABLE NhanVien ALTER COLUMN SoDienThoai CHAR(10)");

            //Bang KhachHang
            migrationBuilder.Sql("ALTER TABLE KhachHang ALTER COLUMN HoTen NVARCHAR(30)");
            migrationBuilder.Sql("ALTER TABLE KhachHang ALTER COLUMN GioiTinh NVARCHAR(3)");
            migrationBuilder.Sql("ALTER TABLE KhachHang ALTER COLUMN QuocTich NVARCHAR(30)");
            migrationBuilder.Sql("ALTER TABLE KhachHang ALTER COLUMN NgaySinh DATE");
            migrationBuilder.Sql("ALTER TABLE KhachHang ALTER COLUMN CMND VARCHAR(15)");
            migrationBuilder.Sql("ALTER TABLE KhachHang ALTER COLUMN SoNhaTenDuong NVARCHAR(30)");
            migrationBuilder.Sql("ALTER TABLE KhachHang ALTER COLUMN PhuongXa NVARCHAR(20)");
            migrationBuilder.Sql("ALTER TABLE KhachHang ALTER COLUMN QuanHuyen NVARCHAR(20)");
            migrationBuilder.Sql("ALTER TABLE KhachHang ALTER COLUMN ThanhPho NVARCHAR(20)");
            migrationBuilder.Sql("ALTER TABLE KhachHang ALTER COLUMN Email VARCHAR(30)");
            migrationBuilder.Sql("ALTER TABLE KhachHang ALTER COLUMN NgheNghiep NVARCHAR(20)");
            migrationBuilder.Sql("ALTER TABLE KhachHang ALTER COLUMN ChiTietCongViec NVARCHAR(50)");
            migrationBuilder.Sql("ALTER TABLE KhachHang ALTER COLUMN NganHang NVARCHAR(30)");
            migrationBuilder.Sql("ALTER TABLE KhachHang ALTER COLUMN SoTaiKhoan VARCHAR(20)");
            migrationBuilder.Sql("ALTER TABLE KhachHang ALTER COLUMN SoDienThoai CHAR(10)");

            //Bang CongTy
            migrationBuilder.Sql("ALTER TABLE CongTy ALTER COLUMN TenCongTy NVARCHAR(30)");
            migrationBuilder.Sql("ALTER TABLE CongTy ALTER COLUMN SoNhaTenDuong NVARCHAR(30)");
            migrationBuilder.Sql("ALTER TABLE CongTy ALTER COLUMN PhuongXa NVARCHAR(20)");
            migrationBuilder.Sql("ALTER TABLE CongTy ALTER COLUMN QuanHuyen NVARCHAR(20)");
            migrationBuilder.Sql("ALTER TABLE CongTy ALTER COLUMN ThanhPho NVARCHAR(20)");
            migrationBuilder.Sql("ALTER TABLE CongTy ALTER COLUMN DienThoai CHAR(10)");
            migrationBuilder.Sql("ALTER TABLE CongTy ALTER COLUMN Email VARCHAR(30)");

            //Bang BenhVien
            migrationBuilder.Sql("ALTER TABLE BenhVien ALTER COLUMN TenBenhVien NVARCHAR(30)");
            migrationBuilder.Sql("ALTER TABLE BenhVien ALTER COLUMN DiaChi NVARCHAR(100)");
            migrationBuilder.Sql("ALTER TABLE BenhVien ALTER COLUMN SDT CHAR(10)");
            migrationBuilder.Sql("ALTER TABLE BenhVien ALTER COLUMN Email VARCHAR(30)");

            //Bang GoiBaoHiem
            migrationBuilder.Sql("ALTER TABLE GoiBaoHiem ALTER COLUMN TenBaoHiem NVARCHAR(30)");
            migrationBuilder.Sql("ALTER TABLE GoiBaoHiem ALTER COLUMN TenGoi NVARCHAR(10)");
            migrationBuilder.Sql("ALTER TABLE GoiBaoHiem ALTER COLUMN MoTa NVARCHAR(50)");
            migrationBuilder.Sql("ALTER TABLE GoiBaoHiem ALTER COLUMN NgayPhatHanh DATE");
            migrationBuilder.Sql("ALTER TABLE GoiBaoHiem ALTER COLUMN TinhTrang NVARCHAR(15)");

            //Bang ChinhSach
            migrationBuilder.Sql("ALTER TABLE ChinhSach ALTER COLUMN TenChinhSach NVARCHAR(30)");
            migrationBuilder.Sql("ALTER TABLE ChinhSach ALTER COLUMN ThoiGianPhatHanh DATE");

            //Bang ChiTietChinhSach
            migrationBuilder.Sql("ALTER TABLE ChiTietChinhSach ALTER COLUMN DieuKienApDung NVARCHAR(50)");
            migrationBuilder.Sql("ALTER TABLE ChiTietChinhSach ALTER COLUMN Mota NVARCHAR(50)");

            //Bang DatLichTuVan
            migrationBuilder.Sql("ALTER TABLE DatLichTuVan ALTER COLUMN TinhTrangDuyet NVARCHAR(10)");
            migrationBuilder.Sql("ALTER TABLE DatLichTuVan ALTER COLUMN DiaDiem NVARCHAR(100)");

            //Bang HopDong
            migrationBuilder.Sql("ALTER TABLE HopDong ALTER COLUMN NgayKyKet DATE");
            migrationBuilder.Sql("ALTER TABLE HopDong ALTER COLUMN HieuLuc DATE");

            //Bang PhieuDangKi
            migrationBuilder.Sql("ALTER TABLE PhieuDangKi ALTER COLUMN TinhTrangDuyet NVARCHAR(10)");
            migrationBuilder.Sql("ALTER TABLE PhieuDangKi ALTER COLUMN DiaDiemKiKet NVARCHAR(100)");
            migrationBuilder.Sql("ALTER TABLE PhieuDangKi ALTER COLUMN ThoiGianKiKet DATE");
            migrationBuilder.Sql("ALTER TABLE PhieuDangKi ALTER COLUMN ToKhaiSucKhoe VARCHAR(100)");

            //Bang PhieuThanhToanBaoHiem
            migrationBuilder.Sql("ALTER TABLE PhieuThanhToanBaoHiem ALTER COLUMN NgayThanhToan DATE");
            migrationBuilder.Sql("ALTER TABLE PhieuThanhToanBaoHiem ALTER COLUMN HinhThucThanhToan NVARCHAR(20)");
            migrationBuilder.Sql("ALTER TABLE PhieuThanhToanBaoHiem ALTER COLUMN TinhTrangDuyet NVARCHAR(10)");

            //Bang QuanLyBaoHiem
            migrationBuilder.Sql("ALTER TABLE QuanLyBaoHiem ALTER COLUMN ThoiGianBatDau DATE");
            migrationBuilder.Sql("ALTER TABLE QuanLyBaoHiem ALTER COLUMN ThoiGianKetThuc DATE");

            //Bang YeuCauChiTra
            migrationBuilder.Sql("ALTER TABLE YeuCauChiTra ALTER COLUMN NguoiYeuCau NVARCHAR(30)");
            migrationBuilder.Sql("ALTER TABLE YeuCauChiTra ALTER COLUMN DiaChi NVARCHAR(100)");
            migrationBuilder.Sql("ALTER TABLE YeuCauChiTra ALTER COLUMN DienThoai CHAR(10)");
            migrationBuilder.Sql("ALTER TABLE YeuCauChiTra ALTER COLUMN Email VARCHAR(30)");
            migrationBuilder.Sql("ALTER TABLE YeuCauChiTra ALTER COLUMN MoiQuanHe NVARCHAR(20)");
            migrationBuilder.Sql("ALTER TABLE YeuCauChiTra ALTER COLUMN TruongHopChiTra NVARCHAR(50)");
            migrationBuilder.Sql("ALTER TABLE YeuCauChiTra ALTER COLUMN NgayYeuCau DATE");
            migrationBuilder.Sql("ALTER TABLE YeuCauChiTra ALTER COLUMN TinhTrangDuyet NVARCHAR(10)");
            migrationBuilder.Sql("ALTER TABLE YeuCauChiTra ALTER COLUMN NoiDieuTri NVARCHAR(100)");
            migrationBuilder.Sql("ALTER TABLE YeuCauChiTra ALTER COLUMN ChanDoan NVARCHAR(50)");
            migrationBuilder.Sql("ALTER TABLE YeuCauChiTra ALTER COLUMN HauQua NVARCHAR(50)");
            migrationBuilder.Sql("ALTER TABLE YeuCauChiTra ALTER COLUMN HinhThucDieuTri NVARCHAR(50)");
            migrationBuilder.Sql("ALTER TABLE YeuCauChiTra ALTER COLUMN NgayBatDau DATE");
            migrationBuilder.Sql("ALTER TABLE YeuCauChiTra ALTER COLUMN NgayKetThuc DATE");
            migrationBuilder.Sql("ALTER TABLE YeuCauChiTra ALTER COLUMN HinhHoaDon VARCHAR(100)");

            //Bang LichSuChiTra
            migrationBuilder.Sql("ALTER TABLE LichSuChiTra ALTER COLUMN TenBenhVien NVARCHAR(30)");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}