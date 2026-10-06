using System.ComponentModel.DataAnnotations;
using CustomerManagement.API.Domain.Entities.Base;

namespace CustomerManagement.API.Domain.Entities;

public class CustomerEntity : BaseEntity
{
    [Required, StringLength(30)]
    public string FirstName { get; set; }
    [Required, StringLength(30)]
    public string LastName { get; set; }
    [Required, StringLength(100)]
    public string Email { get; set; }
    [Required, StringLength(50)]
    public string PhoneNumber { get; set; }
}