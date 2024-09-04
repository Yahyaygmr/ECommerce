using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Client.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/{controller}/{action}/{id?}")]
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
