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
        [Route("GetById")]
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
        [Route("GetByIdKh")]
        public IActionResult GetByIdKh(int idkh)
        {
            var kh = VHIDbContext.KhachHang.FirstOrDefault(x => x.ID_KhachHang == idkh);

            if (kh == null)
            {
                return NotFound("Không tìm thấy Khách hàng.");
            }

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
        [Route("GetByIdgbh")]
        public IActionResult GetByIdgbh(int idgbh)
        {
            var gbh = VHIDbContext.GoiBaoHiem.FirstOrDefault(x => x.ID_GoiBaoHiem == idgbh);

            if (gbh == null)
            {
                return NotFound("Không tìm thấy Gói bảo hiểm.");
            }

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
        [Route("GetByIdNv")]
        public IActionResult GetByIdNv(int idnv)
        {
            var nv = VHIDbContext.NhanVien.FirstOrDefault(x => x.ID_NhanVien == idnv);

            if (nv == null)
            {
                return NotFound("Không tìm thấy Nhân viên.");
            }

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
        [Route("GetByIdPDK")]
        public IActionResult GetByIdPDK(int idpdk)
        {
            var pdk = VHIDbContext.PhieuDangKi.FirstOrDefault(x=>x.ID_PhieuDangKi == idpdk);

            if(pdk == null)
            {
                return NotFound("Không tìm thấy Phiếu đăng kí.");
            }

            var hd = VHIDbContext.HopDong.FirstOrDefault(x => x.PhieuDangKiID_PhieuDangKi == idpdk);

            if (hd == null)
            {
                return NotFound("Không tìm thấy hợp đồng với phiếu đăng kí tương ứng");
            }
            
            HopDongDTO hdDTO = CreateHDDTO(hd);
               
            return Ok(hdDTO);
        }

        [HttpPost]
        [Route("HopDong")]
        public IActionResult HopDong(int idPDK)
        {
            var pdk = VHIDbContext.PhieuDangKi.FirstOrDefault(x => x.ID_PhieuDangKi == idPDK);
            if (pdk == null)
            {
                return NotFound("Không tìm thấy Phiếu đăng kí");
            }
            var hd = VHIDbContext.HopDong.FirstOrDefault(x=>x.PhieuDangKiID_PhieuDangKi == idPDK);
            if (hd != null)
            {
                return BadRequest("Đã tồn tại Hợp đồng cho phiếu đăng kí này.");
            }
            if(pdk.NhanVienID_NhanVien == null)
            {
                return BadRequest("Chưa có Nhân viên tiếp nhận Phiếu đăng kí.");
            }

            var kh = VHIDbContext.KhachHang.FirstOrDefault(x => x.ID_KhachHang == pdk.KhachHangID_KhachHang);
            var nv = VHIDbContext.NhanVien.FirstOrDefault(x => x.ID_NhanVien == pdk.NhanVienID_NhanVien);
            var gbh = VHIDbContext.GoiBaoHiem.FirstOrDefault(x => x.ID_GoiBaoHiem == pdk.GoiBaoHiemID_GoiBaoHiem);

            var HopDongDomain = new HopDong()
            {
                KhachHang = kh,
                GoiBaoHiem = gbh,
                PhieuDangKi = pdk,
                NhanVien = nv,
                DieuKhoan = "Chưa có điều khoản",
                TrangThai = "Dự Thảo"
            };
            VHIDbContext.HopDong.Add(HopDongDomain);
            VHIDbContext.SaveChanges();

            HopDongDTO HopDong_dto = CreateHDDTO(HopDongDomain);
            return Ok(HopDong_dto);
        }

        [HttpPost("ChinhSuaHopDong/{id}")]
        public IActionResult ChinhSuaHopDong(int id, [FromBody] UpdateHopDongDTO hd)
        {
            var hdDomain = VHIDbContext.HopDong.FirstOrDefault(x => x.ID_HopDong == id);
            if (hdDomain == null)
            {
                return NotFound("Không tìm thấy hợp đồng.");
            }
            
            if(hd.TrangThai !=  "Dự Thảo" && hd.TrangThai != "Hiệu Lực" && hd.TrangThai != "Hết Hiệu Lực")
            {
                return BadRequest("Trạng thái hợp đồng không hợp lệ: " + hd.TrangThai);
            }

            // Cập nhật thông tin và lưu vào cơ sở dữ liệu
            hdDomain.NgayKyKet = hd.NgayKyKet;
            hdDomain.ThoiHan= hd.ThoiHan; 
            hdDomain.DieuKhoan= hd.DieuKhoan;   
            hdDomain.HieuLuc= hd.HieuLuc;
            hdDomain.TrangThai = hd.TrangThai;
            VHIDbContext.SaveChanges();

            HopDongDTO hdDTO = CreateHDDTO(hdDomain);

            return Ok(hdDTO);
        }

        [HttpPost]
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
            hd_dto.TrangThai = hd.TrangThai;
            return hd_dto;
        }
    }
}
