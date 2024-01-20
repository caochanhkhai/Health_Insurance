using API.Data;
using API.Domain;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChiTietChinhSachController : ControllerBase
    {
        private readonly VHIDbContext VHIDbContext;

        public ChiTietChinhSachController(VHIDbContext VHIDbContext)
        {
            this.VHIDbContext = VHIDbContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var dsctcs = VHIDbContext.ChiTietChinhSach.ToList();

            if(dsctcs == null || dsctcs.Count == 0)
            {
                return BadRequest("Không tồn tại Chi Tiết Chính Sách nào.");
            }

            List<ChiTietChinhSachDTO> dsctcsDTO = new List<ChiTietChinhSachDTO>();
            foreach (var ctcs in dsctcs)
            {
                ChiTietChinhSachDTO ctcs_dto = CreateChiTietChinhSachDTO(ctcs);
                dsctcsDTO.Add(ctcs_dto);
            }
            return Ok(dsctcsDTO);
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int id)
        {
            var ctcs = VHIDbContext.ChiTietChinhSach.FirstOrDefault(x => x.ID == id);
            if (ctcs == null)
            {
                return NotFound("Không tồn tại Chi tiết chính sách có ID tương ứng");
            }
            ChiTietChinhSachDTO ctcs_dto = CreateChiTietChinhSachDTO(ctcs);
            return Ok(ctcs_dto);
        }

        [HttpGet]
        [Route("GetByIdGoiBaoHiem")]
        public IActionResult GetChiTietChinhSachByIdGoiBaoHiem(int idGoiBaoHiem)
        {
            var ctcsList = VHIDbContext.ChiTietChinhSach.Where(ctcs => ctcs.GoiBaoHiemID_GoiBaoHiem == idGoiBaoHiem).ToList();

            if (ctcsList.Count == 0)
            {
                return NotFound("Không tìm thấy chi tiết chính sách.");
            }

            var ctcsDTOList = ctcsList.Select(ctcs => new ChiTietChinhSachDTO
            {
                ID = ctcs.ID,
                ID_GoiBaoHiem = ctcs.GoiBaoHiemID_GoiBaoHiem,
                ID_ChinhSach = ctcs.ChinhSachID_ChinhSach,
                NgayBatDau = ctcs.NgayBatDau,
                NgayKetThuc = ctcs.NgayKetThuc
            }).ToList();

            return Ok(ctcsDTOList);
        }

        [HttpGet]
        [Route("GetByIdChinhSach")]
        public IActionResult GetChiTietChinhSachByIdChinhSach(int idChinhSach)
        {
            var ctcsList = VHIDbContext.ChiTietChinhSach.Where(ctcs => ctcs.ChinhSachID_ChinhSach == idChinhSach).ToList();

            if (ctcsList.Count == 0)
            {
                return NotFound("Không tìm thấy chi tiết chính sách.");
            }

            var ctcsDTOList = ctcsList.Select(ctcs => new ChiTietChinhSachDTO
            {
                ID = ctcs.ID,
                ID_ChinhSach = ctcs.ChinhSachID_ChinhSach,
                ID_GoiBaoHiem = ctcs.ChinhSachID_ChinhSach,
                NgayBatDau = ctcs.NgayBatDau,
                NgayKetThuc = ctcs.NgayKetThuc
            }).ToList();

            return Ok(ctcsDTOList);
        }

        /*[HttpPut("ChinhSuaBaoHiem/{id}")]
        public IActionResult ChinhSuaBaoHiem(int idgbh, int idcs, [FromBody] ChiTietChinhSachDTO dto)
        {
            var ctcsDomain = VHIDbContext.ChiTietChinhSach.FirstOrDefault(x => x.ID == idcs);
            var csDomain = VHIDbContext.ChinhSach.FirstOrDefault(x => x.ID_ChinhSach == idcs);
            var bhDomain = VHIDbContext.GoiBaoHiem.FirstOrDefault(x => x.ID_GoiBaoHiem == idgbh);
            if (bhDomain == null || bhDomain == null)
            {
                return NotFound("Không tìm thấy.");
            }

            // Cập nhật và lưu vào cơ sở dữ liệu
            ctcsDomain.HanMucChiTra = dto.HanMucChiTra;
            ctcsDomain.DieuKienApDung = dto.DieuKienApDung;
            ctcsDomain.Mota = dto.Mota;
            VHIDbContext.SaveChanges();

            ChiTietChinhSachDTO ctcsDTO = CreateCTCSDTO(ctcsDomain);

            return Ok(ctcsDTO);
        }*/


        private static ChiTietChinhSachDTO CreateChiTietChinhSachDTO(ChiTietChinhSach? ctcs)
        {
            return new ChiTietChinhSachDTO()
            {
                ID = ctcs.ID,
                ID_GoiBaoHiem = ctcs.GoiBaoHiemID_GoiBaoHiem,
                ID_ChinhSach = ctcs.ChinhSachID_ChinhSach,
                NgayBatDau = ctcs.NgayBatDau,
                NgayKetThuc = ctcs.NgayKetThuc
            };
        }
    }
}
