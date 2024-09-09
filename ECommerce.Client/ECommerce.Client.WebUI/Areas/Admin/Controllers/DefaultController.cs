using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Client.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/{controller}/{action}/{id?}")]
    public class DefaultController : Controller
    {
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
            using (var client = new HttpClient())
            {
                var content = new MultipartFormDataContent();

                // Seçilen dosyaları döngüyle al ve API'ye ekle
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        var streamContent = new StreamContent(file.OpenReadStream());
                        streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                        content.Add(streamContent, "files", file.FileName);
                    }
                }

                // API endpoint URL'sini buraya yazın
                var apiUrl = "https://api-endpoint-url.com/upload";
                var response = await client.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    return Ok("Files successfully uploaded.");
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "Error uploading files to the API.");
                }
            }
        }

    }
}
