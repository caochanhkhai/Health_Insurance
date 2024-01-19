/*using API.Data;
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


        [HttpPut("ChinhSuaBaoHiem/{id}")]
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
            ctcsDomain.STT = dto.STT;
            ctcsDomain.HanMucChiTra = dto.HanMucChiTra;
            ctcsDomain.DieuKienApDung= dto.DieuKienApDung;
            ctcsDomain.Mota= dto.Mota;
            VHIDbContext.SaveChanges();

            ChiTietChinhSachDTO ctcsDTO = CreateCTCSDTO(ctcsDomain);

            return Ok(ctcsDTO);
        }

        private static ChiTietChinhSachDTO CreateCTCSDTO(ChiTietChinhSach? ctcs)
        {
            var ctcs_dto = new ChiTietChinhSachDTO();
            ctcs_dto.ID = ctcs.ID;
            ctcs_dto.ID_ChinhSach = ctcs.ChinhSachID_ChinhSach;
            ctcs_dto.ID_GoiBaoHiem = ctcs.GoiBaoHiemID_GoiBaoHiem;
            ctcs_dto.STT = ctcs.STT;
            ctcs_dto.HanMucChiTra = ctcs.HanMucChiTra;
            ctcs_dto.DieuKienApDung = ctcs.DieuKienApDung;
            ctcs_dto.Mota = ctcs.Mota;
            return ctcs_dto;
        }
    }
}
*/