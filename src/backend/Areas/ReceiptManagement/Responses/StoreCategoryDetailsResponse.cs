namespace CaoHub.Api.Areas.ReceiptManagement.Responses
{
    public record StoreCategoryDetailsResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
    }
}
