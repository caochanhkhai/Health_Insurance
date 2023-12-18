using API.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoiBaoHiemController : ControllerBase
    {
        private readonly VHIDbContext VHIDbContext;

        public GoiBaoHiemController(VHIDbContext VHIDbContext)
        {
            this.VHIDbContext = VHIDbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var goiBH = VHIDbContext.GoiBaoHiem.ToList();
            return Ok(goiBH);
        }

    }
}
