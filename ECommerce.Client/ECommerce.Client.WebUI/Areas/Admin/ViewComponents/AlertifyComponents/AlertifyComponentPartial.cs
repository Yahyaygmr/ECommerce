using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Client.WebUI.Areas.Admin.ViewComponents.AlertifyComponents
{
    public class AlertifyComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            // Başarılı bir işlemden sonra bildirim gönderme
            TempData["SuccessMessage"] = "Ürün başarıyla eklendi!";

            // Hatalı bir işlem durumunda bildirim gönderme
            TempData["ErrorMessage"] = "Bir hata oluştu!";
            return View();
        }
    }
}
