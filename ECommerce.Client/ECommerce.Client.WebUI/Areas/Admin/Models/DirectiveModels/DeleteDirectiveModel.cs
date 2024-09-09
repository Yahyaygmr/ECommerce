namespace ECommerce.Client.WebUI.Areas.Admin.Models.DirectiveModels
{
    public class DeleteDirectiveModel
    {
        public string ItemId { get; set; }
        public string controller { get; set; }
        public string action { get; set; }
        public string? area { get; set; }
    }
}
