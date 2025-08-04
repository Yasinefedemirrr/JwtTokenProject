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
            var token = Request.Cookies["JWTToken"];
            if (string.IsNullOrEmpty(token))
                return RedirectToAction("AccessDenied", "Login");

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.GetAsync($"https://localhost:7270/api/Districts?id={id}");

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
