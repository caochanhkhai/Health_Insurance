using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public KhachHang KhachHang { get; set; }

        public int KhachHangID_KhachHang { get; set; }
        public GoiBaoHiem GoiBaoHiem { get; set; }

        public int GoiBaoHiemID_GoiBaoHiem { get; set; }
        public NhanVien? NhanVien { get; set; }
        public int? NhanVienID_NhanVien { get; set; }
    }
}
