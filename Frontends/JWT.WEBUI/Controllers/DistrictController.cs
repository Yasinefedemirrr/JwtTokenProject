using JWT.WEBUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
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

        public async Task<IActionResult> Index(int id)
        {
            var token = TempData["token"]?.ToString();
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("AccessDenied", "Login");

            TempData.Keep("token");

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            // 🔁 API2 yerine artık API1 üzerinden proxy çağrısı yapıyoruz
            var response = await client.GetAsync($"https://localhost:7270/api/Proxy/districtweathers/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized ||
                response.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                return RedirectToAction("AccessDenied", "Login");
            }

            if (!response.IsSuccessStatusCode)
                return RedirectToAction("AccessDenied", "Login");

            var jsonData = await response.Content.ReadAsStringAsync();
            var districtList = JsonSerializer.Deserialize<List<DistrictViewModel>>(jsonData, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return View(districtList);
        }
    }
}
