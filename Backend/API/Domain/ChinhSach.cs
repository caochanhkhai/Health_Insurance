using System.ComponentModel.DataAnnotations;

namespace API.Domain
{
    public class ChinhSach
    {
        [Key] // Sử dụng Data Annotation [Key]
        public int ID_ChinhSach { get; set; }
        //public int STT {  get; set; }
        public string TenChinhSach { get; set; }
        public DateTime ThoiGianPhatHanh { get; set; }
        public decimal HanMucChiTra { get; set; }
        public string DieuKienApDung { get; set; }
        public string Mota { get; set; }
    }
}
