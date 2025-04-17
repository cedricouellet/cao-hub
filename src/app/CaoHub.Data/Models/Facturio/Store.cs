using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaoHub.Data.Models.Facturio
{
    [Table("Store", Schema = "Facturio")]
    public class Store : LogicalDeleteEntityBase
    {
        [Required]
        [ForeignKey("StoreCategory")]
        public int StoreCategoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        public virtual StoreCategory StoreCategory { get; set; } = null!;

        public virtual ICollection<Receipt> Receipts { get; set; } = [];
    }
}
