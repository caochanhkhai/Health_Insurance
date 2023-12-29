using Microsoft.AspNetCore.Http;
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
            var datLichTuVan = VHIDbContext.DatLichTuVan.FirstOrDefault(a => a.ID_YeuCauTuVan == id);
            if (datLichTuVan == null)
            {
                return NotFound();
            }

            var DatLichTuVanDto = new DatLichTuVanDTO
            {
                ID_YeuCauTuVan = datLichTuVan.ID_YeuCauTuVan,
                TinhTrangDuyet = datLichTuVan.TinhTrangDuyet,
                DiaDiem = datLichTuVan.DiaDiem,
                ThoiGian = datLichTuVan.ThoiGian,
//                KhachHangID_KhachHang = datLichTuVan.KhachHangID_KhachHang,
//                NhanVien1ID_NhanVien = datLichTuVan.NhanVien1ID_NhanVien,
//                NhanVien2ID_NhanVien = datLichTuVan.NhanVien2ID_NhanVien,
            };

            return Ok(DatLichTuVanDto);
        }

        [HttpPost]
        public IActionResult CreateAppointment([FromBody] DatLichTuVanDTO dto)
        {
            var dLichTuVan = new DatLichTuVan
            {
                TinhTrangDuyet = dto.TinhTrangDuyet,
                DiaDiem = dto.DiaDiem,
                ThoiGian = dto.ThoiGian,
//                KhachHangID_KhachHang = dto.KhachHangID_KhachHang,
//                NhanVien1ID_NhanVien = dto.NhanVien1ID_NhanVien,
//                NhanVien2ID_NhanVien = dto.NhanVien2ID_NhanVien,
            };

            VHIDbContext.DatLichTuVan.Add(dLichTuVan);
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
