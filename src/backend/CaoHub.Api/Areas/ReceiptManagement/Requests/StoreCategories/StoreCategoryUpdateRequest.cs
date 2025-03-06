using System.ComponentModel.DataAnnotations;

namespace CaoHub.Api.Areas.ReceiptManagement.Requests.StoreCategories
{
    public record StoreCategoryUpdateParams : StoreCategoryCreateParams
    {
        [Required]
        public int? Id { get; set; }
    }
}
