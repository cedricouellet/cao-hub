using CaoHub.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace CaoHub.Web.Areas.ReceiptManagement.Models
{
    /// <summary>
    /// An entity representing a tax on goods.
    /// </summary>
    public class Tax : LogicalDeleteEntityBase
    {
        /// <summary>
        /// Gets or sets the name of this tax.
        /// </summary>
        [Required]
        [MaxLength(20)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the description of this tax.
        /// </summary>
        [MaxLength(50)]
        public string? Description { get; set; } = null!;

        /// <summary>
        /// Gets or sets the rate amount of this tax.
        /// </summary>
        [Required]
        [Range(0.0, 1.0)]
        public decimal Rate { get; set; }

        /// <summary>
        /// Gets or sets the collection of <see cref="ReceiptManagement.ReceiptItemTax"/> in which this tax is referenced.
        /// </summary>
        public virtual ICollection<ReceiptItemTax> ReceiptItemsTaxes { get; set; } = [];
    }
}
