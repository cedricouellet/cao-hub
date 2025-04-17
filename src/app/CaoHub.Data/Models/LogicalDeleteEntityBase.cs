namespace CaoHub.Data.Models
{
    public abstract class LogicalDeleteEntityBase : EntityBase
    {
        public bool IsActive { get; set; } = true;
    }
}
