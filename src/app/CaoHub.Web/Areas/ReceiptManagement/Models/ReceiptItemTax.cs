using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaoHub.Web.Areas.ReceiptManagement.Models
{
    /// <summary>
    /// Represents an entity grouping a <see cref="ReceiptManagement.ReceiptItem"/> and a <see cref="ReceiptManagement.Tax"/>
    /// </summary>
    [PrimaryKey(nameof(ReceiptItemId), nameof(TaxId))]
    public class ReceiptItemTax 
    {
        /// <summary>
        /// Gets or sets the ID of the <see cref="ReceiptManagement.ReceiptItem"/>
        /// </summary>
        [Required]
        [ForeignKey("ReceiptItem")]
        public int ReceiptItemId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the <see cref="ReceiptManagement.Tax"/>
        /// </summary>
        [Required]
        [ForeignKey("Tax")]
        public int TaxId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ReceiptManagement.ReceiptItem"/>.
        /// </summary>
        public virtual ReceiptItem ReceiptItem { get; set; } = null!;

        /// <summary>
        /// Gets or sets the <see cref="ReceiptManagement.ReceiptItemTax" />.
        /// </summary>
        public virtual Tax Tax { get; set; } = null!;
    }
}
