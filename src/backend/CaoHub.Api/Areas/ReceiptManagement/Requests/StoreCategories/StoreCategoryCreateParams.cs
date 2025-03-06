using System.ComponentModel.DataAnnotations;

namespace CaoHub.Api.Areas.ReceiptManagement.Requests.StoreCategories
{
    public record StoreCategoryCreateParams
    {
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }
    }
}
