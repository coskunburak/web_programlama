using Microsoft.AspNetCore.Mvc;
using web_programlama.Models;
using System.Linq;

namespace web_programlama.Controllers
{
    public class CalisanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CalisanController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var calisanlar = _context.Calisanlar.ToList();
            return View(calisanlar);
        }

        public IActionResult Details(int id)
        {
            var calisan = _context.Calisanlar.Find(id);
            if (calisan == null)
                return NotFound();
            return View(calisan);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Calisan calisan)
        {
            if (ModelState.IsValid)
            {
                _context.Calisanlar.Add(calisan);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(calisan);
        }

        public IActionResult Edit(int id)
        {
            var calisan = _context.Calisanlar.Find(id);
            if (calisan == null)
                return NotFound();
            return View(calisan);
        }

        [HttpPost]
        public IActionResult Edit(Calisan calisan)
        {
            if (ModelState.IsValid)
            {
                _context.Calisanlar.Update(calisan);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(calisan);
        }

        public IActionResult Delete(int id)
        {
            var calisan = _context.Calisanlar.Find(id);
            if (calisan == null)
                return NotFound();
            _context.Calisanlar.Remove(calisan);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
