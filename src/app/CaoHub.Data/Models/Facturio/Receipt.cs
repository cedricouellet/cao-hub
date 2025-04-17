using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaoHub.Data.Models.Facturio
{
    [Table("Receipt", Schema = "Facturio")]
    public class Receipt : LogicalDeleteEntityBase
    {
        [Required]
        [ForeignKey("Store")]
        public int StoreId { get; set; }

        [Required]
        [ForeignKey("PaidByPerson")]
        public int PaidByPersonId { get; set; }

        [Range(0.001, 999999.999)]
        [Precision(9, 3)]
        public decimal? PrepaidAmount { get; set; }
        
        [Range(0.001, 999999.99999)]
        [Precision(9, 3)]
        public decimal? DiscountAmount { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        public virtual Store Store { get; set; } = null!;

        public virtual Person PaidByPerson { get; set; } = null!;

        public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; } = [];

        [NotMapped]
        public decimal Subtotal => ReceiptDetails.Sum(x => x.Subtotal);

        [NotMapped]
        public decimal Total => ReceiptDetails.Sum(x => x.Total) - (DiscountAmount ?? 0);
    }
}
