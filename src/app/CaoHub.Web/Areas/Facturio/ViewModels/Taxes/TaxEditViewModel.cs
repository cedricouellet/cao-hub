using CaoHub.Data.Models.Facturio;
using CaoHub.Web.Resources;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CaoHub.Web.Areas.Facturio.ViewModels.Taxes
{
    public class TaxEditViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(SharedResource))]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(SharedResource))]
        [MaxLength(20, ErrorMessageResourceName = "ErrorMaxLength", ErrorMessageResourceType = typeof(SharedResource))]
        public string? Name { get; set; }

        [Display(Name = "Description", ResourceType = typeof(SharedResource))]
        [MaxLength(50, ErrorMessageResourceName = "ErrorMaxLength", ErrorMessageResourceType = typeof(SharedResource))]
        public string? Description { get; set; }

        [Display(Name = "Value", ResourceType = typeof(SharedResource))]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(SharedResource))]
        [Range(0.001, 999999.99999, ErrorMessageResourceName = "ErrorRange", ErrorMessageResourceType = typeof(SharedResource))]
        public decimal? Value { get; set; }

        [Display(Name = "Facturio_CalculationMethods_Singular", ResourceType = typeof(SharedResource))]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(SharedResource))]
        public CalculationMethod? CalculationMethod { get; set; }

        public IEnumerable<SelectListItem> CalculationMethods { get; set; } = [];
    }
}
