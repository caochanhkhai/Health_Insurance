using API.Data;
using API.Domain;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChinhSachController : ControllerBase
    {
        private readonly VHIDbContext VHIDbContext;

        public ChinhSachController(VHIDbContext VHIDbContext)
        {
            this.VHIDbContext = VHIDbContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var dscsDomain = VHIDbContext.ChinhSach.ToList();
            List<ChinhSachDTO> dscsDTO = new List<ChinhSachDTO>();
            foreach (var cs in dscsDomain)
            {
                ChinhSachDTO cs_dto = CreateChinhSachDTO(cs);
                dscsDTO.Add(cs_dto);
            }

            return Ok(dscsDTO);
        }

        [HttpGet]
        [Route("id")]
        public IActionResult GetById(int id)
        {
            var cs = VHIDbContext.ChinhSach.FirstOrDefault(x => x.ID_ChinhSach == id);
            if (cs == null)
            {
                return NotFound("Không tìm thấy chính sách.");
            }
            ChinhSachDTO cs_dto = CreateChinhSachDTO(cs);

            return Ok(cs_dto);
        }

        private static ChinhSachDTO CreateChinhSachDTO(ChinhSach? cs)
        {
            return new ChinhSachDTO()
            {
                ID_ChinhSach = cs.ID_ChinhSach,
                STT = cs.STT,
                TenChinhSach = cs.TenChinhSach,
                ThoiGianPhatHanh = cs.ThoiGianPhatHanh
            };
        }
    }
}
