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
            var lsct = VHIDbContext.LichSuChiTra.ToList();
            return Ok(lsct);
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
