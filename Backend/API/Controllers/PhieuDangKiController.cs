using API.Data;
using API.Domain;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [Route("id")]
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
            phieudk_dto.ID_KhachHang = phieudk.KhachHangID_KhachHang;
            phieudk_dto.ID_GoiBaoHiem = phieudk.GoiBaoHiemID_GoiBaoHiem;
            return Ok(phieudk_dto);
        }

        [HttpPost]
        [Route("ThemPhieuDangKiChoKH(idkh,idgbh)")]
        public IActionResult CreatePhieuDangKi([FromBody] PhieuDangKiDTO dto, int idkh, int idgbh)
        {
           
            var kh = VHIDbContext.KhachHang.FirstOrDefault(b => b.ID_KhachHang == idkh);
            var gbh = VHIDbContext.GoiBaoHiem.FirstOrDefault(b => b.ID_GoiBaoHiem == idgbh);
            PhieuDangKi PDKdomain = new PhieuDangKi()
            {
                TinhTrangDuyet = "Chưa Duyệt",
                DiaDiemKiKet = dto.DiaDiemKiKet,
                ThoiGianKiKet = dto.ThoiGianKiKet,
                ToKhaiSucKhoe = dto.ToKhaiSucKhoe,
                KhachHang = kh,
                GoiBaoHiem = gbh
            };

            VHIDbContext.PhieuDangKi.Add(PDKdomain);
            VHIDbContext.SaveChanges();

            var pdk_dto = new PhieuDangKiDTO()
            {
                ID_PhieuDangKi = PDKdomain.ID_PhieuDangKi,
                TinhTrangDuyet = PDKdomain.TinhTrangDuyet,
                DiaDiemKiKet = PDKdomain.DiaDiemKiKet,
                ThoiGianKiKet = PDKdomain.ThoiGianKiKet,
                ToKhaiSucKhoe = PDKdomain.ToKhaiSucKhoe,
                ID_GoiBaoHiem = PDKdomain.GoiBaoHiemID_GoiBaoHiem,
                ID_KhachHang = PDKdomain.KhachHangID_KhachHang
            };
            return Ok(pdk_dto);
        }
    }
}
