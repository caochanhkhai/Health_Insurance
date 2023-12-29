using API.Data;
using API.Domain;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class YeuCauChiTraController : ControllerBase
    {
        private readonly VHIDbContext VHIDbContext;

        public YeuCauChiTraController(VHIDbContext VHIDbContext)
        {
            this.VHIDbContext = VHIDbContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var ycct = VHIDbContext.YeuCauChiTra.ToList();
            return Ok(ycct);
        }

        [HttpGet]
        [Route("id:int")]
        public IActionResult GetById(int id)
        {
            var ycct = VHIDbContext.YeuCauChiTra.FirstOrDefault(x => x.ID_YeuCauChiTra == id);
            if (ycct == null)
            {
                return NotFound();
            }
            var yctv_dto = new YeuCauChiTraDTO();
            yctv_dto.ID_YeuCauChiTra = ycct.ID_YeuCauChiTra;
            yctv_dto.QLBHID = ycct.QLBHID;
            yctv_dto.NguoiYeuCau = ycct.NguoiYeuCau;
            yctv_dto.DiaChi = ycct.DiaChi;
            yctv_dto.DienThoai = ycct.DienThoai;
            yctv_dto.Email = ycct.Email;
            yctv_dto.MoiQuanHe = ycct.MoiQuanHe;
            yctv_dto.SoTienYeuCauChiTra = ycct.SoTienYeuCauChiTra;
            yctv_dto.TruongHopChiTra = ycct.TruongHopChiTra;
            yctv_dto.NgayYeuCau = ycct.NgayYeuCau;
            yctv_dto.TinhTrangDuyet = ycct.TinhTrangDuyet;
            yctv_dto.NoiDieuTri = ycct.NoiDieuTri;
            yctv_dto.ChanDoan = ycct.ChanDoan;
            yctv_dto.HauQua = ycct.HauQua;
            yctv_dto.HinhThucDieuTri = ycct.HinhThucDieuTri;
            yctv_dto.NgayBatDau = ycct.NgayBatDau;
            yctv_dto.NgayKetThuc = ycct.NgayKetThuc;
            yctv_dto.HinhHoaDon = ycct.HinhHoaDon;
            return Ok(yctv_dto);
        }

        [HttpPost]
        [Route("YeuCauChiTra")]
        public IActionResult YeucauChiTra([FromBody] YeuCauChiTraDTO dto)
        {

            var YeuCauChiTraDomain = new YeuCauChiTra()
            {
                QLBHID = dto.QLBHID,
                NguoiYeuCau = dto.NguoiYeuCau,
                DiaChi = dto.DiaChi,
                DienThoai = dto.DienThoai,
                Email = dto.Email,
                MoiQuanHe = dto.MoiQuanHe,
                SoTienYeuCauChiTra = dto.SoTienYeuCauChiTra,
                TruongHopChiTra = dto.TruongHopChiTra,
                NgayYeuCau = dto.NgayYeuCau,
                TinhTrangDuyet = "Chưa Duyệt",
                NoiDieuTri = dto.NoiDieuTri,
                ChanDoan = dto.ChanDoan,
                HauQua = dto.HauQua,
                HinhThucDieuTri = dto.HinhThucDieuTri,
                NgayBatDau = dto.NgayBatDau,
                NgayKetThuc = dto.NgayKetThuc,
                HinhHoaDon = dto.HinhHoaDon
            };
            VHIDbContext.YeuCauChiTra.Add(YeuCauChiTraDomain);
            VHIDbContext.SaveChanges();

            var YeuCauChiTra_dto = new YeuCauChiTraDTO()
            {
                ID_YeuCauChiTra = YeuCauChiTraDomain.ID_YeuCauChiTra,
                QLBHID = YeuCauChiTraDomain.QLBHID,
                NguoiYeuCau = YeuCauChiTraDomain.NguoiYeuCau,
                DiaChi = YeuCauChiTraDomain.DiaChi,
                DienThoai = YeuCauChiTraDomain.DienThoai,
                Email = YeuCauChiTraDomain.Email,
                MoiQuanHe = YeuCauChiTraDomain.MoiQuanHe,
                SoTienYeuCauChiTra = YeuCauChiTraDomain.SoTienYeuCauChiTra,
                TruongHopChiTra = YeuCauChiTraDomain.TruongHopChiTra,
                NgayYeuCau = YeuCauChiTraDomain.NgayYeuCau,
                TinhTrangDuyet = YeuCauChiTraDomain.TinhTrangDuyet,
                NoiDieuTri = YeuCauChiTraDomain.NoiDieuTri,
                ChanDoan = YeuCauChiTraDomain.ChanDoan,
                HauQua = YeuCauChiTraDomain.HauQua,
                HinhThucDieuTri = YeuCauChiTraDomain.HinhThucDieuTri,
                NgayBatDau = YeuCauChiTraDomain.NgayBatDau,
                NgayKetThuc = YeuCauChiTraDomain.NgayKetThuc,
                HinhHoaDon = YeuCauChiTraDomain.HinhHoaDon
            };
            return CreatedAtAction(nameof(GetById), new { id = YeuCauChiTra_dto.ID_YeuCauChiTra }, YeuCauChiTra_dto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update([FromRoute] int id, [FromBody] YeuCauChiTraDTO dto)
        {
            var ycctDomain = VHIDbContext.YeuCauChiTra.FirstOrDefault(x => x.ID_YeuCauChiTra == id);
            if (ycctDomain == null)
            {
                return NotFound();
            }
            ycctDomain.QLBHID = dto.QLBHID;
            ycctDomain.NguoiYeuCau = dto.NguoiYeuCau;
            ycctDomain.DiaChi = dto.DiaChi;
            ycctDomain.DienThoai = dto.DienThoai;
            ycctDomain.Email = dto.Email;
            ycctDomain.MoiQuanHe = dto.MoiQuanHe;
            ycctDomain.SoTienYeuCauChiTra = dto.SoTienYeuCauChiTra;
            ycctDomain.TruongHopChiTra = dto.TruongHopChiTra;
            ycctDomain.NgayYeuCau = dto.NgayYeuCau;
            ycctDomain.TinhTrangDuyet = dto.TinhTrangDuyet;
            ycctDomain.NoiDieuTri = dto.NoiDieuTri;
            ycctDomain.ChanDoan = dto.ChanDoan;
            ycctDomain.HauQua = dto.HauQua;
            ycctDomain.HinhThucDieuTri = dto.HinhThucDieuTri;
            ycctDomain.NgayBatDau = dto.NgayBatDau;
            ycctDomain.NgayKetThuc = dto.NgayKetThuc;
            ycctDomain.HinhHoaDon = dto.HinhHoaDon;
            VHIDbContext.SaveChanges();
            var updated_ycct_dto = new YeuCauChiTraDTO()
            {
                QLBHID = ycctDomain.QLBHID,
                NguoiYeuCau = ycctDomain.NguoiYeuCau,
                DiaChi = ycctDomain.DiaChi,
                DienThoai = ycctDomain.DienThoai,
                Email = ycctDomain.Email,
                MoiQuanHe = ycctDomain.MoiQuanHe,
                SoTienYeuCauChiTra = ycctDomain.SoTienYeuCauChiTra,
                TruongHopChiTra = ycctDomain.TruongHopChiTra,
                NgayYeuCau = ycctDomain.NgayYeuCau,
                TinhTrangDuyet = ycctDomain.TinhTrangDuyet,
                NoiDieuTri = ycctDomain.NoiDieuTri,
                ChanDoan = ycctDomain.ChanDoan,
                HauQua = ycctDomain.HauQua,
                HinhThucDieuTri = ycctDomain.HinhThucDieuTri,
                NgayBatDau = ycctDomain.NgayBatDau,
                NgayKetThuc = ycctDomain.NgayKetThuc,
                HinhHoaDon = ycctDomain.HinhHoaDon

            };
            return Ok(updated_ycct_dto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var ycctDomain = VHIDbContext.YeuCauChiTra.FirstOrDefault(x => x.ID_YeuCauChiTra == id);
            if (ycctDomain == null)
            {
                return NotFound();
            }
            VHIDbContext.YeuCauChiTra.Remove(ycctDomain);
            VHIDbContext.SaveChanges();
            var ycctDTO = new YeuCauChiTraDTO()
            {
                ID_YeuCauChiTra = ycctDomain.ID_YeuCauChiTra,
                QLBHID = ycctDomain.QLBHID,
                NguoiYeuCau = ycctDomain.NguoiYeuCau,
                DiaChi = ycctDomain.DiaChi,
                DienThoai = ycctDomain.DienThoai,
                Email = ycctDomain.Email,
                MoiQuanHe = ycctDomain.MoiQuanHe,
                SoTienYeuCauChiTra = ycctDomain.SoTienYeuCauChiTra,
                TruongHopChiTra = ycctDomain.TruongHopChiTra,
                NgayYeuCau = ycctDomain.NgayYeuCau,
                TinhTrangDuyet = ycctDomain.TinhTrangDuyet,
                NoiDieuTri = ycctDomain.NoiDieuTri,
                ChanDoan = ycctDomain.ChanDoan,
                HauQua = ycctDomain.HauQua,
                HinhThucDieuTri = ycctDomain.HinhThucDieuTri,
                NgayBatDau = ycctDomain.NgayBatDau,
                NgayKetThuc = ycctDomain.NgayKetThuc,
                HinhHoaDon = ycctDomain.HinhHoaDon
            };
            return Ok(ycctDTO);
        }

    }
}
