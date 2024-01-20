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

        private static LichSuChiTraDTO CreateLichSuChiTraDTO(LichSuChiTra? lsct)
        {
            return new LichSuChiTraDTO()
            {
                ID = lsct.ID,
                ID_YeuCauChiTra = lsct.YeuCauChiTraID_YeuCauChiTra,
                TenBenhVien = lsct.TenBenhVien,
                ThoiGianChiTra = lsct.ThoiGianChiTra,
                SoTienChiTra = lsct.SoTienChiTra
            };
        }

        [HttpPost]
        [Route("ThemLichSuChiTra{idycct:int},{soTienChiTra:decimal}")]
        public IActionResult CreateLichSuChiTra(int idycct, [FromBody] DateTimeRequestModel model, decimal soTienChiTra)
        {
            var ycct = VHIDbContext.YeuCauChiTra.FirstOrDefault(x => x.ID_YeuCauChiTra == idycct);
            if (ycct == null)
            {
                return NotFound("Không tìm thấy yêu cầu chi trả.");
            }
            if (ycct.TinhTrangDuyet != "Đã Duyệt")
            {
                return BadRequest("Tình trạng duyệt của Yêu cầu chi trả không hợp lệ.");
            }
            var lsct = VHIDbContext.LichSuChiTra.FirstOrDefault(x => x.YeuCauChiTraID_YeuCauChiTra == idycct);
            if (lsct != null)
            {
                return BadRequest("Đã tồn tại lịch sử chi trả cho yêu cầu chi trả này.");
            }
            LichSuChiTra lsctDomain = new LichSuChiTra()
            {
                YeuCauChiTra = ycct,
                TenBenhVien = ycct.NoiDieuTri,
                ThoiGianChiTra = model.ThoiGianChiTra,
                SoTienChiTra = soTienChiTra
            };

            VHIDbContext.LichSuChiTra.Add(lsctDomain);
            VHIDbContext.SaveChanges();

            LichSuChiTraDTO lsctDTO = CreateLichSuChiTraDomain(lsctDomain);

            return Ok(lsctDTO);
        }

        public class DateTimeRequestModel
        {
            public DateTime ThoiGianChiTra { get; set; }
            
        }

        private static LichSuChiTraDTO CreateLichSuChiTraDomain(LichSuChiTra lsctDomain)
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
