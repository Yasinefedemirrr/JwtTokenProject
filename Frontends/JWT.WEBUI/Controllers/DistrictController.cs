using Microsoft.AspNetCore.Mvc;

namespace JWT.WEBUI.Controllers
{
    public class DistrictController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
