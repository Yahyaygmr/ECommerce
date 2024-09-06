namespace ECommerce.Client.WebUI.Areas.Admin.Models.ProductModels
{
    public class UpdateProductModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
