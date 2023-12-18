using System.ComponentModel.DataAnnotations;

namespace API.Domain
{
    public class PhieuThanhToanBaoHiem
    {
        [Key] // Sử dụng Data Annotation [Key]
        public int ID_PhieuThanhToan {  get; set; }
        public DateTime NgayThanhToan { get; set; }
        public string HinhThucThanhToan { get; set; }
        public decimal SoTien { get; set; }
        public string TinhTrang { get; set; }
        public HopDong ID_HopDong { get; set; }
    }
}
