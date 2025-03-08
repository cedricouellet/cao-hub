using CaoHub.Web.Models;
using System.ComponentModel.DataAnnotations;

namespace CaoHub.Web.Areas.ReceiptManagement.Models
{
    /// <summary>
    /// An entity representing a person.
    /// </summary>
    public class Person : LogicalDeleteEntityBase
    {
        /// <summary>
        /// Gets or sets the name of this person.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the collection of <see cref="Receipt"/> paid by this person.
        /// </summary>
        public virtual ICollection<Receipt> Receipts { get; set; } = [];

        /// <summary>
        /// Gets or sets the collection of <see cref="ReceiptItemPerson"/> in which this person is referenced.
        /// </summary>
        public virtual ICollection<ReceiptItemPerson> ReceiptItemsPeople { get; set; } = [];
    }
}
