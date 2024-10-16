using ECommerce.Client.WebUI.Custom.CustomHttpClient;
using ECommerce.Client.WebUI.Models.LoginModels;
using ECommerce.Client.WebUI.Models.Responses;
using ECommerce.Client.WebUI.Models.Tokens;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

            if (response.Message != null)
            {
                MessageResponse mresponse = JsonConvert.DeserializeObject<MessageResponse>(response.Message);
            
                if (mresponse.AccessToken != null)
                {
                    Token token1 = new()
                    {
                        AccessToken = mresponse.AccessToken,
                        Expiration = Convert.ToDateTime(mresponse.Expiration),
                    };
                }
            }
            return View();
        }
    }
}
