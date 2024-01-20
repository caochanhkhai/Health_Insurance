using API.Data;
using API.Domain;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichSuChiTraController : ControllerBase
    {
        private readonly VHIDbContext VHIDbContext;

        public LichSuChiTraController(VHIDbContext VHIDbContext)
        {
            this.VHIDbContext = VHIDbContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var lsctDomain = VHIDbContext.LichSuChiTra.ToList();
            if (lsctDomain == null || lsctDomain.Count() == 0)
            {
                return NotFound("Không tồn tại Lịch sử chi trả nào.");
            }

            List<LichSuChiTraDTO> dslsctDTO = new List<LichSuChiTraDTO>();
            foreach (var lsct in lsctDomain)
            {
                LichSuChiTraDTO lsct_dto = CreateLichSuChiTraDTO(lsct);
                dslsctDTO.Add(lsct_dto);
            }
            return Ok(dslsctDTO);
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int id)
        {
            var lsct = VHIDbContext.LichSuChiTra.FirstOrDefault(x => x.ID == id);

            if (lsct == null)
            {
                return NotFound("Không tìm thấy Lịch Sử Chi Trả.");
            }

            var lsct_dto = CreateLichSuChiTraDTO(lsct);

            return Ok(lsct_dto);
        }

        [HttpGet]
        [Route("GetByIdYeuCauChiTra")]
        public IActionResult GetByIdycct(int idycct)
        {
            var ycct = VHIDbContext.YeuCauChiTra.FirstOrDefault(x => x.ID_YeuCauChiTra == idycct);

            if (ycct == null)
            {
                return NotFound("Không tồn tại Yêu Cầu Chi Trả.");
            }

            var lsct = VHIDbContext.LichSuChiTra.Where(q => q.YeuCauChiTraID_YeuCauChiTra == idycct).ToList();

            if (lsct == null || lsct.Count() == 0)
            {
                return NotFound("Không tìm thấy Quản lý bảo hiểm tương ứng với Khách hàng.");
            }

            List<LichSuChiTraDTO> dslsctDTO = new List<LichSuChiTraDTO>();
            foreach (var bh in lsct)
            {
                LichSuChiTraDTO lsct_dto = CreateLichSuChiTraDTO(bh);
                dslsctDTO.Add(lsct_dto);
            }

            return Ok(dslsctDTO);
        }


        [HttpPost]
        [Route("ThemLichSuChiTra")]
        public IActionResult CreateLichSuChiTra( [FromBody] AddLichSuChiTraDTO dto)
        {
            var ycct = VHIDbContext.YeuCauChiTra.FirstOrDefault(x => x.ID_YeuCauChiTra == dto.ID_YeuCauChiTra);
            if (ycct == null)
            {
                return NotFound("Không tìm thấy yêu cầu chi trả.");
            }
            if (ycct.TinhTrangDuyet != "Đã Duyệt")
            {
                return BadRequest("Tình trạng duyệt của Yêu cầu chi trả không hợp lệ: "+ ycct.TinhTrangDuyet);
            }
            var lsct = VHIDbContext.LichSuChiTra.FirstOrDefault(x => x.YeuCauChiTraID_YeuCauChiTra == dto.ID_YeuCauChiTra);
            if (lsct != null)
            {
                return BadRequest("Đã tồn tại lịch sử chi trả cho yêu cầu chi trả này.");
            }
            LichSuChiTra lsctDomain = CreateLichSuChiTraDomain(dto, ycct);

            VHIDbContext.LichSuChiTra.Add(lsctDomain);
            VHIDbContext.SaveChanges();

            LichSuChiTraDTO lsctDTO = CreateLichSuChiTraDTO(lsctDomain);

            return Ok(lsctDTO);
        }

        private static LichSuChiTra CreateLichSuChiTraDomain(AddLichSuChiTraDTO dto, YeuCauChiTra? ycct)
        {
            return new LichSuChiTra()
            {
                YeuCauChiTra = ycct,
                TenBenhVien = ycct.NoiDieuTri,
                ThoiGianChiTra = dto.ThoiGianChiTra,
                SoTienChiTra = dto.SoTienChiTra
            };
        }

        private static LichSuChiTraDTO CreateLichSuChiTraDTO(LichSuChiTra lsctDomain)
        {
            return new LichSuChiTraDTO()
            {
                ID = lsctDomain.ID,
                ID_YeuCauChiTra = lsctDomain.YeuCauChiTraID_YeuCauChiTra,
                TenBenhVien = lsctDomain.TenBenhVien,
                ThoiGianChiTra = lsctDomain.ThoiGianChiTra,
                SoTienChiTra = lsctDomain.SoTienChiTra
            };
        }
    }
}
