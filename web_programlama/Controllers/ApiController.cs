/*using Microsoft.AspNetCore.Mvc;
using ProjeAdi.Models;
using System.Linq;

namespace ProjeAdi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Örnek olarak tüm salonları listeleyen bir endpoint
        [HttpGet("salonlar")]
        public IActionResult GetSalonlar()
        {
            var salonlar = _context.Salonlar.ToList();
            return Ok(salonlar);
        }
    }
}
*/  