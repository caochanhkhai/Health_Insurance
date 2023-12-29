using System.ComponentModel.DataAnnotations;

namespace API.Domain
{
    public class CongTy
    {
        [Key]
        public int ID_CongTy { get; set; }
        public string TenCongTy { get; set; }
        public string SoNhaTenDuong { get; set; }
        public string PhuongXa { get; set; }
        public string QuanHuyen { get; set; }
        public string ThanhPho { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
    }
}
