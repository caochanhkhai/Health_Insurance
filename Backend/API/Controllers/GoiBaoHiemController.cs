using API.Data;
using API.Domain;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoiBaoHiemController : ControllerBase
    {
        private readonly VHIDbContext VHIDbContext;

        public GoiBaoHiemController(VHIDbContext VHIDbContext)
        {
            this.VHIDbContext = VHIDbContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var goiBH = VHIDbContext.GoiBaoHiem.ToList();
            return Ok(goiBH);
        }

        [HttpPost]
        [Route("ThemGoiBaoHiem")]
        public IActionResult ThemCS([FromBody] GoiBaoHiemDTO dto)
        {

            GoiBaoHiem gbh_Domain = new GoiBaoHiem()
            {
                TenBaoHiem = dto.TenBaoHiem,
                TenGoi = dto.TenGoi,
                GiaTien = dto.GiaTien,
                ThoiHan = dto.ThoiHan,
                MoTa = dto.MoTa,
                NgayPhatHanh = dto.NgayPhatHanh,
                TinhTrang = dto.TinhTrang,  
                HinhAnh = dto.HinhAnh
            };

            VHIDbContext.GoiBaoHiem.Add(gbh_Domain);
            VHIDbContext.SaveChanges();

            GoiBaoHiemDTO gbhDTO = CreateGBHDTO(gbh_Domain);

            return Ok(gbhDTO);
        }

        [HttpPut("CapNhatGoiBaoHiem/{id}")]
        public IActionResult CapNhatGBH([FromRoute] int id, [FromBody] GoiBaoHiemDTO? dto)
        {
            var gbhDomain = VHIDbContext.GoiBaoHiem.FirstOrDefault(x => x.ID_GoiBaoHiem == id);

            if (gbhDomain == null)
            {
                return NotFound("Không tìm thấy gói bảo hiểm.");
            }
            // Cập nhật và lưu vào cơ sở dữ liệu
            gbhDomain.TenBaoHiem = dto.TenBaoHiem;
            gbhDomain.TenGoi= dto.TenGoi;
            gbhDomain.GiaTien= dto.GiaTien;
            gbhDomain.ThoiHan= dto.ThoiHan;
            gbhDomain.MoTa= dto.MoTa;
            gbhDomain.NgayPhatHanh= dto.NgayPhatHanh;
            gbhDomain.TinhTrang= dto.TinhTrang;
            gbhDomain.HinhAnh= dto.HinhAnh;
            VHIDbContext.SaveChanges();

            GoiBaoHiemDTO gbhDTO = CreateGBHDTO(gbhDomain);

            return Ok(gbhDTO);
        }

        private static GoiBaoHiemDTO CreateGBHDTO(GoiBaoHiem? gbh)
        {
            return new GoiBaoHiemDTO()
            {
                ID_GoiBaoHiem = gbh.ID_GoiBaoHiem,
                TenBaoHiem = gbh.TenBaoHiem,
                TenGoi = gbh.TenGoi,
                GiaTien = gbh.GiaTien,
                ThoiHan = gbh.ThoiHan,
                MoTa = gbh.MoTa,
                NgayPhatHanh = gbh.NgayPhatHanh,
                TinhTrang = gbh.TinhTrang,
                HinhAnh = gbh.HinhAnh
            };
        }

    }
}
