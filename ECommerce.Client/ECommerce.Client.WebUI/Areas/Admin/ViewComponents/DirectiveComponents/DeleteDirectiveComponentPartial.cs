using ECommerce.Client.WebUI.Areas.Admin.Models.DirectiveModels;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Client.WebUI.Areas.Admin.ViewComponents.DirectiveComponents
{
    public class DeleteDirectiveComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke(DeleteDirectiveModel model)
        {
            return View(model);
        }
    }
}
