using API.Data;
using API.Domain;
using API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CongTyController : ControllerBase
    {
        private readonly VHIDbContext VHIDbContext;

        public CongTyController(VHIDbContext VHIDbContext)
        {
            this.VHIDbContext = VHIDbContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var dsct = VHIDbContext.CongTy.ToList();
            if (dsct.Count() == 0 || dsct == null)
            {
                return NotFound("Không tìm thấy công ty nào.");
            }
            List<CongTyDTO> dsctDTO = new List<CongTyDTO>();
            foreach (var cty in dsct)
            {
               CongTyDTO ctdto = CreateCongTyDTO(cty);
               dsctDTO.Add(ctdto);
            }
            return Ok(dsctDTO);
        }

        [HttpGet]
        [Route("id:int")]
        public IActionResult GetById(int id)
        {
            var ct = VHIDbContext.CongTy.FirstOrDefault(x => x.ID_CongTy == id);
            if (ct == null)
            {
                return NotFound();
            }
            var ct_dto = CreateCongTyDTO(ct);
            return Ok(ct_dto);
        }

        [HttpPost]
        [Route("ThemCongTy")]
        public IActionResult CreateCongTy([FromBody] AddCongTyDTO dto)
        {
            
            CongTy ctDomain = CreateCongTy_Domain(dto);

            VHIDbContext.CongTy.Add(ctDomain);
            VHIDbContext.SaveChanges();

            CongTyDTO ctDTO = CreateCongTyDTO(ctDomain);

            return Ok(ctDTO);
        }

        [HttpPost]
        [Route("ThemCongTy(New)ChoKH")]
        public IActionResult CreateNewCongTy(int idkh, [FromBody] CongTyDTO dto)
        {
            CongTy CongTyDomain = CreateCongTyDomain(dto);
            VHIDbContext.CongTy.Add(CongTyDomain);
            VHIDbContext.SaveChanges();
            CongTyDTO cty_dto = CreateCongTyDTO(CongTyDomain);

            var khDomain = VHIDbContext.KhachHang.FirstOrDefault(x => x.ID_KhachHang == idkh);
            if (khDomain == null)
            {
                return NotFound();
            }

            //Cập nhật lại công ty trong bảng khách hàng
            khDomain.CongTy = CongTyDomain;
            VHIDbContext.SaveChanges();
            KhachHangDTO kh_dto = CreateKHDTO(khDomain, khDomain.CongTy.ID_CongTy, khDomain.TaiKhoanID_TaiKhoan);
            var result = new Tuple<CongTyDTO, KhachHangDTO>(cty_dto, kh_dto);
            return Ok(result);
        }

        [HttpPost("ChinhSuaCongTy(id: int)")]
        public IActionResult ChinhSuaCongTY([FromBody] CongTyDTO dto)
        {
            var ctDomain = VHIDbContext.CongTy.FirstOrDefault(x => x.ID_CongTy == dto.ID_CongTy);
            if (ctDomain == null)
            {
                return NotFound("Không tìm thấy công ty.");
            }

            // Cập nhật và lưu vào cơ sở dữ liệu
            ctDomain.TenCongTy = dto.TenCongTy;
            ctDomain.SoNhaTenDuong= dto.SoNhaTenDuong;
            ctDomain.PhuongXa=dto.PhuongXa;
            ctDomain.QuanHuyen= dto.QuanHuyen;
            ctDomain.ThanhPho= dto.ThanhPho;
            ctDomain.DienThoai= dto.DienThoai;
            ctDomain.Email= dto.Email;
            VHIDbContext.SaveChanges();

            CongTyDTO ctDTO = CreateCongTyDTO(ctDomain);

            return Ok(ctDTO);
        }


        private static CongTyDTO CreateCongTyDTO(CongTy CongTyDomain)
        {
            return new CongTyDTO()
            {
                ID_CongTy = CongTyDomain.ID_CongTy,
                TenCongTy = CongTyDomain.TenCongTy,
                SoNhaTenDuong = CongTyDomain.SoNhaTenDuong,
                PhuongXa = CongTyDomain.PhuongXa,
                QuanHuyen = CongTyDomain.QuanHuyen,
                ThanhPho = CongTyDomain.ThanhPho,
                DienThoai = CongTyDomain.DienThoai,
                Email = CongTyDomain.Email
            };
        }

        private static CongTy CreateCongTy_Domain(AddCongTyDTO dto)
        {
            return new CongTy()
            {
                TenCongTy = dto.TenCongTy,
                SoNhaTenDuong = dto.SoNhaTenDuong,
                PhuongXa = dto.PhuongXa,
                QuanHuyen = dto.QuanHuyen,
                ThanhPho = dto.ThanhPho,
                DienThoai = dto.DienThoai,
                Email = dto.Email
            };
        }


        private static CongTy CreateCongTyDomain(CongTyDTO dto)
        {
            return new CongTy()
            {
                TenCongTy = dto.TenCongTy,
                SoNhaTenDuong = dto.SoNhaTenDuong,
                PhuongXa = dto.PhuongXa,
                QuanHuyen = dto.QuanHuyen,
                ThanhPho = dto.ThanhPho,
                DienThoai = dto.DienThoai,
                Email = dto.Email
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
    }
}
