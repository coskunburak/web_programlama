using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using web_programlama.Models;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace web_programlama.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var salonSayisi = _context.Salonlar.Count();
            var calisanSayisi = _context.Calisanlar.Count();
            var randevuSayisi = _context.Randevular.Count();

            ViewData["SalonSayisi"] = salonSayisi;
            ViewData["CalisanSayisi"] = calisanSayisi;
            ViewData["RandevuSayisi"] = randevuSayisi;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
