using CaoHub.Web.Resources;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CaoHub.Web.Areas.Facturio.ViewModels.Stores
{
    public class StoreEditViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(SharedResource))]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(SharedResource))]
        [MaxLength(50, ErrorMessageResourceName = "ErrorMaxLength", ErrorMessageResourceType = typeof(SharedResource))]
        public string? Name { get; set; }

        [Display(Name = "Facturio_StoresCategories_Singular")]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(SharedResource))]
        public int? StoreCategoryId { get; set; }

        public IEnumerable<SelectListItem> StoreCategories { get; set; } = [];
    }
}
