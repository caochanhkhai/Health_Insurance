using API.Data;
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
        [Route("GetAllChitietchinhsach/idGoiBaoHiem")]
        public IActionResult GetChiTietChinhSachByIdGoiBaoHiem(int idGoiBaoHiem)
        {
            string query = $"SELECT * FROM ChiTietChinhSach WHERE GoiBaoHiemID_GoiBaoHiem = {idGoiBaoHiem}";
            var cccsList = VHIDbContext.ChiTietChinhSach.FromSqlRaw(query).ToList();

            if (cccsList.Count == 0)
            {
                return NotFound("Không tìm thấy chi tiết chính sách.");
            }

            var cccsDTOList = cccsList.Select(cccs => new ChiTietChinhSachDTO
            {
                ID = cccs.ID,
                ID_GoiBaoHiem = cccs.GoiBaoHiemID_GoiBaoHiem,
                ID_ChinhSach = cccs.ChinhSachID_ChinhSach,
                STT = cccs.STT,
                HanMucChiTra = cccs.HanMucChiTra,
                DieuKienApDung = cccs.DieuKienApDung,
                Mota = cccs.Mota
            }).ToList();

            return Ok(cccsDTOList);
        }
    }
}
