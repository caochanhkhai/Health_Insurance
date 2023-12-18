using System.ComponentModel.DataAnnotations;

namespace API.Domain
{
    public class PhieuDangKi
    {
        [Key]
        public int ID_PhieuDangKi {  get; set; }
        public string TinhTrangDuyet {  get; set; }
        public string DiaDiemKiKet { get; set; }
        public DateTime ThoiGianKiKet { get; set; }
        public string ToKhaiSucKhoe {  get; set; }

        public KhachHang ID_KhachHang { get; set; }
        public GoiBaoHiem ID_GoiBaoHiem { get; set; }
    }
}
