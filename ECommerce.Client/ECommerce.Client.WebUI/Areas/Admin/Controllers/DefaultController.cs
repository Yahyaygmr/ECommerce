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
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
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
                action = "upload"

            };
            var response = await _customHttpClientService.PostData(param, content);
            //if (response.IsSuccessStatusCode)
            //{
            //    try
            //    {
            //        return Ok("Index");
            //    }
            //    catch (Exception ex)
            //    {

            //        throw new Exception(ex.Message);
            //    }

            //}
            //else
            //{
            //    return StatusCode((int)response.StatusCode, "Error uploading files to the API.");
            //}
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
