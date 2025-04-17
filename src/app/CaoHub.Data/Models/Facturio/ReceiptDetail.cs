using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaoHub.Data.Models.Facturio
{
    [Table("ReceiptDetail", Schema = "Facturio")]
    public class ReceiptDetail : LogicalDeleteEntityBase
    {
        [Required]
        [ForeignKey("Receipt")]
        public int ReceiptId { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        [Required]
        [Range(0.001, 999999.999)]
        [Precision(9, 3)]
        public decimal UnitPrice { get; set; }

        [Range(0.001, 999999.999)]
        [Precision(9, 3)]
        public decimal? UnitDiscount { get; set; }

        [Required]
        [Range(1, 1000)]
        public int Quantity { get; set; }

        public virtual Receipt Receipt { get; set; } = null!;

        public virtual Product Product { get; set; } = null!;

        public virtual ICollection<ReceiptDetailPerson> ReceiptDetailsPeople { get; set; } = [];

        public virtual ICollection<ReceiptDetailTax> ReceiptDetailsTaxes { get; set; } = [];

        [NotMapped]
        public decimal Price => UnitPrice * Quantity;

        [NotMapped]
        public decimal Discount => UnitDiscount ?? 0 * Quantity;

        [NotMapped]
        public decimal Subtotal => Price - Discount;

        [NotMapped]
        public decimal Total
        {
            get
            {
                var taxAmount = ReceiptDetailsTaxes
                    .Select(x => x.Tax)
                    .Aggregate(0m, (acc, tax) => tax.CalculationMethod switch
                    {
                        CalculationMethod.PercentageBased => acc + Subtotal * tax.Value,
                        CalculationMethod.AdditiveValue => acc + tax.Value,
                        _ => acc
                    });

                return Subtotal + taxAmount;
            }
        }
    }
}
