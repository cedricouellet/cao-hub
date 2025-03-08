using CaoHub.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace CaoHub.Web.Areas.ReceiptManagement.Models
{
    /// <summary>
    /// The entity representing a category of stores.
    /// </summary>
    public class StoreCategory : LogicalDeleteEntityBase
    {
        /// <summary>
        /// Gets or sets the name of this store category.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the collection of <see cref="ReceiptManagement.Store"/> which are part of this store category.
        /// </summary>
        public virtual ICollection<Store> Stores { get; set; } = [];
    }
}
