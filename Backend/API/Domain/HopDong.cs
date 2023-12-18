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

        public PhieuDangKi ID_PhieuDangKi { get; set; }
        public NhanVien ID_NhanVien { get; set; }
        public KhachHang ID_KhachHang { get; set; }
        public GoiBaoHiem ID_GoiBaoHiem { get; set; }
    }
}
