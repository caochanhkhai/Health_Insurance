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
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var dspdkDomain = VHIDbContext.PhieuDangKi.ToList();
            if (dspdkDomain == null || dspdkDomain.Count == 0)
            {
                return BadRequest("Không tồn tại Phiếu đăng ký nào.");
            }
            List<PhieuDangKiDTO> dspdkDTO = new List<PhieuDangKiDTO>();
            foreach (var pdk in dspdkDomain)
            {
                PhieuDangKiDTO pdk_dto = CreatePhieuDKDTO(pdk);
                dspdkDTO.Add(pdk_dto);
            }

            return Ok(dspdkDTO);
        }

        [HttpGet]
        [Route("GetAllPhieuDangKiDaDuyet")]
        public IActionResult GetAllPhieuDangKiDaDuyet()
        {
            var dspdkDomain = VHIDbContext.PhieuDangKi.Where(x=>x.TinhTrangDuyet == "Đã Duyệt").ToList();
            if (dspdkDomain == null || dspdkDomain.Count == 0)
            {
                return BadRequest("Không tồn tại Phiếu đăng ký nào đã được duyệt.");
            }
            List<PhieuDangKiDTO> dspdkDTO = new List<PhieuDangKiDTO>();
            foreach (var pdk in dspdkDomain)
            {
                PhieuDangKiDTO pdk_dto = CreatePhieuDKDTO(pdk);
                dspdkDTO.Add(pdk_dto);
            }

            return Ok(dspdkDTO);
        }

        [HttpGet]
        [Route("GetAllPhieuDangKiChuaDuyet")]
        public IActionResult GetAllPhieuDangKiChuaDuyet()
        {
            var dspdkDomain = VHIDbContext.PhieuDangKi.Where(x => x.TinhTrangDuyet == "Chưa Duyệt").ToList();
            if (dspdkDomain == null || dspdkDomain.Count == 0)
            {
                return BadRequest("Không tồn tại Phiếu đăng ký nào chưa được duyệt.");
            }
            List<PhieuDangKiDTO> dspdkDTO = new List<PhieuDangKiDTO>();
            foreach (var pdk in dspdkDomain)
            {
                PhieuDangKiDTO pdk_dto = CreatePhieuDKDTO(pdk);
                dspdkDTO.Add(pdk_dto);
            }

            return Ok(dspdkDTO);
        }

        [HttpGet]
        [Route("GetAllPhieuDangKiTuChoi")]
        public IActionResult GetAllPhieuDangKiTuChoi()
        {
            var dspdkDomain = VHIDbContext.PhieuDangKi.Where(x => x.TinhTrangDuyet == "Từ Chối").ToList();
            if (dspdkDomain == null || dspdkDomain.Count == 0)
            {
                return BadRequest("Không tồn tại Phiếu đăng ký nào bị từ chối.");
            }
            List<PhieuDangKiDTO> dspdkDTO = new List<PhieuDangKiDTO>();
            foreach (var pdk in dspdkDomain)
            {
                PhieuDangKiDTO pdk_dto = CreatePhieuDKDTO(pdk);
                dspdkDTO.Add(pdk_dto);
            }

            return Ok(dspdkDTO);
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int id)
        {
            var pdk = VHIDbContext.PhieuDangKi.FirstOrDefault(x => x.ID_PhieuDangKi == id);
            if (pdk == null)
            {
                return NotFound("Không tìm thấy phiếu đăng ký.");
            }
            PhieuDangKiDTO pdk_dto = CreatePhieuDKDTO(pdk);

            return Ok(pdk_dto);
        }

        [HttpGet]
        [Route("GetByIdKhachHang")]
        public IActionResult GetByIdkh(int idkh)
        {
            var kh = VHIDbContext.KhachHang.FirstOrDefault(x => x.ID_KhachHang == idkh);

            if (kh == null)
            {
                return NotFound("Không tồn tại Khách hàng.");
            }

            var pdk = VHIDbContext.PhieuDangKi.Where(q => q.KhachHangID_KhachHang == idkh).ToList();

            if (pdk == null || pdk.Count() == 0)
            {
                return NotFound("Không tìm thấy Phiếu đăng ký tương ứng với Khách hàng.");
            }

            List<PhieuDangKiDTO> dspdkDTO = new List<PhieuDangKiDTO>();
            foreach (var dk in pdk)
            {
                PhieuDangKiDTO pdk_dto = CreatePhieuDKDTO(dk);
                dspdkDTO.Add(pdk_dto);
            }

            return Ok(dspdkDTO);
        }

        [HttpGet]
        [Route("GetByIdGoiBaoHiem")]
        public IActionResult GetByIdgbh(int idgbh)
        {
            var gbh = VHIDbContext.GoiBaoHiem.FirstOrDefault(x => x.ID_GoiBaoHiem == idgbh);

            if (gbh == null)
            {
                return NotFound("Không tồn tại Gói bảo hiểm.");
            }

            var pdk = VHIDbContext.PhieuDangKi.Where(q => q.GoiBaoHiemID_GoiBaoHiem == idgbh).ToList();

            if (pdk == null || pdk.Count() == 0)
            {
                return NotFound("Không tìm thấy Phiếu đăng ký tương ứng với Gói bảo hiểm.");
            }

            List<PhieuDangKiDTO> dspdkDTO = new List<PhieuDangKiDTO>();
            foreach (var dk in pdk)
            {
                PhieuDangKiDTO pdk_dto = CreatePhieuDKDTO(dk);
                dspdkDTO.Add(pdk_dto);
            }

            return Ok(dspdkDTO);
        }

        [HttpGet]
        [Route("GetByIdNhanVien")]
        public IActionResult GetByIdnv(int idnv)
        {
            var nv = VHIDbContext.NhanVien.FirstOrDefault(x => x.ID_NhanVien == idnv);

            if (nv == null)
            {
                return NotFound("Không tồn tại Nhân viên.");
            }

            var pdk = VHIDbContext.PhieuDangKi.Where(q => q.NhanVienID_NhanVien == idnv).ToList();

            if (pdk == null || pdk.Count() == 0)
            {
                return NotFound("Không tìm thấy Phiếu đăng ký tương ứng với Nhân viên.");
            }

            List<PhieuDangKiDTO> dspdkDTO = new List<PhieuDangKiDTO>();
            foreach (var dk in pdk)
            {
                PhieuDangKiDTO pdk_dto = CreatePhieuDKDTO(dk);
                dspdkDTO.Add(pdk_dto);
            }

            return Ok(dspdkDTO);
        }


        //[HttpGet]
        //[Route("userId:int")]
        //public IActionResult GetAllByUserId(int userId)
        //{
        //    var phieudk = VHIDbContext.PhieuDangKi.Where(x => x.KhachHangID_KhachHang == userId).ToList();

        //    if (phieudk == null)
        //    {
        //        return NotFound();
        //    }

        //    var phieudk_dto = phieudk.Select(phieudk => new PhieuDangKiDTO
        //        { 
        //    ID_PhieuDangKi = phieudk.ID_PhieuDangKi,
        //    TinhTrangDuyet = phieudk.TinhTrangDuyet,
        //    DiaDiemKiKet = phieudk.DiaDiemKiKet,
        //    ThoiGianKiKet = phieudk.ThoiGianKiKet,
        //    ToKhaiSucKhoe = phieudk.ToKhaiSucKhoe,
        //    ID_KhachHang = phieudk.KhachHangID_KhachHang,
        //    ID_GoiBaoHiem = phieudk.GoiBaoHiemID_GoiBaoHiem,
        //    ID_NhanVien = phieudk.NhanVienID_NhanVien
        //}).ToList();
        //    return Ok(phieudk_dto);
        //}

        //[HttpGet]
        //[Route("GetPhieuDangKyById/{id}")]
        //public IActionResult GetPhieuDangKyByID(int id)
        //{
        //    var phieudk = VHIDbContext.PhieuDangKi.Where(x => x.ID_PhieuDangKi == id).ToList();

        //    if (phieudk == null)
        //    {
        //        return NotFound("Không tìm thấy phiếu đăng ký");
        //    }

        //    var phieudk_dto = phieudk.Select(phieudk => new PhieuDangKiDTO
        //    {
        //        ID_PhieuDangKi = phieudk.ID_PhieuDangKi,
        //        TinhTrangDuyet = phieudk.TinhTrangDuyet,
        //        DiaDiemKiKet = phieudk.DiaDiemKiKet,
        //        ThoiGianKiKet = phieudk.ThoiGianKiKet,
        //        ToKhaiSucKhoe = phieudk.ToKhaiSucKhoe,
        //        ID_KhachHang = phieudk.KhachHangID_KhachHang,
        //        ID_GoiBaoHiem = phieudk.GoiBaoHiemID_GoiBaoHiem,
        //        ID_NhanVien = phieudk.NhanVienID_NhanVien
        //    }).ToList();
        //    return Ok(phieudk_dto);
        //}


        [HttpPost]
        [Route("DangKyBaoHiem")]
        public IActionResult CreatePhieuDangKi([FromBody] AddPhieuDangKiDTO dto)
        {
            var kh = VHIDbContext.KhachHang.FirstOrDefault(b => b.ID_KhachHang == dto.ID_KhachHang);
            var gbh = VHIDbContext.GoiBaoHiem.FirstOrDefault(b => b.ID_GoiBaoHiem == dto.ID_GoiBaoHiem);
            if (kh == null)
            {
                return BadRequest("Không tìm thấy Khách hàng.");
            }
            if (gbh == null)
            {
                return BadRequest("Không tìm thấy Gói bảo hiểm.");
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

            var pdk_dto = CreatePhieuDKDTO(PDKdomain);
            return Ok(pdk_dto);
        }

        [HttpPost("XetDuyetPhieuDangKy")]
        public IActionResult XetDuyetPhieuDangKy(int id, string tinhTrangDuyet)
        {
            var phieuDangKy = VHIDbContext.PhieuDangKi.FirstOrDefault(x => x.ID_PhieuDangKi == id);

            if (phieuDangKy == null)
            {
                return NotFound("Không tìm thấy phiếu đăng ký.");
            }

            // Cập nhật tình trạng duyệt và lưu vào cơ sở dữ liệu
            phieuDangKy.TinhTrangDuyet = tinhTrangDuyet;
            VHIDbContext.SaveChanges();

            PhieuDangKiDTO phieudk_dto = CreatePhieuDKDTO(phieuDangKy);

            return Ok(phieudk_dto);
        }

        [HttpPost("ThemNhanVienChoPhieuDangKi")]
        public IActionResult ThemNhanVienChoPhieuDangKi([FromBody] UpdateNhanVienPhieuDangKiDTO dto)
        {
            var phieuDangKi = VHIDbContext.PhieuDangKi.FirstOrDefault(x => x.ID_PhieuDangKi == dto.ID_PhieuDangKi);
            var nhanVien = VHIDbContext.NhanVien.FirstOrDefault(x => x.ID_NhanVien == dto.ID_NhanVien);

            if (phieuDangKi == null)
            {
                return NotFound("Không tìm thấy phiếu đăng ký.");
            }
            if (phieuDangKi.NhanVienID_NhanVien != null)
            {
                return BadRequest("Đã có Nhân viên cho Phiếu đăng kí này. ID nhân viên: " + phieuDangKi.NhanVienID_NhanVien);
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

        [HttpPost("CungCapToKhai(id: int, toKhai: string)")]
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

