using System.ComponentModel.DataAnnotations;

namespace CaoHub.Web.Areas.ReceiptManagement.ViewModels
{
    public class StoreCategoryCreateViewModel
    {
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; } = null!;
    }
}
