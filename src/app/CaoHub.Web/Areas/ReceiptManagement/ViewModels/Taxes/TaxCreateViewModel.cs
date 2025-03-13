using System.ComponentModel.DataAnnotations;

namespace CaoHub.Web.Areas.ReceiptManagement.ViewModels.Taxes
{
    public class TaxCreateViewModel
    {
        [Required]
        [MaxLength(20)]
        public string? Name { get; set; }

        [MaxLength(50)]
        public string? Description { get; set; }

        [Required]
        [Range(0.0, 100.0)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:P3}")]
        public decimal? RatePercentage { get; set; }
    }
}
