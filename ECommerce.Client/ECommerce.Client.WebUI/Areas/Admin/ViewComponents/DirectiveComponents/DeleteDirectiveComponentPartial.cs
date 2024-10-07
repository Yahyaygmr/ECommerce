using ECommerce.Client.WebUI.Areas.Admin.Models.DirectiveModels;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Client.WebUI.Areas.Admin.ViewComponents.DirectiveComponents
{
    public class DeleteDirectiveComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke(DeleteDirectiveModel model)
        {
            ViewContext.HttpContext.Response.Headers["Cache-Control"] = "no-cache";
            return View(model);
        }
    }
}
