using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Client.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
