using ECommerce.Client.WebUI.Custom.CustomHttpClient;
using ECommerce.Client.WebUI.Models.RegisterModels;
using ECommerce.Client.WebUI.Models.Responses;
using ECommerce.Client.WebUI.Models.ReturnVals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
using Newtonsoft.Json;

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
            RequestParameters param = new()
            {
                controller = "users"
            };
            var response = await _customHttpClientService.Post(param, model);
            if (response.Message != null)
            {
                MessageResponse mresponse = JsonConvert.DeserializeObject<MessageResponse>(response.Message);
                if(mresponse.Succeeded)
                {

                }else
                {
                    ViewBag.message = mresponse.Message;
                }
            }
            return View();
        }
    }
}