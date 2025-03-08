using CaoHub.Web.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaoHub.Web.Areas.ReceiptManagement.Models
{
    /// <summary>
    /// An entity representing a store.
    /// </summary>
    public class Store : LogicalDeleteEntityBase
    {
        /// <summary>
        /// Gets or sets the ID of this store's <see cref="ReceiptManagement.StoreCategory"/>.
        /// </summary>
        [Required]
        [ForeignKey("Store")]
        public int StoreCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the name of this store.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the <see cref="ReceiptManagement.StoreCategory"/> of this store.
        /// </summary>
        public virtual StoreCategory StoreCategory { get; set; } = null!;

        /// <summary>
        /// Gets or sets the collection of <see cref="ReceiptManagement.Receipt"/> produced by this store.
        /// </summary>
        public virtual ICollection<Receipt> Receipts { get; set; } = [];
    }
}
