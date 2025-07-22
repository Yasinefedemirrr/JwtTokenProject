using JWT.WEBUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

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

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                return RedirectToAction("AccessDenied", "Login");

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("Index", "Login");

            var jsonData = await response.Content.ReadAsStringAsync();
            var cityWeatherList = JsonSerializer.Deserialize<List<CityWeatherViewModel>>(jsonData, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(cityWeatherList);
        }

        [HttpGet]
        public async Task<IActionResult> GetDistricts(int cityId)
        {
            var token = TempData["token"]?.ToString();
            TempData.Keep("token");

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"https://localhost:7171/api/Districts?id={cityId}");

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                return Json(new { success = false, message = "Erişim reddedildi." });
            }

            if (!response.IsSuccessStatusCode)
                return Json(new { success = false });

            var jsonData = await response.Content.ReadAsStringAsync();
            var districts = JsonSerializer.Deserialize<List<DistrictViewModel>>(jsonData, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return Json(districts);
        }
    }
}
