using API.Data;
using API.Domain;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenhVienController : ControllerBase
    {
        private readonly VHIDbContext VHIDbContext;

        public BenhVienController(VHIDbContext VHIDbContext)
        {
            this.VHIDbContext = VHIDbContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var dsbvDomain = VHIDbContext.BenhVien.ToList();
            if (dsbvDomain == null || dsbvDomain.Count == 0)
            {
                return BadRequest("Không tồn tại Bệnh Viện nào.");
            }
            List<BenhVienDTO> dsbvDTO = new List<BenhVienDTO>();
            foreach (var bv in dsbvDomain)
            {
                BenhVienDTO bv_dto = CreateBenhVienDTO(bv);
                dsbvDTO.Add(bv_dto);
            }

            return Ok(dsbvDTO);
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int id)
        {
            var bv = VHIDbContext.BenhVien.FirstOrDefault(x => x.ID_BenhVien == id);
            if (bv == null)
            {
                return NotFound("Không tìm thấy bệnh viện.");
            }
            BenhVienDTO bv_dto = CreateBenhVienDTO(bv);

            return Ok(bv_dto);
        }

        [HttpPost]
        [Route("ThemBenhVien")]
        public IActionResult Thembv([FromBody] AddBenhVienDTO dto)
        {
            var dsbv = VHIDbContext.BenhVien.ToList();
            foreach (var bv in dsbv)
            {
                if (dto.Email == bv.Email)
                {
                    return BadRequest("Email đã tồn tại!");
                }
            }
            foreach (var bv in dsbv)
            {
                string sdt = bv.SDT.TrimEnd();
                if (dto.SDT == sdt)
                {
                    return BadRequest("Số Điện Thoại đã tồn tại!");
                }
            }

            BenhVien bv_Domain = new BenhVien()
            {
                TenBenhVien = dto.TenBenhVien,
                DiaChi = dto.DiaChi,
                Email = dto.Email,
                SDT = dto.SDT
            };

            VHIDbContext.BenhVien.Add(bv_Domain);
            VHIDbContext.SaveChanges();

            BenhVienDTO bvDTO = CreateBenhVienDTO(bv_Domain);

            return Ok(bvDTO);
        }

        private static BenhVienDTO CreateBenhVienDTO(BenhVien? bv)
        {
            return new BenhVienDTO()
            {
                ID_BenhVien = bv.ID_BenhVien,
                TenBenhVien = bv.TenBenhVien,
                DiaChi = bv.DiaChi,
                SDT = bv.SDT,
                Email = bv.Email
            };
        }
    }
}
