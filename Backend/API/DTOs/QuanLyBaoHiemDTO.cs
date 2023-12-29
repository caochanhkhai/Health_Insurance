using API.Domain;

namespace API.DTOs
{
    public class QuanLyBaoHiemDTO
    {
        public int ID { get; set; }
        public int ID_KhachHang { get; set; }
        public int ID_GoiBaoHiem { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public decimal HanMucDaSuDung { get; set; }
    }
}
