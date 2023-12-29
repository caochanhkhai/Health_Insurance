using API.Data;
using API.Domain;
using API.DTOs;
using API.MiddleWare;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanController: ControllerBase
    {
        private readonly VHIDbContext VHIDbContext;

        public TaiKhoanController(VHIDbContext VHIDbContext)
        {
            this.VHIDbContext = VHIDbContext;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var dstk = VHIDbContext.TaiKhoan.ToList();
            List<TaiKhoanDTO> dstkDTO = new List<TaiKhoanDTO>();
            foreach (var tk in dstk)
            {
                TaiKhoanDTO tk_dto = new TaiKhoanDTO()
                {
                    ID_TaiKhoan = tk.ID_TaiKhoan,
                    TenDangNhap = tk.TenDangNhap,
                    MatKhau = "",
                    LoaiTaiKhoan = tk.LoaiTaiKhoan,
                    TinhTrang = tk.TinhTrang
                };
                dstkDTO.Add(tk_dto);
            }
            return Ok(dstkDTO);
        }

        [HttpGet]
        [Route("id")]
        public IActionResult GetById(int id)
        {
            var tk = VHIDbContext.TaiKhoan.FirstOrDefault(x => x.ID_TaiKhoan == id);
            if (tk == null)
            {
                return NotFound();
            }
            var tk_dto = new TaiKhoanDTO();
            tk_dto.ID_TaiKhoan = tk.ID_TaiKhoan;
            tk_dto.TenDangNhap = tk.TenDangNhap;
            tk_dto.MatKhau = tk.MatKhau;
            tk_dto.LoaiTaiKhoan = tk.LoaiTaiKhoan;
            tk_dto.TinhTrang = tk.TinhTrang;
            return Ok(tk_dto);
        }

        [HttpPost]
        [Route("DangKi")]
        public IActionResult CreateTaiKhoan([FromBody] TaiKhoanDTO dto)
        {

            var TaiKhoanDomain = new TaiKhoan()
            {
                TenDangNhap = dto.TenDangNhap,
                MatKhau = HashPassword(dto.MatKhau),
                LoaiTaiKhoan = "KH",
                TinhTrang = "Hoạt động"
            };
            VHIDbContext.TaiKhoan.Add(TaiKhoanDomain);
            VHIDbContext.SaveChanges();

            var TaiKhoan_dto = new TaiKhoanDTO()
            {
                ID_TaiKhoan = TaiKhoanDomain.ID_TaiKhoan,
                TenDangNhap = TaiKhoanDomain.TenDangNhap,
                MatKhau = TaiKhoanDomain.MatKhau,
                LoaiTaiKhoan = TaiKhoanDomain.LoaiTaiKhoan,
                TinhTrang = TaiKhoanDomain.TinhTrang
            };
            return CreatedAtAction(nameof(GetById), new { id = TaiKhoan_dto.ID_TaiKhoan }, TaiKhoan_dto);
        }
        
        [HttpGet]
        [Route("DangNhap(TenDangNhap:string,MatKhau:string)")]
        public IActionResult DangNhap(string tenDN,string MK)
        {
            var tk = VHIDbContext.TaiKhoan.FirstOrDefault(x => x.TenDangNhap == tenDN && x.MatKhau == HashPassword(MK));
            if (tk == null)
            {
                return NotFound();
            }
            var jwtService = new JwtService("vhihealthinsurance");
            var accessToken = jwtService.GenerateToken(tk.ID_TaiKhoan.ToString(), tk.TenDangNhap, 120);

            var tk_dto = new TaiKhoanDTO();
            tk_dto.ID_TaiKhoan = tk.ID_TaiKhoan;
            tk_dto.TenDangNhap = tk.TenDangNhap;
            tk_dto.MatKhau = "";
            tk_dto.LoaiTaiKhoan = tk.LoaiTaiKhoan;
            tk_dto.TinhTrang = tk.TinhTrang;

            var response = new
            {
                AccessToken = accessToken,
                TaiKhoan = tk_dto
            };
            return Ok(response);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                // Chuyển đổi mật khẩu thành mảng byte
                byte[] bytes = Encoding.UTF8.GetBytes(password);

                // Băm mật khẩu
                byte[] hashBytes = sha256.ComputeHash(bytes);

                // Chuyển đổi mảng byte thành chuỗi hexa
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
