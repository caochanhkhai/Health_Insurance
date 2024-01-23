using API.Domain;

namespace API.DTOs
{
    public class HopDongDTO
    {
        public int ID_HopDong { get; set; }
        public DateTime NgayKyKet { get; set; }
        public int ThoiHan { get; set; }
        public decimal GiaTriHopDong { get; set; }
        public string DieuKhoan { get; set; }
        public DateTime HieuLuc { get; set; }

        public int ID_PhieuDangKi { get; set; }

        public int ID_NhanVien { get; set; }

        public int ID_KhachHang { get; set; }

        public int ID_GoiBaoHiem { get; set; }
        public string? TrangThai { get; set; }
    }
}
