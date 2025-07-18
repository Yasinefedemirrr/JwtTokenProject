using JWT.WEBUI.Models;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace JWT.WEBUI.Controllers
{
    public class DistrictController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DistrictController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index(int id) // id = cityId
        {
            var token = TempData["token"]?.ToString();
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("AccessDenied", "Login");
            }

            TempData.Keep("token");

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"https://localhost:7280/api/Districts?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonSerializer.Deserialize<List<DistrictViewModel>>(jsonData);
                return View(values);
            }

            return RedirectToAction("AccessDenied", "Login");
        }
    }
}
