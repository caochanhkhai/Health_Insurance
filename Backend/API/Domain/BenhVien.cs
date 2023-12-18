using System.ComponentModel.DataAnnotations;

namespace API.Domain
{
    public class BenhVien
    {
        [Key] // Sử dụng Data Annotation [Key]
        public int ID_BenhVien { get; set; }
        public string TenBenhVien { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string Email { get; set; }
    }
}
