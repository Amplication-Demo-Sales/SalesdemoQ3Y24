using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogManagement.Infrastructure.Models;

[Table("Categories")]
public class CategoryDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    [StringLength(1000)]
    public string? Description { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Name { get; set; }

    public List<ProductDbModel>? Products { get; set; } = new List<ProductDbModel>();

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
