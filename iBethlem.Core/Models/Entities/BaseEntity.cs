using System.ComponentModel.DataAnnotations;

namespace iBethlem.Core.Models.Entities;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public DateTime Created { get; set; } = DateTime.UtcNow;
    [Required]
    public bool IsActive { get; set; } = true;
}
