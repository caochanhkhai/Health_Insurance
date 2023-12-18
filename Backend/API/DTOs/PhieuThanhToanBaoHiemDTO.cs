namespace API.DTOs
{
    public class PhieuThanhToanBaoHiemDTO
    {
        public int ID_PhieuThanhToan { get; set; }
        public DateTime NgayThanhToan { get; set; }
        public string HinhThucThanhToan { get; set; }
        public decimal SoTien { get; set; }
        public string TinhTrang { get; set; }
        public int ID_HopDong { get; set; }
    }
}
