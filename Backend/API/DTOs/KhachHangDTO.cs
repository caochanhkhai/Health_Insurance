using API.Domain;

namespace API.DTOs
{
    public class KhachHangDTO
    {
        public int ID_KhachHang { get; set; }

        public string HoTen { get; set; }

        public string GioiTinh { get; set; }

        public string QuocTich { get; set; }

        public DateTime NgaySinh { get; set; }

        public int ChieuCao { get; set; }

        public int CanNang { get; set; }

        public string SoNhaTenDuong { get; set; }

        public string PhuongXa { get; set; }

        public string QuanHuyen { get; set; }

        public string ThanhPho { get; set; }

        public string Email { get; set; }

        public string CMND { get; set; }

        public string NgheNghiep { get; set; }

        public string ChiTietCongViec { get; set; }

        public decimal ThuNhap { get; set; }

        public string SoTaiKhoan { get; set; }

        public string NganHang { get; set; }

        public string SoDienThoai { get; set; }

        public int? ID_CongTy { get; set; }

        public int ID_TaiKhoan { get; set; }

        public string XacThuc { get; set; }

    }
}
