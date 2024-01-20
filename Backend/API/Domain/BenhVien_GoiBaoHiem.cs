using Microsoft.EntityFrameworkCore;

namespace API.Domain
{
    public class BenhVien_GoiBaoHiem
    {
        public int ID { get; set; }
        public BenhVien BenhVien { get; set; }
        public int BenhVienID_BenhVien { get; set; }
        public GoiBaoHiem GoiBaoHiem { get; set; }
        public int GoiBaoHiemID_GoiBaoHiem { get; set; }


    }

}
