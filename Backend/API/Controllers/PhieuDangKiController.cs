using API.Data;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PhieuDangKiController : ControllerBase
    {
        private readonly VHIDbContext VHIDbContext;

        public PhieuDangKiController(VHIDbContext VHIDbContext)
        {
            this.VHIDbContext = VHIDbContext;
        }

        [HttpGet]
        [Route("id:int")]
        public IActionResult GetById(int id)
        {
            var phieudk = VHIDbContext.PhieuDangKi.FirstOrDefault(x => x.ID_PhieuDangKi == id);
            if (phieudk == null)
            {
                return NotFound();
            }
            var phieudk_dto = new PhieuDangKiDTO();
            phieudk_dto.ID_PhieuDangKi = phieudk.ID_PhieuDangKi;
            phieudk_dto.TinhTrangDuyet = phieudk.TinhTrangDuyet;
            phieudk_dto.DiaDiemKiKet = phieudk.DiaDiemKiKet;
            phieudk_dto.ThoiGianKiKet = phieudk.ThoiGianKiKet;
            phieudk_dto.ToKhaiSucKhoe = phieudk.ToKhaiSucKhoe;
            phieudk_dto.ID_KhachHang = phieudk.ID_KhachHang;
            phieudk_dto.ID_GoiBaoHiem = phieudk.ID_GoiBaoHiem;
            return Ok(phieudk_dto);
        }
    }
}
