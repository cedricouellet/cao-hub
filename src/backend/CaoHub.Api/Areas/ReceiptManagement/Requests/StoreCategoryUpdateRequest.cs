using System.ComponentModel.DataAnnotations;

namespace CaoHub.Api.Areas.ReceiptManagement.Requests
{
    public record StoreCategoryUpdateRequest
    {
        [Required]
        public int? Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }
    }
}
