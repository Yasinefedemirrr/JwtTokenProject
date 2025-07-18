using Microsoft.AspNetCore.Mvc;

namespace JWT.WEBUI.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
