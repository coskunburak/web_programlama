using Microsoft.AspNetCore.Mvc;
using web_programlama.Models;
using System.Linq;

namespace web_programlama.Controllers
{
    public class SalonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SalonController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var salonlar = _context.Salonlar.ToList();
            return View(salonlar);
        }

        public IActionResult Details(int id)
        {
            var salon = _context.Salonlar.Find(id);
            if (salon == null)
                return NotFound();
            return View(salon);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Salon salon)
        {
            if (ModelState.IsValid)
            {
                _context.Salonlar.Add(salon);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(salon);
        }

        public IActionResult Edit(int id)
        {
            var salon = _context.Salonlar.Find(id);
            if (salon == null)
                return NotFound();
            return View(salon);
        }

        [HttpPost]
        public IActionResult Edit(Salon salon)
        {
            if (ModelState.IsValid)
            {
                _context.Salonlar.Update(salon);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(salon);
        }

        public IActionResult Delete(int id)
        {
            var salon = _context.Salonlar.Find(id);
            if (salon == null)
                return NotFound();
            _context.Salonlar.Remove(salon);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
