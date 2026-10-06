using System.ComponentModel.DataAnnotations;

namespace CustomerManagement.API.Domain.Entities.Base;

public class BaseEntity
{
    [Required, Key]
    public int Id { get; set; }
    [Required]
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set;}
}