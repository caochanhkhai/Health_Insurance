using Microsoft.EntityFrameworkCore;

namespace API.Domain
{
    public class QuanLyBaoHiem
    {
        public int ID { get; set; }
        public KhachHang ID_KhachHang {  get; set; }
        public GoiBaoHiem ID_GoiBaoHiem { get; set; }
        public DateTime ThoiGianBatDau {  get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public decimal HanMucDaSuDung { get; set; }
    }
}
