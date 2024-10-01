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
        public async Task<IActionResult> UploadFiles(List<IFormFile> files, string id)
        {
            if (files == null || files.Count == 0)
            {
                return BadRequest("No files were uploaded.");
            }
            string ids = id;
            // API'ye dosyaları gönderme işlemi

            var content = files;

            RequestParameters param = new()
            {
                controller = "products",
                action = "upload"

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
    }
}
