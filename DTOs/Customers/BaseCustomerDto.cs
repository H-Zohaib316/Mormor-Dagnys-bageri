using System.ComponentModel.DataAnnotations;

namespace MormorBageri.DTOs.Customers;

public abstract class BaseCustomerDto
{
    [Required(ErrorMessage = "StoreName is required")]
    public string StoreName { get; set; }
    public string ContactPerson { get; set; }
    public string Email { get; set; }



}
