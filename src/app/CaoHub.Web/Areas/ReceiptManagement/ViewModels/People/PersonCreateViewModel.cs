using System.ComponentModel.DataAnnotations;

namespace CaoHub.Web.Areas.ReceiptManagement.ViewModels.People
{
    public class PersonCreateViewModel
    {
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }
    }
}
