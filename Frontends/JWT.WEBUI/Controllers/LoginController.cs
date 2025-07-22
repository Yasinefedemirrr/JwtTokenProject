using JWT.WEBUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace JWT.WEBUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LoginController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonSerializer.Serialize(model);
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://localhost:7270/api/Login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var tokenObj = JsonSerializer.Deserialize<TokenResponse>(responseData, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (tokenObj != null && !string.IsNullOrEmpty(tokenObj.Token))
                {
                    TempData["token"] = tokenObj.Token;
                    return RedirectToAction("Index", "CityWeather");
                }

                ViewBag.Error = "Token alınamadı.";
                return View();
            }

            ViewBag.Error = "Kullanıcı adı veya şifre yanlış.";
            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
