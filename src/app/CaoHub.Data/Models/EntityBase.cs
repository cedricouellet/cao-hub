using System.ComponentModel.DataAnnotations;

namespace CaoHub.Data.Models
{
    public abstract class EntityBase
    {
        [Key]
        public int Id { get; set; }
    }
}
