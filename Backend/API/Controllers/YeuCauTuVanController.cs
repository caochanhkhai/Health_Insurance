using API.Data;
using API.Domain;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YeuCauTuVanController : ControllerBase
    {
        private readonly VHIDbContext VHIDbContext;

        public YeuCauTuVanController(VHIDbContext VHIDbContext)
        {
            this.VHIDbContext = VHIDbContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var dltv = VHIDbContext.DatLichTuVan.ToList();
            if (dltv == null || dltv.Count == 0)
            {
                return BadRequest("Không tồn tại Yêu cầu tư vấn nào.");
            }
            List<DatLichTuVanDTO> dltvDTO = new List<DatLichTuVanDTO>();
            foreach (var dltv_domain in dltv)
            {
                DatLichTuVanDTO dltv_dto = CreateDLTVDTO(dltv_domain);
                dltvDTO.Add(dltv_dto);
            }
            return Ok(dltvDTO);
        }

        [HttpGet]
        [Route("GetAllYeuCauTuVanChuaDuyet")]
        public IActionResult GetAllYeuCauTuVanChuaDuyet()
        {
            var dltv = VHIDbContext.DatLichTuVan.Where(x=>x.TinhTrangDuyet == "Chưa Duyệt").ToList();
            if (dltv == null || dltv.Count == 0)
            {
                return BadRequest("Không tồn tại Yêu cầu tư vấn nào chưa được duyệt.");
            }
            List<DatLichTuVanDTO> dltvDTO = new List<DatLichTuVanDTO>();
            foreach (var dltv_domain in dltv)
            {
                DatLichTuVanDTO dltv_dto = CreateDLTVDTO(dltv_domain);
                dltvDTO.Add(dltv_dto);
            }
            return Ok(dltvDTO);
        }

        [HttpGet]
        [Route("GetAllYeuCauTuVanDaDuyet")]
        public IActionResult GetAllYeuCauTuVanDaDuyet()
        {
            var dltv = VHIDbContext.DatLichTuVan.Where(x => x.TinhTrangDuyet == "Đã Duyệt").ToList();
            if (dltv == null || dltv.Count == 0)
            {
                return BadRequest("Không tồn tại Yêu cầu tư vấn nào đã được duyệt.");
            }
            List<DatLichTuVanDTO> dltvDTO = new List<DatLichTuVanDTO>();
            foreach (var dltv_domain in dltv)
            {
                DatLichTuVanDTO dltv_dto = CreateDLTVDTO(dltv_domain);
                dltvDTO.Add(dltv_dto);
            }
            return Ok(dltvDTO);
        }

        [HttpGet]
        [Route("GetAllYeuCauTuVanTuChoi")]
        public IActionResult GetAllYeuCauTuVanTuChoi()
        {
            var dltv = VHIDbContext.DatLichTuVan.Where(x => x.TinhTrangDuyet == "Từ Chối").ToList();
            if (dltv == null || dltv.Count == 0)
            {
                return BadRequest("Không tồn tại Yêu cầu tư vấn nào bị từ chối.");
            }
            List<DatLichTuVanDTO> dltvDTO = new List<DatLichTuVanDTO>();
            foreach (var dltv_domain in dltv)
            {
                DatLichTuVanDTO dltv_dto = CreateDLTVDTO(dltv_domain);
                dltvDTO.Add(dltv_dto);
            }
            return Ok(dltvDTO);
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int id)
        {
            var yctv = VHIDbContext.DatLichTuVan.FirstOrDefault(x => x.ID_YeuCauTuVan == id);
            if (yctv == null)
            {
                return NotFound();
            }
            var yctv_dto = CreateDLTVDTO(yctv);
            return Ok(yctv_dto);
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

            var dltv = VHIDbContext.DatLichTuVan.Where(q => q.KhachHangID_KhachHang == idkh).ToList();

            if (dltv == null || dltv.Count() == 0)
            {
                return NotFound("Không tìm thấy yêu cầu tư vấn tương ứng với Khách hàng.");
            }

            List<DatLichTuVanDTO> dsdltvDTO = new List<DatLichTuVanDTO>();
            foreach (var tv in dltv)
            {
                DatLichTuVanDTO dltv_dto = CreateDLTVDTO(tv);
                dsdltvDTO.Add(dltv_dto);
            }

            return Ok(dsdltvDTO);
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

            var dltv = VHIDbContext.DatLichTuVan.Where(q => q.NhanVien1ID_NhanVien == idnv || q.NhanVien2ID_NhanVien == idnv).ToList();

            if (dltv == null || dltv.Count() == 0)
            {
                return NotFound("Không tìm thấy yêu cầu tư vấn tương ứng với Nhân viên.");
            }

            List<DatLichTuVanDTO> dsdltvDTO = new List<DatLichTuVanDTO>();
            foreach (var tv in dltv)
            {
                DatLichTuVanDTO dltv_dto = CreateDLTVDTO(tv);
                dsdltvDTO.Add(dltv_dto);
            }

            return Ok(dsdltvDTO);
        }

        [HttpPost]
        [Route("DatLichTuVan")]
        public IActionResult DatLichTuVan([FromBody] AddDatLichTuVanDTO dto)
        {
            var kh = VHIDbContext.KhachHang.FirstOrDefault(x => x.ID_KhachHang == dto.ID_KhachHang);
            if(kh.XacThuc != "Đã Xác Thực")
            {
                return BadRequest("Khách hàng chưa xác thực tài khoản");
            }

            var DatLichTuVanDomain = new DatLichTuVan()
            {
                TinhTrangDuyet = "Chưa Duyệt",
                DiaDiem = dto.DiaDiem,
                ThoiGian = dto.ThoiGian,
                KhachHang = kh
            };
            VHIDbContext.DatLichTuVan.Add(DatLichTuVanDomain);
            VHIDbContext.SaveChanges();

            var DatLichTuVan_dto = CreateDLTVDTO(DatLichTuVanDomain);
            
            return Ok(DatLichTuVan_dto);
        }

        [HttpPost]
        [Route("UpdateTinhTrangDuyet")]
        public IActionResult UpdateTinhTrangDuyet(int id,string tinhTrangDuyet)
        {
            var dltvDomain = VHIDbContext.DatLichTuVan.FirstOrDefault(x => x.ID_YeuCauTuVan == id);

            if (dltvDomain == null)
            {
                return NotFound("Không tìm thấy Yêu cầu tư vấn.");
            }
            if (tinhTrangDuyet != "Từ Chối" && tinhTrangDuyet != "Đã Duyệt")
            {
                return BadRequest("Tình trạng duyệt không hợp lệ!");
            }
            dltvDomain.TinhTrangDuyet = tinhTrangDuyet;
            VHIDbContext.SaveChanges();
            DatLichTuVanDTO updated_dltv_dto = CreateDLTVDTO(dltvDomain);
            return Ok(updated_dltv_dto);
        }

        [HttpPost]
        [Route("UpdateNhanVien")]
        public IActionResult UpdateNhanVien([FromBody] UpdateNhanVienDLTVDTO dto)
        {
            var dltvDomain = VHIDbContext.DatLichTuVan.FirstOrDefault(x => x.ID_YeuCauTuVan == dto.ID_YeuCauTuVan);
            var nhanVienDomain = VHIDbContext.NhanVien.FirstOrDefault(x=>x.ID_NhanVien == dto.ID_NhanVien);

            if (dltvDomain == null)
            {
                return NotFound("Không tìm thấy Yêu cầu tư vấn.");
            }
            if (nhanVienDomain == null)
            {
                return NotFound("Không tìm thấy nhân viên.");
            }
            if(dltvDomain.TinhTrangDuyet != "Đã Duyệt")
            {
                return BadRequest("Tình trạng duyệt Yêu cầu tư vấn không hợp lệ: " + dltvDomain.TinhTrangDuyet);
            }
            if (dltvDomain.NhanVien1ID_NhanVien != null)
            {
                if (dltvDomain.NhanVien1ID_NhanVien == nhanVienDomain.ID_NhanVien)
                {
                    return BadRequest("Nhân viên này đã nhận yêu cầu tư vấn trước đó.");
                }
                if (dltvDomain.NhanVien2ID_NhanVien != null)
                {
                    return BadRequest("Yêu cầu tư vấn đã có đủ nhân viên");
                }
                else if (dltvDomain.NhanVien2ID_NhanVien == null)
                {
                    dltvDomain.NhanVien2 = nhanVienDomain;
                    VHIDbContext.SaveChanges();
                    DatLichTuVanDTO dltv_dto = CreateDLTVDTO(dltvDomain);
                    return Ok(dltv_dto);
                }
            }
            if (dltvDomain.NhanVien2ID_NhanVien != null)
            {
                if (dltvDomain.NhanVien2ID_NhanVien == nhanVienDomain.ID_NhanVien)
                {
                    return BadRequest("Nhân viên này đã nhận yêu cầu tư vấn trước đó.");
                }
            }
            if(dltvDomain.NhanVien1 == null)
            {
                dltvDomain.NhanVien1 = nhanVienDomain;
            }
            VHIDbContext.SaveChanges();
            DatLichTuVanDTO updated_dltv_dto = CreateDLTVDTO(dltvDomain);
            return Ok(updated_dltv_dto);
        }

        private static DatLichTuVanDTO CreateDLTVDTO(DatLichTuVan? dltvDomain)
        {
            return new DatLichTuVanDTO()
            {
                ID_YeuCauTuVan = dltvDomain.ID_YeuCauTuVan,
                TinhTrangDuyet = dltvDomain.TinhTrangDuyet,
                DiaDiem = dltvDomain.DiaDiem,
                ThoiGian = dltvDomain.ThoiGian,
                ID_KhachHang = dltvDomain.KhachHangID_KhachHang,
                ID_NhanVien1 = dltvDomain.NhanVien1ID_NhanVien,
                ID_NhanVien2 = dltvDomain.NhanVien2ID_NhanVien,
            };
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var dltvDomain = VHIDbContext.DatLichTuVan.FirstOrDefault(x => x.ID_YeuCauTuVan == id);
            if (dltvDomain == null)
            {
                return NotFound();
            }
            VHIDbContext.DatLichTuVan.Remove(dltvDomain);
            VHIDbContext.SaveChanges();
            var dltvDTO = new DatLichTuVanDTO()
            {
                ID_YeuCauTuVan = dltvDomain.ID_YeuCauTuVan,
                TinhTrangDuyet = dltvDomain.TinhTrangDuyet,
                DiaDiem = dltvDomain.DiaDiem,
                ThoiGian = dltvDomain.ThoiGian,
                ID_KhachHang = dltvDomain.KhachHangID_KhachHang,
                ID_NhanVien1 = dltvDomain.NhanVien1ID_NhanVien,
                ID_NhanVien2 = dltvDomain.NhanVien2ID_NhanVien,
            };
            return Ok(dltvDTO);
        }

    }
}
