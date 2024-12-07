using Microsoft.AspNetCore.Mvc;
using web_programlama.Models;
using System.Linq;

namespace web_programlama.Controllers
{
    public class RandevuController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RandevuController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var randevular = _context.Randevular.ToList();
            return View(randevular);
        }

        public IActionResult Details(int id)
        {
            var randevu = _context.Randevular.Find(id);
            if (randevu == null)
                return NotFound();
            return View(randevu);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Randevu randevu)
        {
            if (ModelState.IsValid)
            {
                _context.Randevular.Add(randevu);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(randevu);
        }

        public IActionResult Delete(int id)
        {
            var randevu = _context.Randevular.Find(id);
            if (randevu == null)
                return NotFound();
            _context.Randevular.Remove(randevu);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
