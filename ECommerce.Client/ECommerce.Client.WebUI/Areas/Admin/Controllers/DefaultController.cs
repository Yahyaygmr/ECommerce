using ECommerce.Client.WebUI.Custom.CustomHttpClient;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadFiles([FromQuery] string id,List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                return BadRequest("No files were uploaded.");
            }
            // API'ye dosyaları gönderme işlemi

            var content = files;

            RequestParameters param = new()
            {
                controller = "products",
                action = "upload",
                querystring =$"id={id}"
            };
            var response = await _customHttpClientService.PostData(param, content);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            else
            {
                return StatusCode((int)response.StatusCode, new { message = "Error uploading files to the API." });
            }
        }

        public IActionResult FilePartial(string id)
        {
            ViewBag.id = id;
            return PartialView();
        }
    }
}
