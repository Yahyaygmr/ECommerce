namespace ECommerce.Client.WebUI.Areas.Admin.Models.FileUploadModels
{
    public class FileUplaoadModel : IDisposable
    {
        public string? area { get; set; }
        public string? controller { get; set; }
        public string? action { get; set; }
        public string? querystring { get; set; }

        public void Dispose()
        {
        }
    }
}
