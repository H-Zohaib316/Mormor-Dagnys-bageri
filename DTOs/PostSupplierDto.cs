using System.ComponentModel.DataAnnotations;

namespace MormorBageri.DTOs;

public record PostSupplierDto
{
    [Required]
    public string SupplierName { get; set; }
    [Required]
    public string Address { get; set; }

    [Required]
    public string ContactPerson { get; set; }

    [Required]
    public string Phone { get; set; }

    [Required]
    public string Email { get; set; }

}
