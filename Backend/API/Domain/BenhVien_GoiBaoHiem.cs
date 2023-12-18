using Microsoft.EntityFrameworkCore;

namespace API.Domain
{
    public class BenhVien_GoiBaoHiem
    {
        public int ID { get; set; }
        public BenhVien ID_BenhVien { get; set; }
        public GoiBaoHiem ID_GoiBaoHiem { get; set; }
        
    }

}
