namespace ECommerce.Client.WebUI.Areas.Admin.Models.ProductModels
{
    public class ResultProductModel
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
    }
}
