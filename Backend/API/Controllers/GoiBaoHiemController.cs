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
            if (dsgbhDomain == null || dsgbhDomain.Count == 0)
            {
                return BadRequest("Không tồn tại Gói Bảo Hiểm nào.");
            }
            foreach (var gbh in dsgbhDomain)
            {
                GoiBaoHiemDTO gbh_dto = CreateGoiBaoHiemDTO(gbh);
                dsgbhDTO.Add(gbh_dto);
            }

            return Ok(dsgbhDTO);
        }

        [HttpGet]
        [Route("GetAllGoiBaoHiemDangPhatHanh")]
        public IActionResult GetAllGoiBaoHiemDangPhatHanh()
        {
            var dsgbhDomain = VHIDbContext.GoiBaoHiem.Where(x=>x.TinhTrang == "Đang Phát Hành").ToList();
            List<GoiBaoHiemDTO> dsgbhDTO = new List<GoiBaoHiemDTO>();
            if (dsgbhDomain == null || dsgbhDomain.Count == 0)
            {
                return BadRequest("Không tồn tại Gói Bảo Hiểm nào.");
            }
            foreach (var gbh in dsgbhDomain)
            {
                GoiBaoHiemDTO gbh_dto = CreateGoiBaoHiemDTO(gbh);
                dsgbhDTO.Add(gbh_dto);
            }

            return Ok(dsgbhDTO);
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int id)
        {
            var gbh = VHIDbContext.GoiBaoHiem.FirstOrDefault(x => x.ID_GoiBaoHiem == id);
            if (gbh == null)
            {
                return NotFound("Không tìm thấy gói bảo hiểm.");
            }
            GoiBaoHiemDTO gbh_dto = CreateGoiBaoHiemDTO(gbh);

            return Ok(gbh_dto);
        }

        [HttpPost]
        [Route("UpdateHinhAnh")]
        public IActionResult UpdateGoiBaoHiem_HinhAnh(int idgbh, string LinkHinhAnh)
        {
            var gbhDomain = VHIDbContext.GoiBaoHiem.FirstOrDefault(x => x.ID_GoiBaoHiem == idgbh);
            if (gbhDomain == null)
            {
                return NotFound("Không tồn tại gói bảo hiểm này.");
            }

            gbhDomain.HinhAnh = LinkHinhAnh;

            VHIDbContext.SaveChanges();
            GoiBaoHiemDTO gbh_dto = CreateGoiBaoHiemDTO(gbhDomain);
            return Ok(gbh_dto);
        }

        [HttpPost]
        [Route("ThemGoiBaoHiem")]
        public IActionResult ThemGoiBaoHiem([FromBody] AddGoiBaoHiemDTO dto)
        {
            if (dto.GiaTien <= 0)
            {
                return BadRequest("Giá tiền Gói bảo hiểm không hợp lệ: " + dto.GiaTien);
            }
            if (dto.ThoiHan <= 0)
            {
                return BadRequest("Thời hạn Gói bảo hiểm không hợp lệ: " + dto.ThoiHan);
            }
            GoiBaoHiem gbh_Domain = new GoiBaoHiem()
            {
                TenBaoHiem = dto.TenBaoHiem,
                TenGoi = dto.TenGoi,
                GiaTien = dto.GiaTien,
                ThoiHan = dto.ThoiHan,
                MoTa = dto.MoTa,
                NgayPhatHanh = dto.NgayPhatHanh,
                TinhTrang = "Đang Phát Hành",
                HinhAnh = dto.HinhAnh
            };

            VHIDbContext.GoiBaoHiem.Add(gbh_Domain);
            VHIDbContext.SaveChanges();

            GoiBaoHiemDTO gbhDTO = CreateGBHDTO(gbh_Domain);

            return Ok(gbhDTO);
        }

        [HttpPost("CapNhatGoiBaoHiem")]
        public IActionResult CapNhatGBH(int id, [FromBody] UpdateGoiBaoHiemDTO dto)
        {
            var gbhDomain = VHIDbContext.GoiBaoHiem.FirstOrDefault(x => x.ID_GoiBaoHiem == id);

            if (gbhDomain == null)
            {
                return NotFound("Không tìm thấy gói bảo hiểm.");
            }
            if(dto.TinhTrang != "Đang Phát Hành" && dto.TinhTrang != "Ngưng Phát Hành")
            {
                return BadRequest("Tình trạng Gói bảo hiểm không hợp lệ: " + dto.TinhTrang);
            }
            if (dto.GiaTien <= 0)
            {
                return BadRequest("Giá tiền Gói bảo hiểm không hợp lệ: " + dto.GiaTien);
            }
            if (dto.ThoiHan <= 0)
            {
                return BadRequest("Thời hạn Gói bảo hiểm không hợp lệ: " + dto.ThoiHan);
            }

            gbhDomain.TenBaoHiem = dto.TenBaoHiem;
            gbhDomain.TenGoi = dto.TenGoi;
            gbhDomain.GiaTien = dto.GiaTien;
            gbhDomain.ThoiHan = dto.ThoiHan;
            gbhDomain.MoTa = dto.MoTa;
            gbhDomain.NgayPhatHanh = dto.NgayPhatHanh;
            gbhDomain.TinhTrang = dto.TinhTrang;
            gbhDomain.HinhAnh = dto.HinhAnh;
            VHIDbContext.SaveChanges();

            GoiBaoHiemDTO gbhDTO = CreateGBHDTO(gbhDomain);

            return Ok(gbhDTO);
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
