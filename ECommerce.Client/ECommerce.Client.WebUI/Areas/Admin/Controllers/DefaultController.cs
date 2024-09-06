using ECommerce.Client.WebUI.Areas.Admin.Models.ProductModels;
using ECommerce.Client.WebUI.Custom.CustomHttpClient;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ECommerce.Client.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/{controller}/{action}/{id?}")]
    public class DefaultController : Controller
    {
        private readonly CustomHttpClientService _customHttpClientService;

        public DefaultController(CustomHttpClientService customHttpClientService)
        {
            _customHttpClientService = customHttpClientService;
        }

        public async Task<IActionResult> Index()
        {
            RequestParameters param = new()
            {
                controller = "Products",
            };
            var values = await _customHttpClientService.Get<List<ResultProductModel>>(param);
            return View(values);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductModel model)
        {

            RequestParameters param = new()
            {
                controller = "Products"
            };
            var returnvalue = await _customHttpClientService.Post(param, model);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            RequestParameters param = new()
            {
                controller = "Products",
                action ="getbyid"
            };
            var values = await _customHttpClientService.Get<ResultProductModel>(param, id);

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductModel model)
        {

            RequestParameters param = new()
            {
                controller = "Products"
            };
            //UpdateProductModel model1 = new UpdateProductModel()
            //{
            //    Id = "1c0c7f06-ef91-4a92-ba1a-3788e66b7afc",
            //    Name = "deneme client model",
            //    Price = 3547,
            //    Stock = 150
            //};
            var returnvalue = await _customHttpClientService.Put(param, model);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(string id)
        {
            RequestParameters param = new()
            {
                controller = "Products",
                action = "delete"
            };
            var returnvalue = await _customHttpClientService.Delete(param, id);
            return RedirectToAction("Index");
        }
    }
}
