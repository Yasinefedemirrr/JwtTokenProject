using Microsoft.AspNetCore.Mvc;

namespace JWT.WEBUI.Controllers
{
    public class CityWeatherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
