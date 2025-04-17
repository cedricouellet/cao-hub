using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaoHub.Data.Models.Facturio
{
    [Table("Product", Schema = "Facturio")]
    public class Product : LogicalDeleteEntityBase
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public virtual ICollection<ReceiptDetail> ReceiptDetails { get; set; } = [];
    }
}
