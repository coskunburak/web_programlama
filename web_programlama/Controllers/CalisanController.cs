using Microsoft.AspNetCore.Mvc;
using web_programlama.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            List<SelectListItem> categorySelectList = _context.Salonlar.ToList()
                .Select(salon => new SelectListItem
                {
                    Value = salon.Id.ToString(),
                    Text = salon.Ad
                })
                .ToList();

            ViewBag.Salonlar = categorySelectList;
            return View();
        }

        [HttpPost]
        public IActionResult Create(CalisanDTO calisan)
        {
            if (ModelState.IsValid)
            {
                Calisan yeni = new Calisan()
                {
                    Ad = calisan.Ad,
                    SalonId = calisan.SalonId,
                    UygunlukSaatleri = calisan.UygunlukSaatleri,
                    UzmanlikAlani = calisan.UzmanlikAlani
                };

                _context.Calisanlar.Add(yeni);
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
