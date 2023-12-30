using API.Domain;

namespace API.DTOs
{
    public class PhieuDangKiDTO
    {
        public int ID_PhieuDangKi { get; set; }
        public string TinhTrangDuyet { get; set; }
        public string DiaDiemKiKet { get; set; }
        public DateTime ThoiGianKiKet { get; set; }
        public string ToKhaiSucKhoe { get; set; }

        public int ID_KhachHang { get; set; }
        public int ID_GoiBaoHiem { get; set; }
        public int? ID_NhanVien { get; set; }

    }
}
