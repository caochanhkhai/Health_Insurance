namespace API.DTOs
{
    public class AddPhieuDangKiDTO
    {
        public string DiaDiemKiKet { get; set; }
        public DateTime ThoiGianKiKet { get; set; }
        public string ToKhaiSucKhoe { get; set; }

        public int ID_KhachHang { get; set; }
        public int ID_GoiBaoHiem { get; set; }
    }
}
