using System.ComponentModel.DataAnnotations;

namespace CaoHub.Data.ReceiptManagement.Models
{
    /// <summary>
    /// The entity representing a category of stores
    /// </summary>
    public class StoreCategory
    {
        /// <summary>
        /// Gets or sets the ID
        /// </summary>
        [Key]
        public int StoreCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets whether or not the store category is active
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Gets or sets the collection of stores
        /// </summary>
        public virtual ICollection<Store> Stores { get; set; } = [];
    }
}
