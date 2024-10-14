using ECommerce.Client.WebUI.Areas.Admin.Models.ImageModels;
using ECommerce.Client.WebUI.Custom.CustomHttpClient;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ECommerce.Client.WebUI.Areas.Admin.ViewComponents.ProductComponents
{
    public class ProductImageFilesComponentPartial : ViewComponent
    {
        private readonly CustomHttpClientService _customHttpClientService;

        public ProductImageFilesComponentPartial(CustomHttpClientService customHttpClientService)
        {
            _customHttpClientService = customHttpClientService;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            ViewBag.Id = id;
            RequestParameters param = new()
            {
                controller = "products",
                action = "GetProductImages",
            };
            var response = await _customHttpClientService.Get<List<ImageListModel>>(param, id);
            List<ImageListModel> list = response.Data;

            return View(list);
        }
    }
}
