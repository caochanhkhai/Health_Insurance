using API.Domain;

namespace API.DTOs
{
    public class LichSuChiTraDTO
    {
        public int ID { get; set; }
        public YeuCauChiTra ID_YeuCauChiTra { get; set; }
        public string TenBenhVien { get; set; }
        public DateTime ThoiGianChiTra { get; set; }
        public decimal SoTienChiTra { get; set; }
    }
}
