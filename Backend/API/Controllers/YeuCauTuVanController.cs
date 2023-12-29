using API.Data;
using API.Domain;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YeuCauTuVanController : ControllerBase
    {
        private readonly VHIDbContext VHIDbContext;

        public YeuCauTuVanController(VHIDbContext VHIDbContext)
        {
            this.VHIDbContext = VHIDbContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var dltv = VHIDbContext.DatLichTuVan.ToList();
            List<DatLichTuVanDTO> dltvDTO = new List<DatLichTuVanDTO>();
            foreach (var dltv_domain in dltv)
            {
                DatLichTuVanDTO dltv_dto = new DatLichTuVanDTO()
                {
                    ID_YeuCauTuVan = dltv_domain.ID_YeuCauTuVan,
                    TinhTrangDuyet = dltv_domain.TinhTrangDuyet,
                    DiaDiem = dltv_domain.DiaDiem,
                    ThoiGian = dltv_domain.ThoiGian,
                    ID_KhachHang = dltv_domain.KhachHangID_KhachHang,
                    ID_NhanVien1 = dltv_domain.NhanVien1ID_NhanVien,
                    ID_NhanVien2 = dltv_domain.NhanVien2ID_NhanVien
                };
                dltvDTO.Add(dltv_dto);
            }
            return Ok(dltvDTO);
        }

        [HttpGet]
        [Route("id:int")]
        public IActionResult GetById(int id)
        {
            var yctv = VHIDbContext.DatLichTuVan.FirstOrDefault(x => x.ID_YeuCauTuVan == id);
            if (yctv == null)
            {
                return NotFound();
            }
            var yctv_dto = new DatLichTuVanDTO();
            yctv_dto.ID_YeuCauTuVan = yctv.ID_YeuCauTuVan;
            yctv_dto.TinhTrangDuyet = yctv.TinhTrangDuyet;
            yctv_dto.DiaDiem = yctv.DiaDiem;
            yctv_dto.ThoiGian = yctv.ThoiGian;
            yctv_dto.ID_KhachHang = yctv.KhachHangID_KhachHang;
            yctv_dto.ID_NhanVien1 = yctv.NhanVien1ID_NhanVien;
            yctv_dto.ID_NhanVien2 = yctv.NhanVien2ID_NhanVien;
            return Ok(yctv_dto);
        }

        [HttpPost]
        [Route("DatLichTuVan")]
        public IActionResult DatLichTuVan([FromBody] DatLichTuVanDTO dto)
        {

            var DatLichTuVanDomain = new DatLichTuVan()
            {
                TinhTrangDuyet = "Chưa Duyệt",
                DiaDiem = dto.DiaDiem,
                ThoiGian = dto.ThoiGian,
                KhachHangID_KhachHang = dto.ID_KhachHang,
                NhanVien1ID_NhanVien = dto.ID_NhanVien1,
                NhanVien2ID_NhanVien = dto.ID_NhanVien2
            };
            VHIDbContext.DatLichTuVan.Add(DatLichTuVanDomain);
            VHIDbContext.SaveChanges();

            var DatLichTuVan_dto = new DatLichTuVanDTO()
            {
                ID_YeuCauTuVan = DatLichTuVanDomain.ID_YeuCauTuVan,
                TinhTrangDuyet = DatLichTuVanDomain.TinhTrangDuyet,
                DiaDiem = DatLichTuVanDomain.DiaDiem,
                ThoiGian = DatLichTuVanDomain.ThoiGian,
                ID_KhachHang = DatLichTuVanDomain.KhachHangID_KhachHang,
                ID_NhanVien1 = DatLichTuVanDomain.NhanVien1ID_NhanVien,
                ID_NhanVien2 = DatLichTuVanDomain.NhanVien2ID_NhanVien
            };
            return CreatedAtAction(nameof(GetById), new { id = DatLichTuVan_dto.ID_YeuCauTuVan }, DatLichTuVan_dto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] DatLichTuVanDTO dto)
        {
            var dltvDomain = VHIDbContext.DatLichTuVan.FirstOrDefault(x => x.ID_YeuCauTuVan == id);
            
            if (dltvDomain == null)
            {
                return NotFound();
            }
            dltvDomain.TinhTrangDuyet = dto.TinhTrangDuyet;
            dltvDomain.DiaDiem = dto.DiaDiem;
            dltvDomain.ThoiGian = dto.ThoiGian;
            dltvDomain.KhachHangID_KhachHang = dto.ID_KhachHang;
            dltvDomain.NhanVien1ID_NhanVien = dto.ID_NhanVien1;
            dltvDomain.NhanVien2ID_NhanVien = dto.ID_NhanVien2;
            VHIDbContext.SaveChanges();
            var updated_dltv_dto = new DatLichTuVanDTO()
            {
                TinhTrangDuyet = dltvDomain.TinhTrangDuyet,
                DiaDiem = dltvDomain.DiaDiem,
                ThoiGian = dltvDomain.ThoiGian,
                ID_KhachHang = dltvDomain.KhachHangID_KhachHang,
                ID_NhanVien1 = dltvDomain.NhanVien1ID_NhanVien,
                ID_NhanVien2 = dltvDomain.NhanVien2ID_NhanVien,

            };
            return Ok(updated_dltv_dto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var dltvDomain = VHIDbContext.DatLichTuVan.FirstOrDefault(x => x.ID_YeuCauTuVan == id);
            if (dltvDomain == null)
            {
                return NotFound();
            }
            VHIDbContext.DatLichTuVan.Remove(dltvDomain);
            VHIDbContext.SaveChanges();
            var dltvDTO = new DatLichTuVanDTO()
            {
                ID_YeuCauTuVan = dltvDomain.ID_YeuCauTuVan,
                TinhTrangDuyet = dltvDomain.TinhTrangDuyet,
                DiaDiem = dltvDomain.DiaDiem,
                ThoiGian = dltvDomain.ThoiGian,
                ID_KhachHang = dltvDomain.KhachHangID_KhachHang,
                ID_NhanVien1 = dltvDomain.NhanVien1ID_NhanVien,
                ID_NhanVien2 = dltvDomain.NhanVien2ID_NhanVien,
            };
            return Ok(dltvDTO);
        }

    }
}
