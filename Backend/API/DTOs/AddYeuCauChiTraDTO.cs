namespace API.DTOs
{
    public class AddYeuCauChiTraDTO
    {
        public int QLBHID { get; set; }
        public string NguoiYeuCau { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public string MoiQuanHe { get; set; }
        public decimal SoTienYeuCauChiTra { get; set; }
        public string TruongHopChiTra { get; set; }
        public DateTime NgayYeuCau { get; set; }
        public string NoiDieuTri { get; set; }
        public string ChanDoan { get; set; }
        public string HauQua { get; set; }
        public string HinhThucDieuTri { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string HinhHoaDon { get; set; }
    }
}