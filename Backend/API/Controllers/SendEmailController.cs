using API.Data;
using API.Helper;
using API.MiddleWare;
using API.Service;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendEmailController : ControllerBase
    {
        private readonly IEmailService emailService;
        private readonly VHIDbContext VHIDbContext;
        public SendEmailController(IEmailService service, VHIDbContext VHIDbContext)
        {
            this.emailService = service;
            this.VHIDbContext = VHIDbContext;
        }
        [HttpPost("SendMail")]
        public async Task<IActionResult> SendMail(int idKH, string chuDe, string noiDung)
        {
            var kh = VHIDbContext.KhachHang.FirstOrDefault(x=>x.ID_KhachHang == idKH);
            string email = kh.Email;
            try
            {
                var jwtService = new JwtService("vhihealthinsurance");
                Mailrequest mailrequest = new Mailrequest()
                {
                    ToEmail = email,
                    Subject = chuDe,
                    Body = $"<div>{noiDung}</div>"
                };
                await emailService.SendEmailAsync(mailrequest);
            }
            catch (Exception ex)
            {
                throw;
            }
            return Ok();
        }

        [HttpPost("SendNewEMailToChange")]
        public async Task<IActionResult> SendNewEMail(int idKH, string Email)
        {
            var kh = VHIDbContext.KhachHang.FirstOrDefault(x => x.ID_KhachHang == idKH);
            try
            {
                var jwtService = new JwtService("vhihealthinsurance");
                var accessToken = jwtService.GenerateTokenToChangeEmail(idKH.ToString(), Email, 10);
                Mailrequest mailrequest = new Mailrequest()
                {
                    ToEmail = Email,
                    Subject = "Thay Đổi Email Của Bạn Trên Trang Web Bảo hiểm sức khỏe VHI",
                    Body = $"<div>Để thay đổi Email của bạn trên trang web Bảo hiểm sức khỏe VHI, vui lòng click vào đường dẫn sau đây: <a href=\"https://localhost:8081/VerifyEmail?accessToken={accessToken}\">Link</a></div>"
                };
                await emailService.SendEmailAsync(mailrequest);
            }
            catch (Exception ex)
            {
                throw;
            }
            return Ok();
        }
    }
}
