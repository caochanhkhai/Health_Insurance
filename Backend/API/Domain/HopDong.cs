using System.ComponentModel.DataAnnotations;

namespace API.Domain
{
    public class HopDong
    {
        [Key]
        public int ID_HopDong { get; set; }
        public DateTime NgayKyKet { get; set; }
        public int ThoiHan { get; set; }
        public decimal GiaTriHopDong { get; set; }
        public string DieuKhoan { get; set; }
        public DateTime HieuLuc { get; set; }

        public PhieuDangKi PhieuDangKi { get; set; }
        public int PhieuDangKiID_PhieuDangKi { get; set; }

        public NhanVien NhanVien { get; set; }
        public int NhanVienID_NhanVien { get; set; }    

        public KhachHang KhachHang { get; set; }
        public int KhachHangID_KhachHang { get; set; }

        public GoiBaoHiem GoiBaoHiem { get; set; }
        public int GoiBaoHiemID_GoiBaoHiem { get; set; }
        public string? TrangThai {  get; set; }
    }
}
