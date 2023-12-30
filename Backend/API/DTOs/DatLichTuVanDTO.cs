using API.Domain;

namespace API.DTOs
{
    public class DatLichTuVanDTO
    {
        public int ID_YeuCauTuVan { get; set; }
        public string TinhTrangDuyet { get; set; }
        public string DiaDiem { get; set; }
        public DateTime ThoiGian { get; set; }

        public int ID_KhachHang { get; set; }

        public int? ID_NhanVien1 { get; set; }
        public int? ID_NhanVien2 { get; set; }
    }
}
