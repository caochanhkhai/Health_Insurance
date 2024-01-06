using API.Data;
using API.DTOs;
using API.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class HopDongController: ControllerBase
    {
        private readonly VHIDbContext VHIDbContext;

        public HopDongController(VHIDbContext VHIDbContext)
        {
            this.VHIDbContext = VHIDbContext;
        }

        [HttpGet]
        [Route("id:int")]
        public IActionResult GetById(int id)
        {
            var qlhd = VHIDbContext.HopDong.FirstOrDefault(x => x.ID_HopDong == id);

            if (qlhd == null)
            {
                return NotFound();
            }
            var qlhd_dto = new HopDongDTO();
            qlhd_dto.ID_HopDong = qlhd.ID_HopDong;
            qlhd_dto.NgayKyKet = qlhd.NgayKyKet;
            qlhd_dto.ThoiHan = qlhd.ThoiHan;
            qlhd_dto.GiaTriHopDong = qlhd.GiaTriHopDong;
            qlhd_dto.DieuKhoan = qlhd.DieuKhoan;
            qlhd_dto.HieuLuc = qlhd.HieuLuc;
            qlhd_dto.ID_PhieuDangKi = qlhd.PhieuDangKiID_PhieuDangKi;
            qlhd_dto.ID_NhanVien = qlhd.NhanVienID_NhanVien;
            qlhd_dto.ID_KhachHang = qlhd.KhachHangID_KhachHang;
            qlhd_dto.ID_GoiBaoHiem = qlhd.GoiBaoHiemID_GoiBaoHiem;
            return Ok(qlhd_dto);
        }

        [HttpGet]
        [Route("idkh:int")]
        public IActionResult GetByIdKh(int idkh)
        {
            string query = $"SELECT * FROM HopDong WHERE KhachHangID_KhachHang = '{idkh}' ";
            var qlhd = VHIDbContext.HopDong.FromSqlRaw(query).ToList();

            if (qlhd == null)
            {
                return NotFound();
            }
            var hd = qlhd[0];
            var qlhd_dto = new HopDongDTO();
            qlhd_dto.ID_HopDong = hd.ID_HopDong;
            qlhd_dto.NgayKyKet = hd.NgayKyKet;
            qlhd_dto.ThoiHan = hd.ThoiHan;
            qlhd_dto.GiaTriHopDong = hd.GiaTriHopDong;
            qlhd_dto.DieuKhoan = hd.DieuKhoan;
            qlhd_dto.HieuLuc = hd.HieuLuc;
            qlhd_dto.ID_PhieuDangKi = hd.PhieuDangKiID_PhieuDangKi;
            qlhd_dto.ID_NhanVien = hd.NhanVienID_NhanVien;
            qlhd_dto.ID_KhachHang = hd.KhachHangID_KhachHang;
            qlhd_dto.ID_GoiBaoHiem = hd.GoiBaoHiemID_GoiBaoHiem;
            return Ok(qlhd_dto);
        }

        [HttpGet]
        [Route("idgbh:int")]
        public IActionResult GetByIdgbh(int idgbh)
        {
            string query = $"SELECT * FROM HopDong WHERE GoiBaoHiemID_GoiBaoHiem = '{idgbh}' ";
            var qlhd = VHIDbContext.HopDong.FromSqlRaw(query).ToList();

            if (qlhd == null)
            {
                return NotFound();
            }
            var hd = qlhd[0];
            var qlhd_dto = new HopDongDTO();
            qlhd_dto.ID_HopDong = hd.ID_HopDong;
            qlhd_dto.NgayKyKet = hd.NgayKyKet;
            qlhd_dto.ThoiHan = hd.ThoiHan;
            qlhd_dto.GiaTriHopDong = hd.GiaTriHopDong;
            qlhd_dto.DieuKhoan = hd.DieuKhoan;
            qlhd_dto.HieuLuc = hd.HieuLuc;
            qlhd_dto.ID_PhieuDangKi = hd.PhieuDangKiID_PhieuDangKi;
            qlhd_dto.ID_NhanVien = hd.NhanVienID_NhanVien;
            qlhd_dto.ID_KhachHang = hd.KhachHangID_KhachHang;
            qlhd_dto.ID_GoiBaoHiem = hd.GoiBaoHiemID_GoiBaoHiem;
            return Ok(qlhd_dto);
        }

        [HttpGet]
        [Route("idnv:int")]
        public IActionResult GetByIdNv(int idnv)
        {
            string query = $"SELECT * FROM HopDong WHERE NhanVienID_NhanVien = '{idnv}' ";
            var qlhd = VHIDbContext.HopDong.FromSqlRaw(query).ToList();

            if (qlhd == null)
            {
                return NotFound();
            }
            var hd = qlhd[0];
            var qlhd_dto = new HopDongDTO();
            qlhd_dto.ID_HopDong = hd.ID_HopDong;
            qlhd_dto.NgayKyKet = hd.NgayKyKet;
            qlhd_dto.ThoiHan = hd.ThoiHan;
            qlhd_dto.GiaTriHopDong = hd.GiaTriHopDong;
            qlhd_dto.DieuKhoan = hd.DieuKhoan;
            qlhd_dto.HieuLuc = hd.HieuLuc;
            qlhd_dto.ID_PhieuDangKi = hd.PhieuDangKiID_PhieuDangKi;
            qlhd_dto.ID_NhanVien = hd.NhanVienID_NhanVien;
            qlhd_dto.ID_KhachHang = hd.KhachHangID_KhachHang;
            qlhd_dto.ID_GoiBaoHiem = hd.GoiBaoHiemID_GoiBaoHiem;
            return Ok(qlhd_dto);
        }

        [HttpGet]
        [Route("idpdk:int")]
        public IActionResult GetByIdPDK(int idpdk)
        {
            string query = $"SELECT * FROM HopDong WHERE PhieuDangKiID_PhieuDangKi = '{idpdk}' ";
            var qlhd = VHIDbContext.HopDong.FromSqlRaw(query).ToList();

            if (qlhd == null)
            {
                return NotFound();
            }
            var hd = qlhd[0];
            var qlhd_dto = new HopDongDTO();
            qlhd_dto.ID_HopDong = hd.ID_HopDong;
            qlhd_dto.NgayKyKet = hd.NgayKyKet;
            qlhd_dto.ThoiHan = hd.ThoiHan;
            qlhd_dto.GiaTriHopDong = hd.GiaTriHopDong;
            qlhd_dto.DieuKhoan = hd.DieuKhoan;
            qlhd_dto.HieuLuc = hd.HieuLuc;
            qlhd_dto.ID_PhieuDangKi = hd.PhieuDangKiID_PhieuDangKi;
            qlhd_dto.ID_NhanVien = hd.NhanVienID_NhanVien;
            qlhd_dto.ID_KhachHang = hd.KhachHangID_KhachHang;
            qlhd_dto.ID_GoiBaoHiem = hd.GoiBaoHiemID_GoiBaoHiem;
            return Ok(qlhd_dto);
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

            var HopDong_dto = new HopDongDTO()
            {
                ID_HopDong = HopDongDomain.ID_HopDong,
                ID_KhachHang = HopDongDomain.KhachHangID_KhachHang,
                ID_GoiBaoHiem = HopDongDomain.GoiBaoHiemID_GoiBaoHiem,
                ID_PhieuDangKi = HopDongDomain.PhieuDangKiID_PhieuDangKi,
                ID_NhanVien = HopDongDomain.NhanVienID_NhanVien,
                NgayKyKet = HopDongDomain.NgayKyKet,
                ThoiHan = HopDongDomain.ThoiHan,
                GiaTriHopDong = HopDongDomain.GiaTriHopDong,
                DieuKhoan = HopDongDomain.DieuKhoan,
                HieuLuc = HopDongDomain.HieuLuc
            };
            return CreatedAtAction(nameof(GetById), new { id = HopDong_dto.ID_HopDong }, HopDong_dto);
        }

        [HttpPut("ChinhSuaHopDong/{id}")]
        public IActionResult ChinhSuaHopDong(int id, [FromBody] HopDongDTO hd)
        {
            var hdDomain = VHIDbContext.HopDong.FirstOrDefault(x => x.ID_HopDong == id);
            //var pdkDomain = VHIDbContext.PhieuDangKi.FirstOrDefault(x => x.ID_PhieuDangKi == idpdk);
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
