using CaoHub.Web.Resources;
using System.ComponentModel.DataAnnotations;

namespace CaoHub.Web.Areas.Facturio.ViewModels.People
{
    public class PersonEditViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(SharedResource))]
        [Required(ErrorMessageResourceName = "ErrorRequired", ErrorMessageResourceType = typeof(SharedResource))]
        [MaxLength(50, ErrorMessageResourceName = "ErrorMaxLength", ErrorMessageResourceType = typeof(SharedResource))]
        public string? Name { get; set; }
    }
}
