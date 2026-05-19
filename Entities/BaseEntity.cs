using System.ComponentModel.DataAnnotations;

namespace MormorBageri.Entities;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }

}
