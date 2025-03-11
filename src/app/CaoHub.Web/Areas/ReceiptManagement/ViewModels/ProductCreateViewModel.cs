using System.ComponentModel.DataAnnotations;

namespace CaoHub.Web.Areas.ReceiptManagement.ViewModels
{
    public class ProductCreateViewModel
    {
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }
    }
}
