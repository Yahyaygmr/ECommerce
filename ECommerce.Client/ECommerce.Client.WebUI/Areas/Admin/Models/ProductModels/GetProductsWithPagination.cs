namespace ECommerce.Client.WebUI.Areas.Admin.Models.ProductModels
{
    public class GetProductsWithPagination
    {
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
        public List<ResultProductModel> Products { get; set; }
    }
}
