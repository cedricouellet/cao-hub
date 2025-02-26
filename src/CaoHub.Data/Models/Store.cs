using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaoHub.Data.Models
{
    /// <summary>
    /// An entity representing a store
    /// </summary>
    public class Store
    {
        /// <summary>
        /// Gets or sets the ID 
        /// </summary>
        [Key]
        public int StoreId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the store's category
        /// </summary>
        [ForeignKey("Store")]
        [Required]
        public int StoreCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets whether or not the store is active
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the store category
        /// </summary>
        public virtual StoreCategory StoreCategory { get; set; } = null!;

        /// <summary>
        /// Gets or sets the collection of receipts
        /// </summary>
        public virtual ICollection<Receipt> Receipts { get; set; } = [];
    }
}
