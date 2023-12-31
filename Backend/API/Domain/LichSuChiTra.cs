using System.ComponentModel.DataAnnotations;

namespace API.Domain
{
    public class LichSuChiTra
    {
        public int ID { get; set; }
        public YeuCauChiTra YeuCauChiTra { get; set; }
        public int YeuCauChiTraID_YeuCauChiTra { get; set; }
        public string TenBenhVien { get; set; }
        public DateTime ThoiGianChiTra { get; set; }
        public decimal SoTienChiTra { get; set; }
    }
}
