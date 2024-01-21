namespace API.DTOs
{
    public class AddQuanLyBaoHiemDTO
    {
        public int ID_KhachHang { get; set; }
        public int ID_GoiBaoHiem { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
    }
}
