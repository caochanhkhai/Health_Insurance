using Microsoft.EntityFrameworkCore;

namespace API.Domain
{
    public class QuanLyBaoHiem
    {
        public int ID { get; set; }
        public KhachHang KhachHang { get; set; }
        public int KhachHangID_KhachHang { get; set; }

        public GoiBaoHiem GoiBaoHiem { get; set; }
        public int GoiBaoHiemID_GoiBaoHiem { get; set; }

        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public decimal HanMucDaSuDung { get; set; }
    }
}

