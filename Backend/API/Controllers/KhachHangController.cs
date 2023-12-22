using API.Data;
using API.Domain;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        private readonly VHIDbContext VHIDbContext;

        public KhachHangController(VHIDbContext VHIDbContext)
        {
            this.VHIDbContext = VHIDbContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var dskh = VHIDbContext.KhachHang.ToList();
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
                return NotFound();
            }
            KhachHangDTO kh_dto = CreateKHDTO(kh, kh.CongTyID_CongTy,kh.TaiKhoanID_TaiKhoan);
            return Ok(kh_dto);
        }

        [HttpPost]
        [Route("ThemKhachHang")]
        public IActionResult CreateKhachHang([FromBody] KhachHangDTO dto, int idtk)
        {
            //CongTy ID = 1 là  đối tượng có giá trị ảo
            var congTy = VHIDbContext.CongTy.FirstOrDefault(b => b.ID_CongTy == 1);
            var taiKhoan = VHIDbContext.TaiKhoan.FirstOrDefault(b => b.ID_TaiKhoan == idtk);
            KhachHang KhachHangDomain = CreateKHDomain(dto, congTy, taiKhoan);
            VHIDbContext.KhachHang.Add(KhachHangDomain);
            VHIDbContext.SaveChanges();
            KhachHangDTO kh_dto = CreateKHDTO(KhachHangDomain,0, taiKhoan.ID_TaiKhoan);
            return Ok(kh_dto);
        }

        [HttpPut]
        [Route("UpdateKhachHang(id)")]
        public IActionResult UpdateKhachHang(int id, [FromBody] KhachHangDTO dto)
        {
            var khDomain = VHIDbContext.KhachHang.FirstOrDefault(x => x.ID_KhachHang == id);
            if (khDomain == null)
            {
                return NotFound();
            }
            UpdateKhachHangDomainByDTO(dto, khDomain);

            VHIDbContext.SaveChanges();
            KhachHangDTO kh_dto = CreateKHDTO(khDomain, khDomain.CongTyID_CongTy, khDomain.TaiKhoanID_TaiKhoan);
            return Ok(kh_dto);
        }

        private static KhachHang CreateKHDomain(KhachHangDTO dto, CongTy? congTy, TaiKhoan? taiKhoan)
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
                CongTy = congTy,
                TaiKhoan = taiKhoan
            };
        }
        private static KhachHangDTO CreateKHDTO(KhachHang KhachHangDomain, int idcty, int idtk)
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
                    ID_CongTy = KhachHangDomain.CongTy.ID_CongTy,
                    ID_TaiKhoan = KhachHangDomain.TaiKhoan.ID_TaiKhoan
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
                    ID_TaiKhoan = KhachHangDomain.TaiKhoanID_TaiKhoan
                };
            }
        }
        private static void UpdateKhachHangDomainByDTO(KhachHangDTO dto, KhachHang? khDomain)
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
            khDomain.Email = dto.Email;
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
