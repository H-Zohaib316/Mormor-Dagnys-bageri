
using System.ComponentModel.DataAnnotations;

namespace MormorBageri.DTOs.Customers;

public class PostCustomerDto 
{
    [Required(ErrorMessage = "StoreName is required")]
    public string StoreName { get; set; }
    [Required(ErrorMessage = "Phone is required")]
    public string Phone { get; set; }
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; }
    [Required(ErrorMessage = "ContactPerson is required")]
    public string ContactPerson { get; set; }
    [Required(ErrorMessage = "DeliveryAddress is required")]
    public string DeliveryAddress { get; set; }
    [Required(ErrorMessage = "BiilingAddress is required")]
    public string BillingAddress { get; set; }


}
