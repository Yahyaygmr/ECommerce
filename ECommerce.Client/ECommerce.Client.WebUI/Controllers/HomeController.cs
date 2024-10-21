using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Client.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(IHttpContextAccessor contextAccessor)
        {
            _httpContextAccessor = contextAccessor;
        }

        public IActionResult Index()
        {
            var accessToken = _httpContextAccessor.HttpContext.User.FindFirst("AccessToken")?.Value;

            ViewBag.AccessToken = accessToken;

            return View();
        }
    }
}
