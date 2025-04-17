using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaoHub.Data.Models.Facturio
{
    [Table("StoreCategory", Schema = "Facturio")]
    public class StoreCategory : LogicalDeleteEntityBase
    {
        [Required]
        [MaxLength(25)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Store> Stores { get; set; } = [];
    }
}
