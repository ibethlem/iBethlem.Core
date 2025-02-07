using System.ComponentModel.DataAnnotations;

namespace iBethlem.Core.Models.Entities;

public abstract class NamedEntity : BaseEntity
{
    [MaxLength(100)]
    public string Name { get; set; }
}
