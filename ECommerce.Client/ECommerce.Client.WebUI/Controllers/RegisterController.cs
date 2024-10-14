using ECommerce.Client.WebUI.Custom.CustomHttpClient;
using ECommerce.Client.WebUI.Models.RegisterModels;
using ECommerce.Client.WebUI.Models.ReturnVals;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Client.WebUI.Controllers
{
    public class RegisterController : Controller
    {
        private readonly CustomHttpClientService _customHttpClientService;

        public RegisterController(CustomHttpClientService customHttpClientService)
        {
            _customHttpClientService = customHttpClientService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        } 
        [HttpPost]
        public async Task<IActionResult> Index(RegisterFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            return View();
        }
    }
}
