using API.Data;
using API.Domain;
using API.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;
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
            var ycctList = VHIDbContext.YeuCauChiTra.ToList();

            if (ycctList == null)
            {
                return NotFound("Không tìm thấy yêu cầu chi trả nào.");
            }

            List<YeuCauChiTraDTO> dsycctDTO = new List<YeuCauChiTraDTO>();
            foreach (var ycct in ycctList)
            {
                var YeuCauChiTraDTO = CreateYeuCauChiTraDTO(ycct);
                dsycctDTO.Add(YeuCauChiTraDTO);
            }
            
            return Ok(dsycctDTO);
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int id)
        {
            var ycct = VHIDbContext.YeuCauChiTra.FirstOrDefault(x => x.ID_YeuCauChiTra == id);
            if (ycct == null)
            {
                return NotFound("Không tìm thấy Yêu cầu chi trả.");
            }
            YeuCauChiTraDTO yctv_dto = CreateYeuCauChiTraDTO(ycct);

            return Ok(yctv_dto);
        }

        [HttpGet]
        [Route("GetByIdQuanLyBaoHiem")]
        public IActionResult GetYCCTByIdQuanLyBaoHiem(int QLBHID)
        {
            var qlbh = VHIDbContext.QuanLyBaoHiem.FirstOrDefault(x=>x.ID == QLBHID);

            if (qlbh == null)
            {
                return NotFound("Không tồn tại Quản Lý Bảo Hiểm.");
            }

            var ycctList = VHIDbContext.YeuCauChiTra.Where(ycct => ycct.QLBHID == QLBHID).ToList();

            if (ycctList.Count() == 0 || ycctList == null)
            {
                return NotFound("Không tìm thấy Yêu cầu chi trả ứng với Quản lý bảo hiểm.");
            }

            List<YeuCauChiTraDTO> dsycctDTO = new List<YeuCauChiTraDTO>();
            foreach (var ycct in ycctList)
            {
                var YeuCauChiTraDTO = CreateYeuCauChiTraDTO(ycct);
                dsycctDTO.Add(YeuCauChiTraDTO);
            }

            return Ok(dsycctDTO);
        }

        [HttpPost]
        [Route("YeuCauChiTra")]
        public IActionResult YeucauChiTra([FromBody] AddYeuCauChiTraDTO dto)
        {
            var qlbh = VHIDbContext.QuanLyBaoHiem.FirstOrDefault(x => x.ID == dto.QLBHID); 
            if (qlbh == null)
            {
                return NotFound("Không tìm thấy quản lý bảo hiểm tương ứng.");
            }

            var YeuCauChiTraDomain = new YeuCauChiTra()
            {
                QLBH = qlbh,
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

            var YeuCauChiTra_dto = CreateYeuCauChiTraDTO(YeuCauChiTraDomain);
            return Ok(YeuCauChiTra_dto);
        }

        [HttpPost("DuyetYeuCauChiTra")]
        public IActionResult DuyetYeuCauChiTra([FromBody] DuyetYeuCauChiTraDTO dto)
        {
            var ycctDomain = VHIDbContext.YeuCauChiTra.FirstOrDefault(x => x.ID_YeuCauChiTra == dto.ID_YeuCauChiTra);

            if (ycctDomain == null)
            {
                return NotFound("Không tìm thấy yêu cầu chi trả.");
            }

            if (dto.TinhTrangDuyet != "Từ Chối" && dto.TinhTrangDuyet != "Đã Duyệt")
            {
                return BadRequest("Tình trạng duyệt không hợp lệ");
            }

            // Cập nhật tình trạng duyệt và lưu vào cơ sở dữ liệu
            ycctDomain.TinhTrangDuyet = dto.TinhTrangDuyet;
            VHIDbContext.SaveChanges();

            YeuCauChiTraDTO ycctDTO = CreateYeuCauChiTraDTO(ycctDomain);

            return Ok(ycctDTO);
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

        private static YeuCauChiTraDTO CreateYeuCauChiTraDTO(YeuCauChiTra? ycct)
        {
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
            return yctv_dto;
        }

    }
}