using System.ComponentModel.DataAnnotations;

namespace API.Domain
{
    public class DatLichTuVan
    {
        [Key]
        public int ID_YeuCauTuVan {  get; set; }
        public string TinhTrangDuyet {get; set; }
        public string DiaDiem { get; set; }
        public DateTime ThoiGian {  get; set; }

        public KhachHang ID_KhachHang { get; set; }
        
        public NhanVien ID_NhanVien1 { get; set; }
        public NhanVien ID_NhanVien2 { get; set; }
    }
}

