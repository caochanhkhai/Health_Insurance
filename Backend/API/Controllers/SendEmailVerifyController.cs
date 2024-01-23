using API.Data;
using API.Helper;
using API.MiddleWare;
using API.Service;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendEmailVerifyController : ControllerBase
    {
        private readonly IEmailService emailService;
        public SendEmailVerifyController(IEmailService service)
        {
            this.emailService = service;
        }
        [HttpPost("SendMail")]
        public async Task<IActionResult> SendMail(string email,int idkh)
        {
            try
            {
                var jwtService = new JwtService("vhihealthinsurance");
                var accessToken = jwtService.GenerateToken(idkh.ToString(),"", 10);
                Mailrequest mailrequest = new Mailrequest()
                {
                    ToEmail = email,
                    Subject = "Xác thực tài khoản Bảo hiểm sức khỏe VHI",
                    Body = $"<div>Để xác thực tài khoản của bạn trên trang web Bảo hiểm sức khỏe VHI, vui lòng click vào đường dẫn sau đây: <a href=\"http://localhost:8081/VerifyEmail?accessToken={accessToken}\">Link</a></div>"
                };
                await emailService.SendEmailAsync(mailrequest);
            }
            catch(Exception ex)
            {
                throw;
            }
            return Ok();
        }
    }
}
