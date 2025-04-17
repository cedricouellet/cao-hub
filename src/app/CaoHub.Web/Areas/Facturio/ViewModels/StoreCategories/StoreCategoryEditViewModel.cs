using CaoHub.Web.Resources;
using System.ComponentModel.DataAnnotations;

namespace CaoHub.Web.Areas.Facturio.ViewModels.StoreCategories
{
    public class StoreCategoryEditViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(SharedResource))]
        [MaxLength(25, ErrorMessageResourceName = "ErrorMaxLength", ErrorMessageResourceType = typeof(SharedResource))]
        public string? Name { get; set; }
    }
}
