namespace CaoHub.Web.Models
{
    /// <summary>
    /// The base class for database entities that contain logical (soft) delete properties.
    /// </summary>
    public abstract class LogicalDeleteEntityBase : EntityBase
    {
        /// <summary>
        /// Gets or sets whether or not the entity is active.
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}
