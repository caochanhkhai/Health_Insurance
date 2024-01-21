using API.Data;
using API.Domain;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenhVienGoiBaoHiemController : ControllerBase
    {
        private readonly VHIDbContext VHIDbContext;

        public BenhVienGoiBaoHiemController(VHIDbContext VHIDbContext)
        {
            this.VHIDbContext = VHIDbContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var dsbv_gbhDomain = VHIDbContext.BenhVien_GoiBaoHiem.ToList();
            List<BenhVien_GoiBaoHiemDTO> dsbv_gbhDTO = new List<BenhVien_GoiBaoHiemDTO>();
            if (dsbv_gbhDomain == null || dsbv_gbhDomain.Count == 0)
            {
                return BadRequest("Không tồn tại.");
            }
            foreach (var bv_gbh in dsbv_gbhDomain)
            {
                BenhVien_GoiBaoHiemDTO bv_gbh_dto = CreateBenhVien_GoiBaoHiemDTO(bv_gbh);
                dsbv_gbhDTO.Add(bv_gbh_dto);
            }

            return Ok(dsbv_gbhDTO);
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int id)
        {
            var bv_gbh = VHIDbContext.BenhVien_GoiBaoHiem.FirstOrDefault(x => x.ID == id);
            if (bv_gbh == null)
            {
                return NotFound("Không tìm thấy thông tin tương ứng.");
            }
            BenhVien_GoiBaoHiemDTO bv_gbh_dto = CreateBenhVien_GoiBaoHiemDTO(bv_gbh);

            return Ok(bv_gbh_dto);
        }

        [HttpGet]
        [Route("GetByIdGoiBaoHiem")]
        public IActionResult GetByIdGoiBaoHiem(int idGoiBaoHiem)
        {
            var bv_gbhList = VHIDbContext.BenhVien_GoiBaoHiem.Where(bv_gbh => bv_gbh.GoiBaoHiemID_GoiBaoHiem == idGoiBaoHiem).ToList();

            if (bv_gbhList.Count == 0)
            {
                return NotFound("Không tìm thấy thông tin tương ứng.");
            }

            var bv_gbhDTOList = bv_gbhList.Select(bv_gbh => new BenhVien_GoiBaoHiemDTO
            {
                ID = bv_gbh.ID,
                ID_GoiBaoHiem = bv_gbh.GoiBaoHiemID_GoiBaoHiem,
                ID_BenhVien = bv_gbh.BenhVienID_BenhVien
            }).ToList();

            return Ok(bv_gbhDTOList);
        }

        [HttpGet]
        [Route("GetByIdBenhVien")]
        public IActionResult GetByIdBenhVien(int idBenhVien)
        {
            var bv_gbhList = VHIDbContext.BenhVien_GoiBaoHiem.Where(bv_gbh => bv_gbh.BenhVienID_BenhVien == idBenhVien).ToList();

            if (bv_gbhList.Count == 0)
            {
                return NotFound("Không tìm thấy thông tin tương ứng.");
            }

            var bv_gbhDTOList = bv_gbhList.Select(bv_gbh => new BenhVien_GoiBaoHiemDTO
            {
                ID = bv_gbh.ID,
                ID_GoiBaoHiem = bv_gbh.GoiBaoHiemID_GoiBaoHiem,
                ID_BenhVien = bv_gbh.BenhVienID_BenhVien
            }).ToList();

            return Ok(bv_gbhDTOList);
        }

        [HttpPost]
        [Route("ThemBenhVienChoGoiBaoHiem")]
        public IActionResult ThemBenhVienChoGoiBaoHiem([FromBody] AddBenhVienGoiBaoHiemDTO dto)
        {
            var gbhDomain = VHIDbContext.GoiBaoHiem.FirstOrDefault(x => x.ID_GoiBaoHiem == dto.ID_GoiBaoHiem);
            var bvDomain = VHIDbContext.BenhVien.FirstOrDefault(x => x.ID_BenhVien == dto.ID_BenhVien);

            if (gbhDomain == null)
            {
                return NotFound("Không tồn tại Gói bảo hiểm.");
            }

            if (bvDomain == null)
            {
                return NotFound("Không tồn tại Bệnh viện.");
            }

            BenhVien_GoiBaoHiem bv_gbh = new BenhVien_GoiBaoHiem()
            {
                BenhVien = bvDomain,
                GoiBaoHiem = gbhDomain
            };
            VHIDbContext.BenhVien_GoiBaoHiem.Add(bv_gbh);
            VHIDbContext.SaveChanges();

            BenhVien_GoiBaoHiemDTO bv_gbhDTO = CreateBenhVien_GoiBaoHiemDTO(bv_gbh);

            return Ok(bv_gbhDTO);
        }
        private static BenhVien_GoiBaoHiemDTO CreateBenhVien_GoiBaoHiemDTO(BenhVien_GoiBaoHiem? bv_gbh)
        {
            return new BenhVien_GoiBaoHiemDTO()
            {
                ID = bv_gbh.ID,
                ID_GoiBaoHiem = bv_gbh.GoiBaoHiemID_GoiBaoHiem,
                ID_BenhVien = bv_gbh.BenhVienID_BenhVien
            };
        }
    }
}
