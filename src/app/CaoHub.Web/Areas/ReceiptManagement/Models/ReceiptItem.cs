using CaoHub.Web.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaoHub.Web.Areas.ReceiptManagement.Models
{
    /// <summary>
    /// An entity representing an item in a receipt.
    /// </summary>
    public class ReceiptItem : LogicalDeleteEntityBase
    {
        /// <summary>
        /// Gets or sets the ID of the <see cref="ReceiptManagement.Receipt"/> in which this item is located.
        /// </summary>
        [Required]
        [ForeignKey("Receipt")]
        public int ReceiptId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the <see cref="ReceiptManagement.Product"/> being billed.
        /// </summary>
        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the unit price, pre-tax.
        /// </summary>
        [Required]
        [Range(0, 999999.999)]
        [Precision(9, 3)]
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the unit discount amount, pre-tax.
        /// </summary>
        [Range(0, 999999.999)]
        [Precision(9, 3)]
        public decimal? UnitDiscount { get; set; }

        /// <summary>
        /// Gets or sets the quantity the <see cref="ReceiptManagement.Product"/> being billed.
        /// </summary>
        [Required]
        [Range(1, 1000)]
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="ReceiptManagement.Receipt"/> in which this item is located.
        /// </summary>
        public virtual Receipt Receipt { get; set; } = null!;

        /// <summary>
        /// Gets or sets the <see cref="ReceiptManagement.Product"/> being billed.
        /// </summary>
        public virtual Product Product { get; set; } = null!;

        /// <summary>
        /// Gets or sets the collection of <see cref="ReceiptManagement.ReceiptItemPerson"/> in which this receipt item is referenced.
        /// </summary>
        public virtual ICollection<ReceiptItemPerson> ReceiptItemsPeople { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of <see cref="ReceiptManagement.ReceiptItemTax"/> in which this receipt item is referenced.
        /// </summary>
        public virtual ICollection<ReceiptItemTax> ReceiptItemsTaxes { get; set; } = [];
    }
}
