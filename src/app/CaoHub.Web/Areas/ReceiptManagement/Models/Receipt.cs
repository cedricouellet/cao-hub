using CaoHub.Web.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaoHub.Web.Areas.ReceiptManagement.Models
{
    /// <summary>
    /// An entity representing a receipt
    /// </summary>
    public class Receipt : LogicalDeleteEntityBase
    {
        /// <summary>
        /// Gets or sets the ID of the <see cref="ReceiptManagement.Store"/> where this receipt was produced.
        /// </summary>
        [Required]
        [ForeignKey("Store")]
        public int StoreId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the <see cref="ReceiptManagement.Person"/> who paid this receipt.
        /// </summary>
        [Required]
        [ForeignKey("PaidByPerson")]
        public int PaidByPersonId { get; set; }

        /// <summary>
        /// Gets or sets the date in which this receipt was produces.
        /// </summary>
        public DateTime Date { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the <see cref="ReceiptManagement.Store"/> where this receipt was produced.
        /// </summary>
        public virtual Store Store { get; set; } = null!;

        /// <summary>
        /// Gets or sets the <see cref="ReceiptManagement.Person"/> who paid this receipt.
        /// </summary>
        public virtual Person PaidByPerson { get; set; } = null!;

        /// <summary>
        /// Gets or sets the collection of <see cref="ReceiptManagement.ReceiptItem"/> in this receipt.
        /// </summary>
        public virtual ICollection<ReceiptItem> ReceiptItems { get; set; } = [];
    }
}
