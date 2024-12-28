using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class YapayZekaController : Controller
{
    private readonly HttpClient _httpClient;

    public YapayZekaController()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("Api-Key", "3b3f6e5d-12b6-40a3-a7e0-a559c5009145"); 
    }

    public IActionResult FotoYukle()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> FotoYukle(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            ModelState.AddModelError("", "Lütfen bir fotoğraf seçin.");
            return View();
        }

        using (var stream = new MemoryStream())
        {
            await file.CopyToAsync(stream);
            var imageBytes = stream.ToArray();
            var base64Image = Convert.ToBase64String(imageBytes);

            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(base64Image), "image");

            var response = await _httpClient.PostAsync("https://api.deepai.org/api/neuraltalk", formData);

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Yapay zeka API'sine bağlanılamadı.");
                return View();
            }

            var result = await response.Content.ReadAsStringAsync();
            TempData["Sonuc"] = result; 
            return RedirectToAction("Sonuc");
        }
    }


    public IActionResult Sonuc()
    {
        var sonuc = TempData["Sonuc"];
        if (sonuc != null)
        {
            ViewBag.Sonuc = sonuc.ToString();
        }
        else
        {
            ViewBag.Sonuc = "Henüz bir sonuç alınamadı. Lütfen bir fotoğraf yükleyip tekrar deneyin.";
        }
        return View();
    }


}
