namespace CaoHub.Api.Areas.ReceiptManagement.Responses.StoreCategories
{
    public record StoreCategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
}
