using System.ComponentModel.DataAnnotations;

namespace CaoHub.Api.Areas.ReceiptManagement.Requests
{
    public record StoreCategoryCreateRequest
    {
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }
    }
}
