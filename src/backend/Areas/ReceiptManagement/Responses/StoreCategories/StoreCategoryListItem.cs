namespace CaoHub.Api.Areas.ReceiptManagement.Responses.StoreCategories
{
    public record StoreCategoryListItem
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
}
