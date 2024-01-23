using API.Data;
using API.Domain;
using API.DTOs;
using API.Helper;
using API.MiddleWare;
using API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        private readonly VHIDbContext VHIDbContext;
        private readonly IEmailService emailService;

        public KhachHangController(VHIDbContext VHIDbContext, IEmailService emailService)
        {
            this.VHIDbContext = VHIDbContext;
            this.emailService = emailService;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var dskh = VHIDbContext.KhachHang.ToList();
            if (dskh.Count() == 0 || dskh == null)
            {
                return NotFound("Không tìm thấy Khách hàng nào.");
            }

            List<KhachHangDTO> dskhDTO = new List<KhachHangDTO>();
            foreach (var khachHang in dskh)
            {
                KhachHangDTO kh_dto = CreateKHDTO(khachHang, khachHang.CongTyID_CongTy, khachHang.TaiKhoanID_TaiKhoan);
                dskhDTO.Add(kh_dto);
            }
            return Ok(dskhDTO);
        }

        [HttpGet]
        [Route("id")]
        public IActionResult GetById(int id)
        {
            var kh = VHIDbContext.KhachHang.FirstOrDefault(x => x.ID_KhachHang == id);
            if (kh == null)
            {
                return NotFound("Không tìm thấy Khách hàng.");
            }
            KhachHangDTO kh_dto = CreateKHDTO(kh, kh.CongTyID_CongTy,kh.TaiKhoanID_TaiKhoan);
            return Ok(kh_dto);
        }

        [HttpGet]
        [Route("GetByIdTaiKhoan")]
        public IActionResult GetByIdtk(int idtk)
        {
            var tk = VHIDbContext.TaiKhoan.FirstOrDefault(x => x.ID_TaiKhoan == idtk);

            if (tk == null)
            {
                return NotFound("Không tồn tại Tài khoản.");
            }

            var kh = VHIDbContext.KhachHang.FirstOrDefault(q => q.TaiKhoanID_TaiKhoan == idtk);

            if (kh == null)
            {
                return NotFound("Không tìm thấy Tài khoản tương ứng với Khách hàng.");
            }

            KhachHangDTO kh_dto = CreateKHDTO(kh, kh.CongTyID_CongTy, kh.TaiKhoanID_TaiKhoan);
               

            return Ok(kh_dto);
        }

        [HttpGet]
        [Route("GetByIdCongty")]
        public IActionResult GetByIdct(int idct)
        {
            var nv = VHIDbContext.CongTy.FirstOrDefault(x => x.ID_CongTy == idct);

            if (nv == null)
            {
                return NotFound("Không tồn tại Công ty.");
            }

            var dskh = VHIDbContext.KhachHang.Where(q => q.CongTyID_CongTy == idct).ToList();

            if (dskh == null || dskh.Count() == 0)
            {
                return NotFound("Không tìm thấy Công ty tương ứng với Khách hàng.");
            }

            List<KhachHangDTO> dskhDTO = new List<KhachHangDTO>();
            foreach (var kh in dskh)
            {
                KhachHangDTO kh_dto = CreateKHDTO(kh, kh.CongTyID_CongTy, kh.TaiKhoanID_TaiKhoan);
                dskhDTO.Add(kh_dto);
            }

            return Ok(dskhDTO);
        }

        [HttpPost]
        [Route("ThemKhachHang")]
        public IActionResult CreateKhachHang([FromBody] AddKhachHangDTO dto, int idtk)
        {
            var taiKhoan = VHIDbContext.TaiKhoan.FirstOrDefault(b => b.ID_TaiKhoan == idtk);
            if(taiKhoan == null)
            {
                return NotFound("Không tìm thấy Tài khoản");
            }
            var khDomain = VHIDbContext.KhachHang.FirstOrDefault(x=>x.TaiKhoanID_TaiKhoan==idtk);
            if (khDomain != null)
            {
                return BadRequest("Đã tồn tại khách hàng cho tài khoản này.");
            }

            var dskh = VHIDbContext.KhachHang.ToList();
            foreach (var kh in dskh)
            {
                if (dto.CMND == kh.CMND)
                {
                    return BadRequest("CMND đã tồn tại!");
                }
            }
            foreach (var kh in dskh)
            {
                if (dto.Email == kh.Email)
                {
                    return BadRequest("Email đã tồn tại!");
                }
            }
            foreach (var kh in dskh)
            {
                if (dto.SoTaiKhoan == kh.SoTaiKhoan)
                {
                    return BadRequest("Số Tài Khoản đã tồn tại!");
                }
            }
            foreach (var kh in dskh)
            {
                string sdt = kh.SoDienThoai.TrimEnd();
                if (dto.SoDienThoai == sdt)
                {
                    return BadRequest("Số Điện Thoại đã tồn tại!");
                }
            }

            KhachHang KhachHangDomain = CreateKHDomain(dto, taiKhoan);
            VHIDbContext.KhachHang.Add(KhachHangDomain);
            VHIDbContext.SaveChanges();
            KhachHangDTO kh_dto = CreateKHDTO(KhachHangDomain,0, taiKhoan.ID_TaiKhoan);
            //Gửi email cho khách hàng
            var jwtService = new JwtService("vhihealthinsurance");
            var accessToken = jwtService.GenerateToken(KhachHangDomain.ID_KhachHang.ToString(), "", 10);
            Mailrequest mailrequest = new Mailrequest()
            {
                ToEmail = KhachHangDomain.Email,
                Subject = "Xác thực tài khoản Bảo hiểm sức khỏe VHI",
                Body = $"<div>Để xác thực tài khoản của bạn trên trang web Bảo hiểm sức khỏe VHI, vui lòng click vào đường dẫn sau đây: <a href=\"http://localhost:8081/VerifyEmail?accessToken={accessToken}\">Link</a></div>"
            };
            emailService.SendEmailAsync(mailrequest);

            return Ok(kh_dto);
        }

        [HttpPost]
        [Route("UpdateThongTinCaNhanKhachHang(id)")]
        public IActionResult UpdateThongTinCaNhanKhachHang(int id, [FromBody] UpdateTTCNKhachHangDTO dto)
        {
            var khDomain = VHIDbContext.KhachHang.FirstOrDefault(x => x.ID_KhachHang == id);
            if (khDomain == null)
            {
                return NotFound("Không tìm thấy Khách hàng.");
            }
            //Lấy ra danh sách khách hàng khác không phải là khách hàng đang cần cập nhật thông tin
            var dskhKhac = VHIDbContext.KhachHang.Where(x=>x.ID_KhachHang != id).ToList();
            foreach (var kh in dskhKhac)
            {
                if (dto.CMND == kh.CMND)
                {
                    return BadRequest("CMND đã tồn tại!");
                }
                if (dto.SoTaiKhoan == kh.SoTaiKhoan)
                {
                    return BadRequest("Số Tài Khoản đã tồn tại!");
                }
                string sdt = kh.SoDienThoai.TrimEnd();
                if (dto.SoDienThoai == sdt)
                {
                    return BadRequest("Số Điện Thoại đã tồn tại!");
                }
            }

            UpdateKhachHangDomainByDTO(dto, khDomain);

            VHIDbContext.SaveChanges();
            KhachHangDTO kh_dto = CreateKHDTO(khDomain, khDomain.CongTyID_CongTy, khDomain.TaiKhoanID_TaiKhoan);
            return Ok(kh_dto);
        }

        [HttpPost]
        [Route("UpdateEmailKhachHang(id)")]
        public IActionResult UpdateEmailKhachHang(int id, string Email)
        {
            var khDomain = VHIDbContext.KhachHang.FirstOrDefault(x => x.ID_KhachHang == id);
            if (khDomain == null)
            {
                return NotFound("Không tìm thấy Khách hàng.");
            }
            //Lấy ra danh sách khách hàng khác không phải là khách hàng đang cần cập nhật thông tin
            var dskhKhac = VHIDbContext.KhachHang.Where(x => x.ID_KhachHang != id).ToList();
            foreach (var kh in dskhKhac)
            {
                if (Email == kh.Email)
                {
                    return BadRequest("Email đã tồn tại!");
                }
            }
            //Cập nhật lại Email cho Khách hàng
            khDomain.Email = Email;

            VHIDbContext.SaveChanges();
            KhachHangDTO kh_dto = CreateKHDTO(khDomain, khDomain.CongTyID_CongTy, khDomain.TaiKhoanID_TaiKhoan);
            return Ok(kh_dto);
        }

        [HttpPost]
        [Route("UpdateCongTyKhachHang(idkh,idct)")]
        public IActionResult UpdateKhachHang_CongTy(int idkh,int idct)
        {
            var khDomain = VHIDbContext.KhachHang.FirstOrDefault(x => x.ID_KhachHang == idkh);
            var ctDomain = VHIDbContext.CongTy.FirstOrDefault(x => x.ID_CongTy == idct);
            if (khDomain == null)
            {
                return NotFound("Khong ton tai khach hang!");
            }
            if (ctDomain == null)
            {
                return NotFound("Khong ton tai cong ty!");
            }

            khDomain.CongTy = ctDomain;

            VHIDbContext.SaveChanges();
            KhachHangDTO kh_dto = CreateKHDTO(khDomain, khDomain.CongTyID_CongTy, khDomain.TaiKhoanID_TaiKhoan);
            return Ok(kh_dto);
        }

        [HttpPost]
        [Route("XacThucKhachHang(idkh)")]
        public IActionResult UpdateKhachHang_XacThuc(int idkh)
        {
            var khDomain = VHIDbContext.KhachHang.FirstOrDefault(x => x.ID_KhachHang == idkh);
            if (khDomain == null)
            {
                return NotFound("Khong ton tai khach hang!");
            }

            khDomain.XacThuc = "Đã Xác Thực";

            VHIDbContext.SaveChanges();
            KhachHangDTO kh_dto = CreateKHDTO(khDomain, khDomain.CongTyID_CongTy, khDomain.TaiKhoanID_TaiKhoan);
            return Ok(kh_dto);
        }


        private static KhachHang CreateKHDomain(AddKhachHangDTO dto,  TaiKhoan? taiKhoan)
        {
            return new KhachHang()
            {
                HoTen = dto.HoTen,
                GioiTinh = dto.GioiTinh,
                QuocTich = dto.QuocTich,
                NgaySinh = dto.NgaySinh,
                ChieuCao = dto.ChieuCao,
                CanNang = dto.CanNang,
                SoNhaTenDuong = dto.SoNhaTenDuong,
                PhuongXa = dto.PhuongXa,
                QuanHuyen = dto.QuanHuyen,
                ThanhPho = dto.ThanhPho,
                Email = dto.Email,
                CMND = dto.CMND,
                NgheNghiep = dto.NgheNghiep,
                ChiTietCongViec = dto.ChiTietCongViec,
                ThuNhap = dto.ThuNhap,
                SoTaiKhoan = dto.SoTaiKhoan,
                NganHang = dto.NganHang,
                SoDienThoai = dto.SoDienThoai,
                TaiKhoan = taiKhoan,
                XacThuc = "Chưa Xác Thực"
            };
        }
        private static KhachHangDTO CreateKHDTO(KhachHang KhachHangDomain, int? idcty, int idtk)
        {
            if (idcty == 0)
            {
                return new KhachHangDTO()
                {
                    ID_KhachHang = KhachHangDomain.ID_KhachHang,
                    HoTen = KhachHangDomain.HoTen,
                    GioiTinh = KhachHangDomain.GioiTinh,
                    QuocTich = KhachHangDomain.QuocTich,
                    NgaySinh = KhachHangDomain.NgaySinh,
                    ChieuCao = KhachHangDomain.ChieuCao,
                    CanNang = KhachHangDomain.CanNang,
                    SoNhaTenDuong = KhachHangDomain.SoNhaTenDuong,
                    PhuongXa = KhachHangDomain.PhuongXa,
                    QuanHuyen = KhachHangDomain.QuanHuyen,
                    ThanhPho = KhachHangDomain.ThanhPho,
                    Email = KhachHangDomain.Email,
                    CMND = KhachHangDomain.CMND,
                    NgheNghiep = KhachHangDomain.NgheNghiep,
                    ChiTietCongViec = KhachHangDomain.ChiTietCongViec,
                    ThuNhap = KhachHangDomain.ThuNhap,
                    SoTaiKhoan = KhachHangDomain.SoTaiKhoan,
                    NganHang = KhachHangDomain.NganHang,
                    SoDienThoai = KhachHangDomain.SoDienThoai,
                    ID_TaiKhoan = KhachHangDomain.TaiKhoanID_TaiKhoan,
                    XacThuc = KhachHangDomain.XacThuc
                };
            }
            else
            {
                return new KhachHangDTO()
                {
                    ID_KhachHang = KhachHangDomain.ID_KhachHang,
                    HoTen = KhachHangDomain.HoTen,
                    GioiTinh = KhachHangDomain.GioiTinh,
                    QuocTich = KhachHangDomain.QuocTich,
                    NgaySinh = KhachHangDomain.NgaySinh,
                    ChieuCao = KhachHangDomain.ChieuCao,
                    CanNang = KhachHangDomain.CanNang,
                    SoNhaTenDuong = KhachHangDomain.SoNhaTenDuong,
                    PhuongXa = KhachHangDomain.PhuongXa,
                    QuanHuyen = KhachHangDomain.QuanHuyen,
                    ThanhPho = KhachHangDomain.ThanhPho,
                    Email = KhachHangDomain.Email,
                    CMND = KhachHangDomain.CMND,
                    NgheNghiep = KhachHangDomain.NgheNghiep,
                    ChiTietCongViec = KhachHangDomain.ChiTietCongViec,
                    ThuNhap = KhachHangDomain.ThuNhap,
                    SoTaiKhoan = KhachHangDomain.SoTaiKhoan,
                    NganHang = KhachHangDomain.NganHang,
                    SoDienThoai = KhachHangDomain.SoDienThoai,
                    ID_CongTy = KhachHangDomain.CongTyID_CongTy,
                    ID_TaiKhoan = KhachHangDomain.TaiKhoanID_TaiKhoan,
                    XacThuc = KhachHangDomain.XacThuc
                };
            }
        }
        private static void UpdateKhachHangDomainByDTO(UpdateTTCNKhachHangDTO dto, KhachHang? khDomain)
        {
            khDomain.HoTen = dto.HoTen;
            khDomain.GioiTinh = dto.GioiTinh;
            khDomain.QuocTich = dto.QuocTich;
            khDomain.GioiTinh = dto.GioiTinh;
            khDomain.QuocTich = dto.QuocTich;
            khDomain.NgaySinh = dto.NgaySinh;
            khDomain.ChieuCao = dto.ChieuCao;
            khDomain.CanNang = dto.CanNang;
            khDomain.SoNhaTenDuong = dto.SoNhaTenDuong;
            khDomain.PhuongXa = dto.PhuongXa;
            khDomain.QuanHuyen = dto.QuanHuyen;
            khDomain.ThanhPho = dto.ThanhPho;
            khDomain.CMND = dto.CMND;
            khDomain.NgheNghiep = dto.NgheNghiep;
            khDomain.ChiTietCongViec = dto.ChiTietCongViec;
            khDomain.ThuNhap = dto.ThuNhap;
            khDomain.SoTaiKhoan = dto.SoTaiKhoan;
            khDomain.NganHang = dto.NganHang;
            khDomain.SoDienThoai = dto.SoDienThoai;
        }
    }
}
