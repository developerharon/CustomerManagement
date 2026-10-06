using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.API.Shared.DTOs.Customers;

public class CreateOrUpdateCustomerDTO
{
    public int Id { get; set; }
    [Required, StringLength(30)]
    public string FirstName { get; set; }
    [Required, StringLength(30)]
    public string LastName { get; set; }
    [Required, StringLength(100)]
    public string Email { get; set; }
    [Required, StringLength(50)]
    public string PhoneNumber { get; set; }
}