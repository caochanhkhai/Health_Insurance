using API.Domain;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class DatLichTuVanDTO
    {
        public int ID_YeuCauTuVan { get; set; }
        public string TinhTrangDuyet { get; set; }
        public string DiaDiem { get; set; }
        public DateTime ThoiGian { get; set; }
        public int KhachHangID_KhachHang { get; set; }
        public int NhanVien1ID_NhanVien { get; set; }
        public int NhanVien2ID_NhanVien { get; set; }
    }
}
