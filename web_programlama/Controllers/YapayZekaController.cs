using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class YapayZekaController : Controller
{
    private readonly HttpClient _httpClient;

    public YapayZekaController()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer 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"); 
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

            var request = new
            {
                messages = new[]
                {
                    new
                    {
                        role = "user",
                        content = new object[]
                        {
                            new
                            {
                                type = "text",
                                text = "Bana bir saç kesimi önerisi verebilir misin?"
                            },
                            new
                            {
                                type = "image_url",
                                image_url = new
                                {
                                    url = $"data:image/jpeg;base64,{base64Image}",
                                }
                            }
                        }
                    }
                },
                model = "llama-3.2-11b-vision-preview",
                temperature = 1,
                max_tokens = 1024,
                top_p = 1,
                stream = false,
                stop = (object?)null
            };

            // JSON stringine dönüştür
            string jsonString = JsonSerializer.Serialize(request);

            // JSON'u HttpContent'e dönüştür
            HttpContent httpContent = new StringContent(jsonString, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://api.groq.com/openai/v1/chat/completions", httpContent);

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Yanıt kodu: {response.StatusCode}");
                Console.WriteLine($"Hata içeriği: {response.Content}");
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
