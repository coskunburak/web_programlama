﻿using Microsoft.AspNetCore.Mvc;
using web_programlama.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

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

            ViewBag.Calisanlar = _context.Calisanlar.Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Ad});
            ViewBag.Islemler = _context.Islemler.Select(r => new SelectListItem { Value = r.Id.ToString(), Text = r.Ad});
            ViewBag.KullaniciIdler = _context.Users.Select(r => new SelectListItem { Value = r.Id, Text = r.UserName});

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
