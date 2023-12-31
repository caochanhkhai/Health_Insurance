namespace API.DTOs
{
    public class AddPhieuThanhToanBaoHiemDTO
    {
        public DateTime NgayThanhToan { get; set; }
        public string HinhThucThanhToan { get; set; }
        public decimal SoTien { get; set; }
        public int ID_HopDong { get; set; }
    }
}
