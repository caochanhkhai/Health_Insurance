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

            if (dscsDomain == null || dscsDomain.Count == 0)
            {
                return BadRequest("Không tồn tại Chính Sách nào.");
            }

            foreach (var cs in dscsDomain)
            {
                ChinhSachDTO cs_dto = CreateChinhSachDTO(cs);
                dscsDTO.Add(cs_dto);
            }

            return Ok(dscsDTO);
        }

        [HttpGet]
        [Route("GetById")]
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

        [HttpPost]
        [Route("ThemChinhSach")]
        public IActionResult ThemCS([FromBody] AddChinhSachDTO dto)
        {
            ChinhSach cs_Domain = new ChinhSach()
            {
                TenChinhSach = dto.TenChinhSach,
                ThoiGianPhatHanh = dto.ThoiGianPhatHanh,
                HanMucChiTra = dto.HanMucChiTra,
                DieuKienApDung = dto.DieuKienApDung,
                Mota = dto.Mota
            };

            VHIDbContext.ChinhSach.Add(cs_Domain);
            VHIDbContext.SaveChanges();

            ChinhSachDTO csDTO = CreateChinhSachDTO(cs_Domain);

            return Ok(csDTO);
        }


        private static ChinhSachDTO CreateChinhSachDTO(ChinhSach? cs)
        {
            return new ChinhSachDTO()
            {
                ID_ChinhSach = cs.ID_ChinhSach,
                TenChinhSach = cs.TenChinhSach,
                ThoiGianPhatHanh = cs.ThoiGianPhatHanh,
                HanMucChiTra = cs.HanMucChiTra,
                DieuKienApDung = cs.DieuKienApDung,
                Mota = cs.Mota
            };
        }
    }
}