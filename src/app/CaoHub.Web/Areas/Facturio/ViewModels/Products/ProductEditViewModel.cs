using CaoHub.Web.Resources;
using System.ComponentModel.DataAnnotations;

namespace CaoHub.Web.Areas.Facturio.ViewModels.Products
{
    public class ProductEditViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(SharedResource))]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(SharedResource))]
        [MaxLength(ErrorMessageResourceName = "ErrorMaxLength", ErrorMessageResourceType = typeof(SharedResource))]
        public string? Name { get; set; }
    }
}
