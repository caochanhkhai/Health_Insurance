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
            return Ok(qlBH);
        }

        [HttpGet]
        [Route("id:int")]
        public IActionResult GetById(int id)
        {
            var qlbh = VHIDbContext.QuanLyBaoHiem.FirstOrDefault(x => x.ID == id);

            if (qlbh == null)
            {
                return NotFound();
            }
            var qlbh_dto = new QuanLyBaoHiemDTO();
            qlbh_dto.ID = qlbh.ID;
            qlbh_dto.ID_KhachHang = qlbh.KhachHangID_KhachHang;
            qlbh_dto.ID_GoiBaoHiem = qlbh.GoiBaoHiemID_GoiBaoHiem;
            qlbh_dto.ThoiGianBatDau = qlbh.ThoiGianBatDau;
            qlbh_dto.ThoiGianKetThuc = qlbh.ThoiGianKetThuc;
            qlbh_dto.HanMucDaSuDung = qlbh.HanMucDaSuDung;
            return Ok(qlbh_dto);
        }

        [HttpGet]
        [Route("idkh:int")]
        public IActionResult GetByIdKh(int idkh)
        {
            string query = $"SELECT * FROM QuanLyBaoHiem WHERE KhachHangID_KhachHang = '{idkh}' ";
            var qlbh = VHIDbContext.QuanLyBaoHiem.FromSqlRaw(query).ToList();

            if (qlbh == null)
            {
                return NotFound();
            }
            var bh = qlbh[0];
            var qlbh_dto = new QuanLyBaoHiemDTO();
            qlbh_dto.ID = bh.ID;
            qlbh_dto.ID_KhachHang = bh.KhachHangID_KhachHang;
            qlbh_dto.ID_GoiBaoHiem = bh.GoiBaoHiemID_GoiBaoHiem;
            qlbh_dto.ThoiGianBatDau = bh.ThoiGianBatDau;
            qlbh_dto.ThoiGianKetThuc = bh.ThoiGianKetThuc;
            qlbh_dto.HanMucDaSuDung = bh.HanMucDaSuDung;
            return Ok(qlbh_dto);
        }

        [HttpPost]
        [Route("QuanLyBaoHiem")]
        public IActionResult QuanLyBaoHiem([FromBody] QuanLyBaoHiemDTO dto)
        {

            var QuanLyBaoHiemDomain = new QuanLyBaoHiem()
            {
                KhachHangID_KhachHang = dto.ID_KhachHang,
                GoiBaoHiemID_GoiBaoHiem = dto.ID_GoiBaoHiem,
                ThoiGianBatDau = dto.ThoiGianBatDau,
                ThoiGianKetThuc = dto.ThoiGianKetThuc,
                HanMucDaSuDung = dto.HanMucDaSuDung
            };
            VHIDbContext.QuanLyBaoHiem.Add(QuanLyBaoHiemDomain);
            VHIDbContext.SaveChanges();

            var QuanLyBaoHiem_dto = new QuanLyBaoHiemDTO()
            {
                ID = QuanLyBaoHiemDomain.ID,
                ID_KhachHang = QuanLyBaoHiemDomain.KhachHangID_KhachHang,
                ID_GoiBaoHiem = QuanLyBaoHiemDomain.GoiBaoHiemID_GoiBaoHiem,
                ThoiGianBatDau = QuanLyBaoHiemDomain.ThoiGianBatDau,
                ThoiGianKetThuc = QuanLyBaoHiemDomain.ThoiGianKetThuc,
                HanMucDaSuDung = QuanLyBaoHiemDomain.HanMucDaSuDung

            };
            return CreatedAtAction(nameof(GetById), new { id = QuanLyBaoHiem_dto.ID }, QuanLyBaoHiem_dto);
        }

        [HttpPut]
        [Route("CapNhatHanMucSuDung{id:int}")]
        public IActionResult UpdateHanMucSuDung([FromRoute] int id, decimal HanMucSuDung)
        {
            var qlbhDomain = VHIDbContext.QuanLyBaoHiem.FirstOrDefault(x => x.ID == id);
            if (qlbhDomain == null)
            {
                return NotFound();
            }
            qlbhDomain.HanMucDaSuDung = qlbhDomain.HanMucDaSuDung + HanMucSuDung;
            VHIDbContext.SaveChanges();
            var updated_qlbh_dto = new QuanLyBaoHiemDTO()
            {
                ID_KhachHang = qlbhDomain.KhachHangID_KhachHang,
                ID_GoiBaoHiem = qlbhDomain.GoiBaoHiemID_GoiBaoHiem,
                ThoiGianBatDau = qlbhDomain.ThoiGianBatDau,
                ThoiGianKetThuc = qlbhDomain.ThoiGianKetThuc,
                HanMucDaSuDung = qlbhDomain.HanMucDaSuDung

            };
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

    }
}
