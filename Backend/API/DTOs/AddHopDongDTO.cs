namespace API.DTOs
{
    public class AddHopDongDTO
    {
        public DateTime NgayKyKet { get; set; }
        public int ThoiHan { get; set; }
        public string DieuKhoan { get; set; }
        public DateTime HieuLuc { get; set; }

        public int ID_PhieuDangKi { get; set; }
    }
}
