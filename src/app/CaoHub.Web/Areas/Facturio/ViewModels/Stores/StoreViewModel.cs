namespace CaoHub.Web.Areas.Facturio.ViewModels.Stores
{
    public class StoreViewModel
    {
        public int Id { get; set; }

        public int StoreCategoryId { get; set; }

        public string Name { get; set; } = null!;

        public string StoreCategoryName { get; set; } = null!;
    }
}
