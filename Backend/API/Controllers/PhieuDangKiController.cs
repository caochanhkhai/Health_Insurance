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
        [Route("userId:int")]
        public IActionResult GetAllByUserId(int userId)
        {
            var phieudk = VHIDbContext.PhieuDangKi.Where(x => x.KhachHangID_KhachHang == userId).ToList();

            if (phieudk == null)
            {
                return NotFound();
            }

            var phieudk_dto = phieudk.Select(phieudk => new PhieuDangKiDTO
                { 
            ID_PhieuDangKi = phieudk.ID_PhieuDangKi,
            TinhTrangDuyet = phieudk.TinhTrangDuyet,
            DiaDiemKiKet = phieudk.DiaDiemKiKet,
            ThoiGianKiKet = phieudk.ThoiGianKiKet,
            ToKhaiSucKhoe = phieudk.ToKhaiSucKhoe,
            ID_KhachHang = phieudk.KhachHangID_KhachHang,
            ID_GoiBaoHiem = phieudk.GoiBaoHiemID_GoiBaoHiem,
            ID_NhanVien = phieudk.NhanVienID_NhanVien
        }).ToList();
            return Ok(phieudk_dto);
        }

        [HttpGet]
        [Route("GetPhieuDangKyById/{id}")]
        public IActionResult GetPhieuDangKyByID(int id)
        {
            var phieudk = VHIDbContext.PhieuDangKi.Where(x => x.ID_PhieuDangKi == id).ToList();

            if (phieudk == null)
            {
                return NotFound("Không tìm thấy phiếu đăng ký");
            }

            var phieudk_dto = phieudk.Select(phieudk => new PhieuDangKiDTO
            {
                ID_PhieuDangKi = phieudk.ID_PhieuDangKi,
                TinhTrangDuyet = phieudk.TinhTrangDuyet,
                DiaDiemKiKet = phieudk.DiaDiemKiKet,
                ThoiGianKiKet = phieudk.ThoiGianKiKet,
                ToKhaiSucKhoe = phieudk.ToKhaiSucKhoe,
                ID_KhachHang = phieudk.KhachHangID_KhachHang,
                ID_GoiBaoHiem = phieudk.GoiBaoHiemID_GoiBaoHiem,
                ID_NhanVien = phieudk.NhanVienID_NhanVien
            }).ToList();
            return Ok(phieudk_dto);
        }


        [HttpPost]
        [Route("DangKyBaoHiem")]
        public IActionResult CreatePhieuDangKi([FromBody] PhieuDangKiDTO dto, int idkh, int idgbh)
        {
           
            var kh = VHIDbContext.KhachHang.FirstOrDefault(b => b.ID_KhachHang == idkh);
            var gbh = VHIDbContext.GoiBaoHiem.FirstOrDefault(b => b.ID_GoiBaoHiem == idgbh);
            var nv = VHIDbContext.NhanVien.FirstOrDefault(b => b.ID_NhanVien == dto.ID_NhanVien);
            if (kh == null || gbh == null)
            {
                return BadRequest("Khách hàng hoặc gói bảo hiểm không tồn tại.");
            }
            PhieuDangKi PDKdomain = new PhieuDangKi
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

            var pdk_dto = new PhieuDangKiDTO
            {
                ID_PhieuDangKi = PDKdomain.ID_PhieuDangKi,
                TinhTrangDuyet = PDKdomain.TinhTrangDuyet,
                DiaDiemKiKet = PDKdomain.DiaDiemKiKet,
                ThoiGianKiKet = PDKdomain.ThoiGianKiKet,
                ToKhaiSucKhoe = PDKdomain.ToKhaiSucKhoe,
                ID_GoiBaoHiem = PDKdomain.GoiBaoHiemID_GoiBaoHiem,
                ID_KhachHang = PDKdomain.KhachHangID_KhachHang,
                ID_NhanVien = PDKdomain.NhanVienID_NhanVien
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

        [HttpPut("ThemNhanVienChoPhieuDangKi")]
        public IActionResult ThemNhanVienChoPhieuDangKi(int idpdk, int idnv)
        {
            var phieuDangKi = VHIDbContext.PhieuDangKi.FirstOrDefault(x => x.ID_PhieuDangKi == idpdk);
            var nhanVien = VHIDbContext.NhanVien.FirstOrDefault(x => x.ID_NhanVien == idnv);

            if (phieuDangKi == null)
            {
                return NotFound("Không tìm thấy phiếu đăng ký.");
            }
            if (nhanVien == null)
            {
                return NotFound("Không tìm thấy nhân viên.");
            }

            // Cập nhật nhân viên và lưu vào cơ sở dữ liệu
            phieuDangKi.NhanVien = nhanVien;
            VHIDbContext.SaveChanges();
            PhieuDangKiDTO phieudk_dto = CreatePhieuDKDTO(phieuDangKi);

            return Ok(phieudk_dto);
        }

        [HttpPut("CungCapToKhai(id: int, toKhai: string)")]
        public IActionResult CungCapToKhai([FromBody] CungCapToKhaiRequestDTO dto)
        {
            var pdkDomain = VHIDbContext.PhieuDangKi.FirstOrDefault(x => x.ID_PhieuDangKi == dto.id);
            if (pdkDomain == null)
            {
                return NotFound("Không tìm thấy phiếu đăng ký.");
            }

            // Cập nhật tờ khai và lưu vào cơ sở dữ liệu
            pdkDomain.ToKhaiSucKhoe = dto.toKhai;
            VHIDbContext.SaveChanges();

            PhieuDangKiDTO pdkDTO = CreatePhieuDKDTO(pdkDomain);

            return Ok(pdkDTO);
        }

        private static PhieuDangKiDTO CreatePhieuDKDTO(PhieuDangKi? phieuDangKiDomain)
        {
            return new PhieuDangKiDTO
            {
                ID_PhieuDangKi = phieuDangKiDomain.ID_PhieuDangKi,
                TinhTrangDuyet = phieuDangKiDomain.TinhTrangDuyet,
                DiaDiemKiKet = phieuDangKiDomain.DiaDiemKiKet,
                ThoiGianKiKet = phieuDangKiDomain.ThoiGianKiKet,
                ToKhaiSucKhoe = phieuDangKiDomain.ToKhaiSucKhoe,
                ID_KhachHang = phieuDangKiDomain.KhachHangID_KhachHang,
                ID_GoiBaoHiem = phieuDangKiDomain.GoiBaoHiemID_GoiBaoHiem,
                ID_NhanVien = phieuDangKiDomain.NhanVienID_NhanVien
            };
        }
    }
}

