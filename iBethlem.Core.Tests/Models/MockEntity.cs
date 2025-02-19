using iBethlem.Core.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace iBethlem.Core.Tests.Models;

public class MockEntity : NamedEntity
{
    [MaxLength(150)]
    public string MockProperty { get; set; }
}
