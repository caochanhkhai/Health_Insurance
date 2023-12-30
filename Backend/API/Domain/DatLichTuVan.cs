using System.ComponentModel.DataAnnotations;

namespace API.Domain
{
    public class DatLichTuVan
    {
        [Key]
        public int ID_YeuCauTuVan { get; set; }
        public string TinhTrangDuyet { get; set; }
        public string DiaDiem { get; set; }
        public DateTime ThoiGian { get; set; }

        public KhachHang KhachHang { get; set; }
        public int KhachHangID_KhachHang { get; set; }


        public NhanVien? NhanVien1 { get; set; }
        public int? NhanVien1ID_NhanVien { get; set; }

        public NhanVien? NhanVien2 { get; set; }
        public int? NhanVien2ID_NhanVien { get; set; }

    }
}
