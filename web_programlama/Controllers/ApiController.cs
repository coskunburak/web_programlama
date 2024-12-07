using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_programlama.Models;

namespace web_programlama.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Api
        [HttpGet]
        public IActionResult GetRandevular()
        {
            var randevular = _context.Randevular
                                     .Include(r => r.Calisan)
                                     .Include(r => r.Islem)
                                     .ToList();
            return Ok(randevular);
        }

        // GET: api/Api/5
        [HttpGet("{id}")]
        public IActionResult GetRandevu(int id)
        {
            var randevu = _context.Randevular
                                  .Include(r => r.Calisan)
                                  .Include(r => r.Islem)
                                  .FirstOrDefault(r => r.Id == id);

            if (randevu == null)
                return NotFound();

            return Ok(randevu);
        }

        // POST: api/Api
        [HttpPost]
        public IActionResult CreateRandevu([FromBody] Randevu randevu)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Randevular.Add(randevu);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetRandevu), new { id = randevu.Id }, randevu);
        }

        // PUT: api/Api/5
        [HttpPut("{id}")]
        public IActionResult UpdateRandevu(int id, [FromBody] Randevu randevu)
        {
            if (id != randevu.Id)
                return BadRequest();

            _context.Entry(randevu).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Randevular.Any(r => r.Id == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        // DELETE: api/Api/5
        [HttpDelete("{id}")]
        public IActionResult DeleteRandevu(int id)
        {
            var randevu = _context.Randevular.Find(id);
            if (randevu == null)
                return NotFound();

            _context.Randevular.Remove(randevu);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
