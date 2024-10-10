using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Client.WebUI.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
