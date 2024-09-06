namespace ECommerce.Api.WebAPI.Models.ProductViewModels
{
    public class UpdateProductViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
