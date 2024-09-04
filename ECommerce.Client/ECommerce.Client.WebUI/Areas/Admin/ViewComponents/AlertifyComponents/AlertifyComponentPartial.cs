using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Client.WebUI.Areas.Admin.ViewComponents.AlertifyComponents
{
    public class AlertifyComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
