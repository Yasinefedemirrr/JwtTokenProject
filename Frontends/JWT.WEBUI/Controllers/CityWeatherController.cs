using JWT.WEBUI.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace JWT.WEBUI.Controllers
{
    public class CityWeatherController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CityWeatherController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var token = TempData["token"]?.ToString();
            TempData.Keep("token");

            if (string.IsNullOrEmpty(token))
                return RedirectToAction("Index", "Login");

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync("https://localhost:7270/api/CityWeathers");

            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                return RedirectToAction("AccessDenied", "Login");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index", "Login");

            var jsonData = await response.Content.ReadAsStringAsync();
            var cityWeatherList = JsonSerializer.Deserialize<List<CityWeatherViewModel>>(jsonData);

            return View(cityWeatherList);
        }
    }
}
