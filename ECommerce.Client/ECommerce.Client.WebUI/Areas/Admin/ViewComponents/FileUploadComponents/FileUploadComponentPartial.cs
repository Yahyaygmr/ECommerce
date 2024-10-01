using ECommerce.Client.WebUI.Areas.Admin.Models.FileUploadModels;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Client.WebUI.Areas.Admin.ViewComponents.FileUploadComponents
{
    public class FileUploadComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke(FileUplaoadModel model)
        {
            return View(model);
        }
    }
}
