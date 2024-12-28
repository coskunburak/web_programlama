using Microsoft.AspNetCore.Mvc;
using web_programlama.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            // Çalışanlar
            ViewBag.Calisanlar = _context.Calisanlar
                                        .Select(c => new SelectListItem
                                        {
                                            Value = c.Id.ToString(),
                                            Text = c.Ad
                                        })
                                        .ToList();

            // İşlemler (Hizmetler)
            ViewBag.Islemler = _context.Islemler
                                       .Select(i => new SelectListItem
                                       {
                                           Value = i.Id.ToString(),
                                           Text = i.Ad
                                       })
                                       .ToList();

            // Müşteriler
            ViewBag.Kullanicilar = _context.Users
                                           .Select(u => new SelectListItem
                                           {
                                               Value = u.Id.ToString(),
                                               Text = $"{u.UserName} ({u.Email})"
                                           })
                                           .ToList();

            return View();
        }


        [HttpPost]
        public IActionResult Create(Randevu randevu)
        {
            if (ModelState.IsValid)
            {
                randevu.RandevuZamani = DateTime.SpecifyKind(randevu.RandevuZamani, DateTimeKind.Utc);
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
