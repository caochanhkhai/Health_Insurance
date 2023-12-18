using API.Domain;

namespace API.DTOs
{
    public class ChiTietChinhSachDTO
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
