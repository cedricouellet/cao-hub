using System.ComponentModel.DataAnnotations;

namespace CaoHub.Data.Models
{
    /// <summary>
    /// The base class for database entities.
    /// </summary>
    public abstract class EntityBase
    {
        /// <summary>
        /// Gets or sets the unique identifier of this entity.
        /// </summary>
        [Key]
        public int Id { get; set; }
    }
}
