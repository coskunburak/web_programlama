using Microsoft.AspNetCore.Mvc;
using web_programlama.Models;
using System.Linq;

namespace web_programlama.Controllers
{
    public class IslemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IslemController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var islemler = _context.Islemler.ToList();
            return View(islemler);
        }

        public IActionResult Details(int id)
        {
            var islem = _context.Islemler.Find(id);
            if (islem == null)
                return NotFound();
            return View(islem);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Islem islem)
        {
            if (ModelState.IsValid)
            {
                _context.Islemler.Add(islem);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(islem);
        }

        public IActionResult Edit(int id)
        {
            var islem = _context.Islemler.Find(id);
            if (islem == null)
                return NotFound();
            return View(islem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Islem islem)
        {
            if (ModelState.IsValid)
            {
                _context.Islemler.Update(islem);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(islem);
        }

        public IActionResult Delete(int id)
        {
            var islem = _context.Islemler.Find(id);
            if (islem == null)
                return NotFound();
            return View(islem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var islem = _context.Islemler.Find(id);
            if (islem == null)
                return NotFound();

            _context.Islemler.Remove(islem);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
