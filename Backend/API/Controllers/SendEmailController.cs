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
    }
}
