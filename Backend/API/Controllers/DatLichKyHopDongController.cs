/* using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.DTOs;
using API.Domain;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatLichKyHopDongController : ControllerBase
    {
        private readonly VHIDbContext VHIDbContext;

        public DatLichKyHopDongController(VHIDbContext VHIDbContext)
        {
            this.VHIDbContext = VHIDbContext;
        }

        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var datLichKyHD = VHIDbContext.PhieuDangKi.FirstOrDefault(a => a.ID_PhieuDangKi == id);
            if (datLichKyHD == null)
            {
                return NotFound();
            }

            var datLichKyHDDto = new PhieuDangKiDTO
            {
                ID_PhieuDangKi = datLichKyHD.ID_PhieuDangKi, 
                TinhTrangDuyet = datLichKyHD.TinhTrangDuyet,
                DiaDiemKiKet = datLichKyHD.DiaDiemKiKet,
                ThoiGianKiKet = datLichKyHD.ThoiGianKiKet,
                ID_KhachHang = datLichKyHD.KhachHangID_KhachHang,
                ID_GoiBaoHiem = datLichKyHD.GoiBaoHiemID_GoiBaoHiem,
                ID_NhanVien = datLichKyHD.NhanVienID_NhanVien
            };

            return Ok(datLichKyHDDto);
        }

        [HttpPost]
        public IActionResult CreateAppointment([FromBody] PhieuDangKiDTO dto)
        {
            var dLichKyHD = new PhieuDangKi
            {
                TinhTrangDuyet = dto.TinhTrangDuyet,
                DiaDiemKiKet = dto.DiaDiemKiKet,
                ThoiGianKiKet = dto.ThoiGianKiKet,
                ID_KhachHang = dto.KhachHangID_KhachHang,
                ID_GoiBaoHiem = dto.GoiBaoHiemID_GoiBaoHiem,
                ID_NhanVien = dto.NhanVienID_NhanVien
            };

            VHIDbContext.PhieuDangKi.Add(dLichKyHD);
            VHIDbContext.SaveChanges();

            var createdDatLichTuVan = new DatLichTuVanDTO
            {
                ID_YeuCauTuVan = dLichTuVan.ID_YeuCauTuVan,
                TinhTrangDuyet = dLichTuVan.TinhTrangDuyet,
                DiaDiem = dLichTuVan.DiaDiem,
                ThoiGian = dLichTuVan.ThoiGian,
//                KhachHangID_KhachHang = dLichTuVan.KhachHangID_KhachHang,
//                NhanVien1ID_NhanVien = dLichTuVan.NhanVien1ID_NhanVien,
//                NhanVien2ID_NhanVien = dLichTuVan.NhanVien2ID_NhanVien,
            };

            return CreatedAtAction(nameof(GetById), new { id = createdDatLichTuVan.ID_YeuCauTuVan }, createdDatLichTuVan);
        }
    }
}
*/