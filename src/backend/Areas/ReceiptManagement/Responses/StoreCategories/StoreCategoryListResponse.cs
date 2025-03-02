using CaoHub.Api.Responses;

namespace CaoHub.Api.Areas.ReceiptManagement.Responses.StoreCategories
{
    public record StoreCategoryListResponse : PaginationResponse
    {
        public IEnumerable<StoreCategoryListItem> Items { get; set; } = [];
    }
}
