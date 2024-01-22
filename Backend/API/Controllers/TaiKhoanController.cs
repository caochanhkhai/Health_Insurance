using API.Data;
using API.Domain;
using API.DTOs;
using API.Helper;
using API.MiddleWare;
using API.Service;
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
        private readonly IEmailService emailService;

        public TaiKhoanController(VHIDbContext VHIDbContext, IEmailService emailService)
        {
            this.VHIDbContext = VHIDbContext;
            this.emailService = emailService;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            var dstk = VHIDbContext.TaiKhoan.ToList();
            if(dstk.Count()==0 || dstk == null)
            {
                return NotFound("Không tồn tại tài khoản");
            }
            List<TaiKhoanDTO> dstkDTO = new List<TaiKhoanDTO>();
            foreach (var tk in dstk)
            {
                TaiKhoanDTO tk_dto = CreateTaiKhoanDTO(tk);
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
                return NotFound("Không tìm thấy tài khoản");
            }
            TaiKhoanDTO tk_dto = CreateTaiKhoanDTO(tk);
            return Ok(tk_dto);
        }

        [HttpPost]
        [Route("DangKi")]
        public IActionResult CreateTaiKhoan([FromBody] AddTaiKhoanDTO dto)
        {
            var dstk = VHIDbContext.TaiKhoan.ToList();
            foreach(var tk in dstk)
            {
                string tdn = tk.TenDangNhap.TrimEnd();
                if (dto.TenDangNhap == tdn)
                {
                    return BadRequest("Tên Đăng Nhập đã tồn tại!");
                }
            }
            var TaiKhoanDomain = new TaiKhoan()
            {
                TenDangNhap = dto.TenDangNhap,
                MatKhau = HashPassword(dto.MatKhau),
                LoaiTaiKhoan = "KH",
                TinhTrang = "Hoạt động"
            };
            VHIDbContext.TaiKhoan.Add(TaiKhoanDomain);
            VHIDbContext.SaveChanges();

            TaiKhoanDTO TaiKhoan_dto = CreateTaiKhoanDTO(TaiKhoanDomain);
            return Ok(TaiKhoan_dto);
        }

        [HttpPost]
        [Route("ThemTaiKhoanByADMIN")]
        public IActionResult CreateTaiKhoanByADMIN([FromBody] AddTaiKhoanByAdminDTO dto)
        {
            //Kiểm tra loại tài khoản
            if(dto.LoaiTaiKhoan != "ADMIN" && dto.LoaiTaiKhoan != "NVTC" && dto.LoaiTaiKhoan != "NV")
            {
                return BadRequest("Loại tài khoản không hợp lệ");
            }
            //Kiểm tra tình trạng hoạt động
            if (dto.TinhTrang != "Đã Khóa" && dto.TinhTrang != "Hoạt Động")
            {
                return BadRequest("Tình trạng hoạt động không hợp lệ");
            }
            var dstk = VHIDbContext.TaiKhoan.ToList();
            foreach (var tk in dstk)
            {
                string tdn = tk.TenDangNhap.TrimEnd();
                if (dto.TenDangNhap == tdn)
                {
                    return BadRequest("Tên Đăng Nhập đã tồn tại!");
                }
            }

            var TaiKhoanDomain = new TaiKhoan()
            {
                TenDangNhap = dto.TenDangNhap,
                MatKhau = HashPassword(dto.MatKhau),
                LoaiTaiKhoan = dto.LoaiTaiKhoan,
                TinhTrang = dto.TinhTrang
            };
            VHIDbContext.TaiKhoan.Add(TaiKhoanDomain);
            VHIDbContext.SaveChanges();

            TaiKhoanDTO TaiKhoan_dto = CreateTaiKhoanDTO(TaiKhoanDomain);
            return CreatedAtAction(nameof(GetById), new { id = TaiKhoan_dto.ID_TaiKhoan }, TaiKhoan_dto);
        }

        [HttpGet]
        [Route("DangNhap(TenDangNhap:string,MatKhau:string)")]
        public IActionResult DangNhap(string tenDN,string MK)
        {
            var tk = VHIDbContext.TaiKhoan.FirstOrDefault(x => x.TenDangNhap == tenDN && x.MatKhau == HashPassword(MK));
            if (tk == null)
            {
                return NotFound("Không tìm thấy tài khoản.");
            }
            var jwtService = new JwtService("vhihealthinsurance");
            var accessToken = jwtService.GenerateToken(tk.ID_TaiKhoan.ToString(), tk.TenDangNhap, 120);

            TaiKhoanDTO tk_dto = CreateTaiKhoanDTO(tk);

            var response = new
            {
                AccessToken = accessToken,
                TaiKhoan = tk_dto
            };
            return Ok(response);
        }

        [HttpPost("ChinhSuaTinhTrangHoatDong")]
        public IActionResult ChinhSuaTinhTrangHoatDong([FromBody] UpdateTinhTrangTaiKhoanDTO dto)
        {
            var tk = VHIDbContext.TaiKhoan.FirstOrDefault(x => x.ID_TaiKhoan == dto.id);
            if (tk == null)
            {
                return NotFound("Không tìm thấy tài khoản.");
            }
            //Kiểm tra tình trạng hoạt động
            if (dto.tinhTrang != "Đã Khóa" && dto.tinhTrang != "Hoạt Động")
            {
                return BadRequest("Tình trạng hoạt động không hợp lệ");
            }
            // Cập nhật và lưu vào cơ sở dữ liệu
            tk.TinhTrang = dto.tinhTrang;
            VHIDbContext.SaveChanges();

            TaiKhoanDTO tk_dto = CreateTaiKhoanDTO(tk);

            return Ok(tk_dto);
        }

        [HttpPost]
        [Route("DoiMatKhau")]
        public IActionResult DoiMatKhau([FromBody] DoiMatKhauDTO dto)
        {
            var tk = VHIDbContext.TaiKhoan.FirstOrDefault(x => x.ID_TaiKhoan == dto.ID_TaiKhoan);
            if (tk == null)
            {
                return NotFound("Không tìm thấy tài khoản.");
            }
            var kh = VHIDbContext.KhachHang.FirstOrDefault(x => x.TaiKhoan == tk);
            if(kh == null)
            {
                return NotFound("Không tìm thấy khách hàng với tài khoản tương ứng.");
            }
            if(HashPassword(dto.MatKhauCu) != tk.MatKhau)
            {
                return BadRequest("Mật khẩu hiện tại không đúng.");
            }
            //Gửi email cho khách hàng
            var jwtService = new JwtService("vhihealthinsurance");
            var accessToken = jwtService.GenerateTokenChangePassword(tk.ID_TaiKhoan.ToString(), dto.MatKhauMoi, 10);

            Mailrequest mailrequest = new Mailrequest()
            {
                ToEmail = kh.Email,
                Subject = "Đổi mật khẩu cho tài khoản Bảo hiểm sức khỏe VHI",
                Body = $"<div>Để ĐỔI MẬT KHẨU tài khoản của bạn trên trang web Bảo hiểm sức khỏe VHI, vui lòng click vào đường dẫn sau đây: <a href=\"https://localhost:8081/VerifyEmail?accessToken={accessToken}\">Link</a></div>"
            };
            emailService.SendEmailAsync(mailrequest);
            return Ok();
        }

        [HttpPost("UpDateMatKhau")]
        public IActionResult UpDateMatKhau(int idtk, string MatKhauMoi)
        {
            var tk = VHIDbContext.TaiKhoan.FirstOrDefault(x => x.ID_TaiKhoan == idtk);
            if (tk == null)
            {
                return NotFound("Không tìm thấy tài khoản.");
            }

            // Cập nhật và lưu vào cơ sở dữ liệu
            tk.MatKhau = HashPassword(MatKhauMoi);
            VHIDbContext.SaveChanges();

            TaiKhoanDTO tk_dto = CreateTaiKhoanDTO(tk);

            return Ok("Mật Khẩu đã được cập nhật.");
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult XoaTaiKhoan([FromRoute] int id)
        {
            var tk = VHIDbContext.TaiKhoan.FirstOrDefault(x => x.ID_TaiKhoan == id);
            if (tk == null)
            {
                return NotFound("Không tồn tại tài khoản.");
            }
            VHIDbContext.TaiKhoan.Remove(tk);
            VHIDbContext.SaveChanges();

            TaiKhoanDTO tk_dto = CreateTaiKhoanDTO(tk);

            return Ok(tk_dto);
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

        private static TaiKhoanDTO CreateTaiKhoanDTO(TaiKhoan? tk)
        {
            var tk_dto = new TaiKhoanDTO();
            tk_dto.ID_TaiKhoan = tk.ID_TaiKhoan;
            tk_dto.TenDangNhap = tk.TenDangNhap;
            tk_dto.MatKhau = "";/*tk.MatKhau;*/
            tk_dto.LoaiTaiKhoan = tk.LoaiTaiKhoan;
            tk_dto.TinhTrang = tk.TinhTrang;
            return tk_dto;
        }

    }
}
