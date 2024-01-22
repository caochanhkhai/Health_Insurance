using API.Data;
using API.Domain;
using API.DTOs;
using API.Helper;
using API.MiddleWare;
using API.Service;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuThanhToanBaoHiemController : ControllerBase
    {
        private readonly VHIDbContext VHIDbContext;

        public PhieuThanhToanBaoHiemController(VHIDbContext VHIDbContext)
        {
            this.VHIDbContext = VHIDbContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var dspttbh = VHIDbContext.PhieuThanhToanBaoHiem.ToList();
            if (dspttbh == null || dspttbh.Count == 0)
            {
                return BadRequest("Không tồn tại Phiếu thanh toán bảo hiểm nào.");
            }
            List<PhieuThanhToanBaoHiemDTO> dspttbhDTO = new List<PhieuThanhToanBaoHiemDTO>();
            foreach (var pttbh in dspttbh)
            {
                PhieuThanhToanBaoHiemDTO pttbh_dto = CreatePhieuThanhToanBaoHiemDTO(pttbh);
                dspttbhDTO.Add(pttbh_dto);
            }
            return Ok(dspttbhDTO);
        }

        [HttpGet]
        [Route("id")]
        public IActionResult GetById(int id)
        {
            var pttbh = VHIDbContext.PhieuThanhToanBaoHiem.FirstOrDefault(x => x.ID_PhieuThanhToan== id);
            if (pttbh == null)
            {
                return NotFound("Không tìm thấy Phiếu thanh toán bảo hiểm.");
            }
            var pttbh_dto = CreatePhieuThanhToanBaoHiemDTO(pttbh);
            return Ok(pttbh_dto);
        }

        [HttpGet]
        [Route("GetByIdHopDong")]
        public IActionResult GetByIdhd(int idhd)
        {
            var hd = VHIDbContext.HopDong.FirstOrDefault(x => x.ID_HopDong == idhd);

            if (hd == null)
            {
                return NotFound("Không tồn tại Hợp đồng.");
            }

            var pttbh = VHIDbContext.PhieuThanhToanBaoHiem.Where(q => q.HopDongID_HopDong == idhd).ToList();

            if (pttbh == null || pttbh.Count() == 0)
            {
                return NotFound("Không tìm thấy Hợp đồng tương ứng với Phiếu thanh toán bảo hiểm.");
            }

            List<PhieuThanhToanBaoHiemDTO> dspttbhDTO = new List<PhieuThanhToanBaoHiemDTO>();
            foreach (var ttbh in pttbh)
            {
                PhieuThanhToanBaoHiemDTO pttbh_dto = CreatePhieuThanhToanBaoHiemDTO(ttbh);
                dspttbhDTO.Add(pttbh_dto);
            }

            return Ok(dspttbhDTO);
        }

        [HttpGet]
        [Route("GetPhieuThanhToanDenHan")]
        public IActionResult GetPhieuThanhToanDenHan(int soNgay)
        {
            var pttdhList = VHIDbContext.PhieuThanhToanDenHan.FromSqlRaw("EXEC GetHopDongWithDays "+ soNgay).ToList();
            if (pttdhList == null || pttdhList.Count == 0)
            {
                return NotFound("Không tồn tại Phiếu thanh toán đến hạn trong "+ soNgay +" ngày.");
            }

            var pttdhDTOList = pttdhList.Select(pttdh => new PhieuThanhToanDenHanDTO
            {
                ID_HopDong = pttdh.ID_HopDong,
                TenBaoHiem = pttdh.TenBaoHiem,
                TenGoi = pttdh.TenGoi,
                SoNgayDenHan = pttdh.SoNgayDenHan,
                SoTienCanDong = pttdh.SoTienCanDong
            }).ToList();
            
            return Ok(pttdhDTOList);
        }

        [HttpPost]
        [Route("ThemPhieuThanhToanBaoHiem")]
        public IActionResult CreatePhieuThanhToanBaoHiem([FromBody] AddPhieuThanhToanBaoHiemDTO dto)
        {
            var hd = VHIDbContext.HopDong.FirstOrDefault(x => x.ID_HopDong == dto.ID_HopDong);
            if (hd == null)
            {
                return NotFound("Không tìm thấy hợp đồng.");
            }
            if (dto.SoTien == 0)
            {
                return BadRequest("Chưa có số tiền thanh toán.");
            }
            if (dto.HinhThucThanhToan != "Tiền Mặt" && dto.HinhThucThanhToan != "Chuyển Khoản")
            {
                return BadRequest("Hình thức thanh toán không hợp lệ.");
            }
            PhieuThanhToanBaoHiem pttbhDomain = CreatePhieuThanhToanBaoHiemDomain(dto, hd);

            VHIDbContext.PhieuThanhToanBaoHiem.Add(pttbhDomain);
            VHIDbContext.SaveChanges();

            PhieuThanhToanBaoHiemDTO pttbhDTO = CreatePhieuThanhToanBaoHiemDTO(pttbhDomain);

            return Ok(pttbhDTO);
        }

        [HttpPost("XetDuyetPhieuThanhToan/{id}")]
        public IActionResult XetDuyetPhieuThanhToan(int id, string tinhTrangDuyet)
        {
            var phieuThanhToanDomain = VHIDbContext.PhieuThanhToanBaoHiem.FirstOrDefault(x => x.ID_PhieuThanhToan == id);

            if (phieuThanhToanDomain == null)
            {
                return NotFound("Không tìm thấy Phiếu thanh toán bảo hiểm.");
            }
            if (tinhTrangDuyet != "Từ Chối" && tinhTrangDuyet != "Đã Duyệt")
            {
                return BadRequest("Tình trạng duyệt không hợp lệ");
            }
            // Cập nhật tình trạng duyệt và lưu vào cơ sở dữ liệu
            phieuThanhToanDomain.TinhTrangDuyet = tinhTrangDuyet;
            VHIDbContext.SaveChanges();

            PhieuThanhToanBaoHiemDTO pttbhDTO = CreatePhieuThanhToanBaoHiemDTO(phieuThanhToanDomain);

            return Ok(pttbhDTO);
        }

        private static PhieuThanhToanBaoHiemDTO CreatePhieuThanhToanBaoHiemDTO(PhieuThanhToanBaoHiem pttbhDomain)
        {
            return new PhieuThanhToanBaoHiemDTO()
            {
                ID_PhieuThanhToan = pttbhDomain.ID_PhieuThanhToan,
                NgayThanhToan = pttbhDomain.NgayThanhToan,
                HinhThucThanhToan = pttbhDomain.HinhThucThanhToan,
                SoTien = pttbhDomain.SoTien,
                TinhTrangDuyet = pttbhDomain.TinhTrangDuyet,
                ID_HopDong = pttbhDomain.HopDongID_HopDong
            };
        }

        private static PhieuThanhToanBaoHiem CreatePhieuThanhToanBaoHiemDomain(AddPhieuThanhToanBaoHiemDTO dto, HopDong? hd)
        {
            return new PhieuThanhToanBaoHiem()
            {
                NgayThanhToan = dto.NgayThanhToan,
                HinhThucThanhToan = dto.HinhThucThanhToan,
                SoTien = dto.SoTien,
                TinhTrangDuyet = "Chưa Duyệt",
                HopDong = hd
            };
        }

    }
}
