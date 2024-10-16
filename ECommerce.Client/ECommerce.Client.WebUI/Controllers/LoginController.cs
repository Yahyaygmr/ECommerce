using ECommerce.Client.WebUI.Custom.CustomHttpClient;
using ECommerce.Client.WebUI.Models.LoginModels;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Client.WebUI.Controllers
{
    public class LoginController : Controller
    {
        private readonly CustomHttpClientService _customHttpClientService;

        public LoginController(CustomHttpClientService customHttpClientService)
        {
            _customHttpClientService = customHttpClientService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(LoginFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            RequestParameters param = new()
            {
                controller = "users",
                action = "Login"
            };
            var response = await _customHttpClientService.Post(param, model);
            return View();
        }
    }
}
