using System.ComponentModel.DataAnnotations;

namespace API.Domain
{
    public class TaiKhoan
    {
        [Key]
        public int ID_TaiKhoan { get; set; }

        public string TenDangNhap { get; set; }

        public string MatKhau { get; set; }

        public string LoaiTaiKhoan { get; set; }

        public string TinhTrang { get; set; }
    }
}
