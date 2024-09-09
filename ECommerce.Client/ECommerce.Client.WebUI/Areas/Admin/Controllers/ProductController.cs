using ECommerce.Client.WebUI.Areas.Admin.Models.ProductModels;
using ECommerce.Client.WebUI.Custom.CustomHttpClient;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ECommerce.Client.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/{controller}/{action}/{id?}")]
    public class ProductController : Controller
    {
        private readonly CustomHttpClientService _customHttpClientService;

        public ProductController(CustomHttpClientService customHttpClientService)
        {
            _customHttpClientService = customHttpClientService;
        }

        public async Task<IActionResult> Index(int? size=5, int? page=0)
        {
            RequestParameters param = new()
            {
                controller = "Products",
                querystring = $"page={page}&size={size}"
            };
            var values = await _customHttpClientService.Get<GetProductsWithPagination>(param);
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
            RequestParameters param = new() { controller = "Products" };
            try
            {
                var returnvalue = await _customHttpClientService.Post(param, model);
                return RedirectToAction("Index");
            }
            catch (HttpRequestException ex)
            {

                // Sunucudan gelen hatayı JSON formatında yakalayıp işleme
                var responseContent = ex.Message;

                // Eğer sunucudan dönen hata mesajı JSON formatında ise ve detayları içeriyorsa:
                // Örnek: [{"key":"Price","value":["Fiyat bilgisi negatif olamaz"]},{"key":"Stock","value":["Stok bilgisi negatif olamaz"]}]
                try
                {
                    // Hata mesajı JSON değilse, sadece JSON kısmını ayıklıyoruz.
                    var jsonStartIndex = responseContent.IndexOf('[');
                    if (jsonStartIndex >= 0)
                    {
                        var jsonContent = responseContent.Substring(jsonStartIndex);

                        var errorList = JsonConvert.DeserializeObject<List<ErrorDetail>>(jsonContent);

                        if (errorList != null)
                        {
                            foreach (var error in errorList)
                            {
                                foreach (var message in error.Value)
                                {
                                    ModelState.AddModelError(error.Key, message);
                                }
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, responseContent);
                    }
                }
                catch (Exception)
                {
                    ModelState.AddModelError(string.Empty, "Beklenmeyen bir hata oluştu: " + responseContent);
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            RequestParameters param = new()
            {
                controller = "Products",
                action = "getbyid"
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
public class ErrorDetail
{
    public string Key { get; set; }
    public List<string> Value { get; set; }
}