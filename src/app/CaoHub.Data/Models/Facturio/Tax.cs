using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaoHub.Data.Models.Facturio
{
    [Table("Tax", Schema = "Facturio")]
    public class Tax : LogicalDeleteEntityBase
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; } = null!;

        [MaxLength(50)]
        public string? Description { get; set; } = null!;

        [Required]
        [Range(0.001, 999999.99999)]
        [Precision(9, 5)]
        public decimal Value { get; set; }

        [Required]
        public CalculationMethod CalculationMethod { get; set; }

        [NotMapped]
        public string FormattedValue => CalculationMethod switch
        {
            CalculationMethod.PercentageBased => Value.ToString("P3"),
            CalculationMethod.AdditiveValue => Value.ToString("C"),
            _ => Value.ToString(),
        };

        public virtual ICollection<ReceiptDetailTax> ReceiptDetailsTaxes { get; set; } = [];
    }
}
