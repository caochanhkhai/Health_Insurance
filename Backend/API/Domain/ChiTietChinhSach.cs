using Microsoft.EntityFrameworkCore;

namespace API.Domain
{
    public class ChiTietChinhSach
    {
        public int ID { get; set; }
        public GoiBaoHiem GoiBaoHiem { get; set; }
        public int GoiBaoHiemID_GoiBaoHiem { get; set; }
        public ChinhSach ChinhSach { get; set; }
        public int ChinhSachID_ChinhSach { get; set; }
        
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
    }
}
