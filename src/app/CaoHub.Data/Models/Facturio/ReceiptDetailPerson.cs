using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaoHub.Data.Models.Facturio
{
    [Table("ReceiptDetail_Person", Schema = "Facturio")]
    [PrimaryKey(nameof(ReceiptDetailId), nameof(PersonId))]
    public class ReceiptDetailPerson
    {
        [Required]
        [ForeignKey("ReceiptDetail")]
        public int ReceiptDetailId { get; set; }

        [Required]
        [ForeignKey("Person")]
        public int PersonId { get; set; }

        public virtual ReceiptDetail ReceiptDetail { get; set; } = null!;

        public virtual Person Person { get; set; } = null!;
    }
}
