using CaoHub.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace CaoHub.Web.Areas.ReceiptManagement.Models
{
    /// <summary>
    /// An entity representing a product.
    /// </summary>
    public class Product : LogicalDeleteEntityBase
    {
        /// <summary>
        /// Gets or sets the name of this product.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the collection of <see cref="ReceiptItem"/> in which this product has appeared.
        /// </summary>
        public virtual ICollection<ReceiptItem> ReceiptItems { get; set; } = [];
    }
}
