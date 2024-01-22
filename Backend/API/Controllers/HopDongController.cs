using API.Data;
using API.DTOs;
using API.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HopDongController: ControllerBase
    {
        private readonly VHIDbContext VHIDbContext;

        public HopDongController(VHIDbContext VHIDbContext)
        {
            this.VHIDbContext = VHIDbContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var hdList = VHIDbContext.HopDong.ToList();

            if (hdList.Count()==0 || hdList == null)
            {
                return NotFound("Không tìm thấy hợp đồng nào.");
            }

            List<HopDongDTO> dshdDTO = new List<HopDongDTO>();
            foreach (var hd in hdList)
            {
                var HopDongDTO = CreateHDDTO(hd);
                dshdDTO.Add(HopDongDTO);
            }

            return Ok(dshdDTO);
        }

        [HttpGet]
        [Route("id:int")]
        public IActionResult GetById(int id)
        {
            var hd = VHIDbContext.HopDong.FirstOrDefault(x => x.ID_HopDong == id);

            if (hd == null)
            {
                return NotFound("Không tìm thấy hợp đồng");
            }
            HopDongDTO hd_dto = CreateHDDTO(hd);
            return Ok(hd_dto);
        }

        [HttpGet]
        [Route("idkh:int")]
        public IActionResult GetByIdKh(int idkh)
        {
            var dshd = VHIDbContext.HopDong.Where(x => x.KhachHangID_KhachHang == idkh).ToList();

            if (dshd.Count() == 0 || dshd == null)
            {
                return NotFound("Không tìm thấy hợp đồng với khách hàng tương ứng");
            }

            List<HopDongDTO> dshdDTO = new List<HopDongDTO>();
            foreach (var hd in dshd)
            {
                HopDongDTO hd_dto = CreateHDDTO(hd);
                dshdDTO.Add(hd_dto);
            }

            return Ok(dshdDTO);
        }

        [HttpGet]
        [Route("idgbh:int")]
        public IActionResult GetByIdgbh(int idgbh)
        {
            var dshd = VHIDbContext.HopDong.Where(x=>x.GoiBaoHiemID_GoiBaoHiem == idgbh).ToList();

            if (dshd.Count() == 0 || dshd == null)
            {
                return NotFound("Không tìm thấy hợp đồng với gói bảo hiểm tương ứng");
            }
            List<HopDongDTO> dshdDTO = new List<HopDongDTO>();
            foreach (var item in dshd)
            {
                HopDongDTO hdDomain = CreateHDDTO(item);
                dshdDTO.Add(hdDomain);
            }
            return Ok(dshdDTO);
        }

        [HttpGet]
        [Route("idnv:int")]
        public IActionResult GetByIdNv(int idnv)
        {
            var dshd = VHIDbContext.HopDong.Where(x => x.NhanVienID_NhanVien == idnv).ToList();

            if (dshd.Count() == 0 || dshd == null)
            {
                return NotFound("Không tìm thấy hợp đồng với nhân viên tương ứng");
            }
            List<HopDongDTO> dshdDTO = new List<HopDongDTO>();
            foreach (var item in dshd)
            {
                HopDongDTO hdDomain = CreateHDDTO(item);
                dshdDTO.Add(hdDomain);
            }
            return Ok(dshdDTO);
        }

        [HttpGet]
        [Route("idpdk:int")]
        public IActionResult GetByIdPDK(int idpdk)
        {
            var dshd = VHIDbContext.HopDong.Where(x => x.PhieuDangKiID_PhieuDangKi == idpdk);

            if (dshd.Count() == 0 || dshd == null)
            {
                return NotFound("Không tìm thấy hợp đồng với phiếu đăng kí tương ứng");
            }
            List<HopDongDTO> dshdDTO = new List<HopDongDTO>();
            foreach (var item in dshd)
            {
                HopDongDTO hdDomain = CreateHDDTO(item);
                dshdDTO.Add(hdDomain);
            }
            return Ok(dshdDTO);
        }

        [HttpPost]
        [Route("HopDong")]
        public IActionResult HopDong([FromBody] HopDongDTO dto)
        {

            var HopDongDomain = new HopDong()
            {
                KhachHangID_KhachHang = dto.ID_KhachHang,
                GoiBaoHiemID_GoiBaoHiem = dto.ID_GoiBaoHiem,
                PhieuDangKiID_PhieuDangKi = dto.ID_PhieuDangKi,
                NhanVienID_NhanVien = dto.ID_NhanVien,
                NgayKyKet = dto.NgayKyKet,
                ThoiHan = dto.ThoiHan,
                GiaTriHopDong = dto.GiaTriHopDong,
                DieuKhoan = dto.DieuKhoan,
                HieuLuc = dto.HieuLuc
            };
            VHIDbContext.HopDong.Add(HopDongDomain);
            VHIDbContext.SaveChanges();

            HopDongDTO HopDong_dto = CreateHDDTO(HopDongDomain);
            return Ok(HopDong_dto);
        }

        [HttpPut("ChinhSuaHopDong/{id}")]
        public IActionResult ChinhSuaHopDong(int id, [FromBody] UpdateHopDongDTO hd)
        {
            var hdDomain = VHIDbContext.HopDong.FirstOrDefault(x => x.ID_HopDong == id);
            if (hdDomain == null)
            {
                return NotFound("Không tìm thấy hợp đồng.");
            }

            // Cập nhật và lưu vào cơ sở dữ liệu
            hdDomain.NgayKyKet = hd.NgayKyKet;
            hdDomain.ThoiHan= hd.ThoiHan;
            hdDomain.GiaTriHopDong = hd.GiaTriHopDong;  
            hdDomain.DieuKhoan= hd.DieuKhoan;   
            hdDomain.HieuLuc= hd.HieuLuc;
            VHIDbContext.SaveChanges();

            HopDongDTO hdDTO = CreateHDDTO(hdDomain);

            return Ok(hdDTO);
        }

        [HttpPut]
        [Route("XacDinhGiaTriHopDong")]
        public IActionResult XacDinhGiaTriHopDong([FromBody] XacDinhGiaTriHopDongDTO dto)
        {
            var hd = VHIDbContext.HopDong.FirstOrDefault(x => x.ID_HopDong == dto.id);

            if (hd == null)
            {
                return NotFound("Không tìm thấy hợp đồng");
            }
            hd.GiaTriHopDong = dto.price;
            VHIDbContext.SaveChanges();

            HopDongDTO hd_dto = CreateHDDTO(hd);
            return Ok(hd_dto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var hdDomain = VHIDbContext.HopDong.FirstOrDefault(x => x.ID_HopDong == id);
            if (hdDomain == null)
            {
                return NotFound("Không tìm thấy hợp đồng");
            }
            VHIDbContext.HopDong.Remove(hdDomain);
            VHIDbContext.SaveChanges();

            HopDongDTO hdDTO = CreateHDDTO(hdDomain);         
            return Ok(hdDTO);
        }

        private static HopDongDTO CreateHDDTO(HopDong? hd)
        {
            var hd_dto = new HopDongDTO();
            hd_dto.ID_HopDong = hd.ID_HopDong;
            hd_dto.ID_KhachHang = hd.KhachHangID_KhachHang;
            hd_dto.ID_PhieuDangKi = hd.PhieuDangKiID_PhieuDangKi;
            hd_dto.ID_GoiBaoHiem = hd.GoiBaoHiemID_GoiBaoHiem;
            hd_dto.ID_NhanVien = hd.NhanVienID_NhanVien;
            hd_dto.NgayKyKet = hd.NgayKyKet;
            hd_dto.ThoiHan = hd.ThoiHan;
            hd_dto.GiaTriHopDong = hd.GiaTriHopDong;
            hd_dto.DieuKhoan = hd.DieuKhoan;
            hd_dto.HieuLuc = hd.HieuLuc;       
            return hd_dto;
        }
    }
}
