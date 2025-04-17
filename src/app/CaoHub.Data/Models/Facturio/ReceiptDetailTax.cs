using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaoHub.Data.Models.Facturio
{
    [Table("ReceiptDetail_Tax", Schema = "Facturio")]
    [PrimaryKey(nameof(ReceiptDetailId), nameof(TaxId))]
    public class ReceiptDetailTax 
    {
        [Required]
        [ForeignKey("ReceiptDetail")]
        public int ReceiptDetailId { get; set; }

        [Required]
        [ForeignKey("Tax")]
        public int TaxId { get; set; }

        public virtual ReceiptDetail ReceiptDetail { get; set; } = null!;

        public virtual Tax Tax { get; set; } = null!;
    }
}
