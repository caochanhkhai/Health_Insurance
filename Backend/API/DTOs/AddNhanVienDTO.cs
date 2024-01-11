using API.Domain;

namespace API.DTOs
{
    public class AddNhanVienDTO
    {
        public string HoTen { get; set; }

        public string GioiTinh { get; set; }

        public string QuocTich { get; set; }

        public DateTime NgaySinh { get; set; }

        public string CMND { get; set; }

        public string SoNhaTenDuong { get; set; }

        public string PhuongXa { get; set; }

        public string QuanHuyen { get; set; }

        public string ThanhPho { get; set; }

        public string Email { get; set; }

        public string LoaiNhanVien { get; set; }

        public string SoTaiKhoan { get; set; }

        public string NganHang { get; set; }

        public string SoDienThoai { get; set; }

        public int ID_TaiKhoan { get; set; }
    }
}
