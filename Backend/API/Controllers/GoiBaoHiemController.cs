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
            var dsgbhDomain = VHIDbContext.GoiBaoHiem.ToList();
            List<GoiBaoHiemDTO> dsgbhDTO = new List<GoiBaoHiemDTO>();
            foreach (var gbh in dsgbhDomain)
            {
                GoiBaoHiemDTO gbh_dto = CreateGoiBaoHiemDTO(gbh);
                dsgbhDTO.Add(gbh_dto);
            }

            return Ok(dsgbhDTO);
        }

        [HttpGet]
        [Route("id")]
        public IActionResult GetById(int id)
        {
            var gbh = VHIDbContext.GoiBaoHiem.FirstOrDefault(x => x.ID_GoiBaoHiem == id);
            if (gbh == null)
            {
                return NotFound();
            }
            GoiBaoHiemDTO gbh_dto = CreateGoiBaoHiemDTO(gbh);

            return Ok(gbh_dto);
        }

        [HttpPut]
        [Route("UpdateHinhAnh(idgbh)")]
        public IActionResult UpdateGoiBaoHiem_HinhAnh(int idgbh, string LinkHinhAnh)
        {
            var gbhDomain = VHIDbContext.GoiBaoHiem.FirstOrDefault(x => x.ID_GoiBaoHiem == idgbh);
            if (gbhDomain == null)
            {
                return NotFound("Không tồn tại gói bảo hiểm này!");
            }

            gbhDomain.HinhAnh = LinkHinhAnh;

            VHIDbContext.SaveChanges();
            GoiBaoHiemDTO gbh_dto = CreateGoiBaoHiemDTO(gbhDomain);
            return Ok(gbh_dto);
        }

        private static GoiBaoHiemDTO CreateGoiBaoHiemDTO(GoiBaoHiem? gbh)
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
