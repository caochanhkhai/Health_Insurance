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

       /* [HttpGet]
        [Route("{idGoiBaoHiem}")]
        public IActionResult GetChiTietChinhSachByIdGoiBaoHiem(int idGoiBaoHiem)
        {
            string query = $"SELECT * FROM ChiTietChinhSach WHERE ID_GoiBaoHiem = {idGoiBaoHiem}";
            var cccsList = VHIDbContext.ChiTietChinhSach.FromSqlRaw(query).ToList();

            if (cccsList.Count == 0)
            {
                return NotFound();
            }

            Console.WriteLine("Id goi bao hiem: "+cccsList[0].ID_GoiBaoHiem);

            var cccsDTOList = cccsList.Select(cccs => new ChiTietChinhSachDTO
            {
                ID = cccs.ID,
                ID_GoiBaoHiem = cccs.ID_GoiBaoHiem,
                ID_ChinhSach = cccs.ID_ChinhSach,
                STT = cccs.STT,
                HanMucChiTra = cccs.HanMucChiTra,
                DieuKienApDung = cccs.DieuKienApDung,
                Mota = cccs.Mota
            }).ToList();

            return Ok(cccsDTOList);
        }*/
    }
}
