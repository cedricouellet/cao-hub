using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CaoHub.Data.ReceiptManagement.Models
{
    /// <summary>
    /// Represents an entity grouping <see cref="Models.ReceiptItem"/> and a <see cref="Models.Person" />.
    /// </summary>
    [PrimaryKey(nameof(ReceiptItemId), nameof(PersonId))]
    public class ReceiptItemPerson
    {
        /// <summary>
        /// Gets or sets the ID of the <see cref="Models.ReceiptItem"/>.
        /// </summary>
        [Required]
        [ForeignKey("ReceiptItem")]
        public int ReceiptItemId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the <see cref="Models.Person"/>.
        /// </summary>
        [Required]
        [ForeignKey("Person")]
        public int PersonId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="Models.ReceiptItem"/>.
        /// </summary>
        public virtual ReceiptItem ReceiptItem { get; set; } = null!;

        /// <summary>
        /// Gets or sets the <see cref="Models.Person"/>.
        /// </summary>
        public virtual Person Person { get; set; } = null!;
    }
}
