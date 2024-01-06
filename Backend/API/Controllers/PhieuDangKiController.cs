using API.Data;
using API.Domain;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

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

        [HttpPut("XetDuyetPhieuDangKy/{id}")]
        public IActionResult XetDuyetPhieuDangKy(int id, [FromBody] PhieuDangKiDTO phieuDangKiDto)
        {
            var phieuDangKy = VHIDbContext.PhieuDangKi.SingleOrDefault(x => x.ID_PhieuDangKi == id);

            if (phieuDangKy == null)
            {
                return NotFound("Không tìm thấy phiếu đăng ký.");
            }

            // Cập nhật tình trạng duyệt và lưu vào cơ sở dữ liệu
            phieuDangKy.TinhTrangDuyet = phieuDangKiDto.TinhTrangDuyet;
            VHIDbContext.SaveChanges();
            var phieudk_dto = new PhieuDangKiDTO
            {
                ID_PhieuDangKi = phieuDangKy.ID_PhieuDangKi,
                TinhTrangDuyet = phieuDangKy.TinhTrangDuyet,
                DiaDiemKiKet = phieuDangKy.DiaDiemKiKet,
                ThoiGianKiKet = phieuDangKy.ThoiGianKiKet,
                ToKhaiSucKhoe = phieuDangKy.ToKhaiSucKhoe,
                ID_KhachHang = phieuDangKy.KhachHangID_KhachHang,
                ID_GoiBaoHiem = phieuDangKy.GoiBaoHiemID_GoiBaoHiem,
                ID_NhanVien = phieuDangKy.NhanVienID_NhanVien
            };

            return Ok(phieudk_dto);
        }

        [HttpPut("CungCapToKhai/{id}")]
        public IActionResult CungCapToKhai(int idpdk, int id, string toKhai)
        {
            //var khDomain = VHIDbContext.KhachHang.FirstOrDefault(x => x.ID_KhachHang == id);
            var pdkDomain = VHIDbContext.PhieuDangKi.FirstOrDefault(x => x.ID_PhieuDangKi == idpdk);
            if (pdkDomain == null)
            {
                return NotFound("Không tìm thấy phiếu đăng ký.");
            }

            // Cập nhật tờ khai và lưu vào cơ sở dữ liệu
            pdkDomain.ToKhaiSucKhoe = toKhai;
            VHIDbContext.SaveChanges();

            PhieuDangKiDTO pdkDTO = CreatePDKDTO(pdkDomain);

            return Ok(pdkDTO);
        }

        private static PhieuDangKiDTO CreatePDKDTO(PhieuDangKi? pdk)
        {
            var pdk_dto = new PhieuDangKiDTO();
            pdk_dto.ID_PhieuDangKi = pdk.ID_PhieuDangKi;
            pdk_dto.TinhTrangDuyet = pdk.TinhTrangDuyet;
            pdk_dto.DiaDiemKiKet = pdk.DiaDiemKiKet;
            pdk_dto.ThoiGianKiKet = pdk.ThoiGianKiKet;
            pdk_dto.ToKhaiSucKhoe = pdk.ToKhaiSucKhoe ;
            pdk_dto.ID_KhachHang = pdk.KhachHangID_KhachHang;
            pdk_dto.ID_NhanVien = pdk.NhanVienID_NhanVien;
            pdk_dto.ID_GoiBaoHiem = pdk.GoiBaoHiemID_GoiBaoHiem;
            return pdk_dto;
        }
    }
}
