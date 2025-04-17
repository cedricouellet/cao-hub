using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaoHub.Data.Models.Facturio
{
    [Table("Person", Schema = "Facturio")]
    public class Person : LogicalDeleteEntityBase
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        public virtual ICollection<Receipt> Receipts { get; set; } = [];

        public virtual ICollection<ReceiptDetailPerson> ReceiptDetailsPeople { get; set; } = [];
    }
}
