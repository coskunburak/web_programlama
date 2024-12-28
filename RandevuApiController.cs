using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourNamespace.Models;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class RandevuApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RandevuApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/RandevuApi
        [HttpGet]
        public ActionResult<IEnumerable<Randevu>> GetRandevular()
        {
            return Ok(_context.Randevular.ToList());
        }

        // GET: api/RandevuApi/5
        [HttpGet("{id}")]
        public ActionResult<Randevu> GetRandevu(int id)
        {
            var randevu = _context.Randevular.Find(id);

            if (randevu == null)
            {
                return NotFound(new { message = "Randevu bulunamadı" });
            }

            return Ok(randevu);
        }

        // POST: api/RandevuApi
        [HttpPost]
        public ActionResult<Randevu> PostRandevu([FromBody] Randevu randevu)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Randevular.Add(randevu);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetRandevu), new { id = randevu.Id }, randevu);
        }

        // PUT: api/RandevuApi/5
        [HttpPut("{id}")]
        public IActionResult PutRandevu(int id, [FromBody] Randevu randevu)
        {
            if (id != randevu.Id)
            {
                return BadRequest(new { message = "Randevu ID uyumsuzluğu" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingRandevu = _context.Randevular.Find(id);
            if (existingRandevu == null)
            {
                return NotFound(new { message = "Randevu bulunamadı" });
            }

            existingRandevu.TarihSaat = randevu.TarihSaat;
            existingRandevu.MusteriAdSoyad = randevu.MusteriAdSoyad;
            existingRandevu.Hizmet = randevu.Hizmet;
            existingRandevu.CalisanId = randevu.CalisanId;
            existingRandevu.SalonId = randevu.SalonId;

            _context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/RandevuApi/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRandevu(int id)
        {
            var randevu = _context.Randevular.Find(id);
            if (randevu == null)
            {
                return NotFound(new { message = "Randevu bulunamadı" });
            }

            _context.Randevular.Remove(randevu);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
