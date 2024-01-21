using API.Data;
using API.Domain;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuanLyBaoHiemController : ControllerBase
    {
        private readonly VHIDbContext VHIDbContext;

        public QuanLyBaoHiemController(VHIDbContext VHIDbContext)
        {
            this.VHIDbContext = VHIDbContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var qlBH = VHIDbContext.QuanLyBaoHiem.ToList();
            if (qlBH == null || qlBH.Count() == 0)
            {
                return NotFound("Không tồn tại Quản Lý Bảo Hiểm nào.");
            }
            List<QuanLyBaoHiemDTO> dsqlbhDTO = new List<QuanLyBaoHiemDTO>();
            foreach (var qlbh in qlBH)
            {
                QuanLyBaoHiemDTO qlbh_dto = CreateQuanLyBaoHiemDTO(qlbh);
                dsqlbhDTO.Add(qlbh_dto);
            }
            return Ok(dsqlbhDTO);
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int id)
        {
            var qlbh = VHIDbContext.QuanLyBaoHiem.FirstOrDefault(x => x.ID == id);

            if (qlbh == null )
            {
                return NotFound("Không tìm thấy Quản Lý Bảo Hiểm.");
            }

            var qlbh_dto = CreateQuanLyBaoHiemDTO(qlbh);

            return Ok(qlbh_dto);
        }

        [HttpGet]
        [Route("GetByIdKhachHang")]
        public IActionResult GetByIdKh(int idkh)
        {
            var kh = VHIDbContext.KhachHang.FirstOrDefault(x=>x.ID_KhachHang == idkh);

            if (kh == null)
            {
                return NotFound("Không tồn tại khách hàng.");
            }

            var qlbh = VHIDbContext.QuanLyBaoHiem.Where(q => q.KhachHangID_KhachHang == idkh).ToList();

            if (qlbh == null || qlbh.Count() == 0)
            {
                return NotFound("Không tìm thấy Quản lý bảo hiểm tương ứng với Khách hàng.");
            }

            List<QuanLyBaoHiemDTO> dsqlbhDTO = new List<QuanLyBaoHiemDTO>();
            foreach ( var bh in qlbh )
            {
                QuanLyBaoHiemDTO qlbh_dto = CreateQuanLyBaoHiemDTO(bh);
                dsqlbhDTO.Add(qlbh_dto);
            }

            return Ok(dsqlbhDTO);
        }

        [HttpGet]
        [Route("GetByIdGoiBaoHiem")]
        public IActionResult GetByIdgbh(int idgbh)
        {
            var kh = VHIDbContext.GoiBaoHiem.FirstOrDefault(x => x.ID_GoiBaoHiem == idgbh);

            if (kh == null)
            {
                return NotFound("Không tồn tại Gói bảo hiểm.");
            }

            var qlbh = VHIDbContext.QuanLyBaoHiem.Where(q => q.GoiBaoHiemID_GoiBaoHiem == idgbh).ToList();

            if (qlbh == null || qlbh.Count() == 0)
            {
                return NotFound("Không tìm thấy Quản lý bảo hiểm tương ứng với Gói bảo hiểm.");
            }

            List<QuanLyBaoHiemDTO> dsqlbhDTO = new List<QuanLyBaoHiemDTO>();
            foreach (var bh in qlbh)
            {
                QuanLyBaoHiemDTO qlbh_dto = CreateQuanLyBaoHiemDTO(bh);
                dsqlbhDTO.Add(qlbh_dto);
            }
            return Ok(dsqlbhDTO);
        }

        [HttpPost]
        [Route("QuanLyBaoHiem")]
        public IActionResult QuanLyBaoHiem([FromBody] AddQuanLyBaoHiemDTO dto)
        {
            var kh = VHIDbContext.KhachHang.FirstOrDefault(x=>x.ID_KhachHang == dto.ID_KhachHang);
            var gbh = VHIDbContext.GoiBaoHiem.FirstOrDefault(x=>x.ID_GoiBaoHiem == dto.ID_GoiBaoHiem);
            if (kh == null)
            {
                return NotFound("Không tìm thấy Khách hàng.");
            }
            if (gbh == null)
            {
                return NotFound("Không tìm thấy Gói bảo hiểm.");
            }
            
            var QuanLyBaoHiemDomain = new QuanLyBaoHiem()
            {
                KhachHang = kh,
                GoiBaoHiem = gbh,
                ThoiGianBatDau = dto.ThoiGianBatDau,
                ThoiGianKetThuc = dto.ThoiGianKetThuc,
                HanMucDaSuDung = 0
            };

            VHIDbContext.QuanLyBaoHiem.Add(QuanLyBaoHiemDomain);
            VHIDbContext.SaveChanges();

            QuanLyBaoHiemDTO QuanLyBaoHiem_dto = CreateQuanLyBaoHiemDTO(QuanLyBaoHiemDomain);

            return Ok(QuanLyBaoHiem_dto);
        }

        [HttpPost]
        [Route("CapNhatHanMucSuDung")]
        public IActionResult UpdateHanMucSuDung(int id, decimal HanMucSuDung)
        {
            var qlbhDomain = VHIDbContext.QuanLyBaoHiem.FirstOrDefault(x => x.ID == id);
            if (qlbhDomain == null)
            {
                return NotFound("Không tìm thấy Quản lý bảo hiểm.");
            }
            qlbhDomain.HanMucDaSuDung = qlbhDomain.HanMucDaSuDung + HanMucSuDung;
            VHIDbContext.SaveChanges();

            QuanLyBaoHiemDTO updated_qlbh_dto = CreateQuanLyBaoHiemDTO(qlbhDomain);
            
            return Ok(updated_qlbh_dto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var qlbhDomain = VHIDbContext.QuanLyBaoHiem.FirstOrDefault(x => x.ID == id);
            if (qlbhDomain == null)
            {
                return NotFound();
            }
            VHIDbContext.QuanLyBaoHiem.Remove(qlbhDomain);
            VHIDbContext.SaveChanges();
            var qlbhDTO = new QuanLyBaoHiemDTO()
            {
                ID = qlbhDomain.ID,
                ID_KhachHang = qlbhDomain.KhachHangID_KhachHang,
                ID_GoiBaoHiem = qlbhDomain.GoiBaoHiemID_GoiBaoHiem,
                ThoiGianBatDau = qlbhDomain.ThoiGianBatDau,
                ThoiGianKetThuc = qlbhDomain.ThoiGianKetThuc,
                HanMucDaSuDung = qlbhDomain.HanMucDaSuDung,
            };
            return Ok(qlbhDTO);
        }

        private static QuanLyBaoHiemDTO CreateQuanLyBaoHiemDTO(QuanLyBaoHiem? bh)
        {
            var qlbh_dto = new QuanLyBaoHiemDTO();
            qlbh_dto.ID = bh.ID;
            qlbh_dto.ID_KhachHang = bh.KhachHangID_KhachHang;
            qlbh_dto.ID_GoiBaoHiem = bh.GoiBaoHiemID_GoiBaoHiem;
            qlbh_dto.ThoiGianBatDau = bh.ThoiGianBatDau;
            qlbh_dto.ThoiGianKetThuc = bh.ThoiGianKetThuc;
            qlbh_dto.HanMucDaSuDung = bh.HanMucDaSuDung;
            return qlbh_dto;
        }

    }
}
