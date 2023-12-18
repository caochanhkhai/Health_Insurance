using Microsoft.EntityFrameworkCore;

namespace API.Domain
{
    public class ChiTietChinhSach
    {
        public int ID { get; set; }
        public GoiBaoHiem ID_GoiBaoHiem { get; set; }
        public ChinhSach ID_ChinhSach { get; set; }
        public int STT { get; set; }
        public decimal HanMucChiTra { get; set; }
        public string DieuKienApDung { get; set; }
        public string Mota { get; set; }
    }
}
