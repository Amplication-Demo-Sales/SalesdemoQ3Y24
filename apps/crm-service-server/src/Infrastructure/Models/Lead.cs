using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrmService.Infrastructure.Models;

[Table("Leads")]
public class LeadDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? CustomerId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public CustomerDbModel? Customer { get; set; } = null;

    [Key()]
    [Required()]
    public string Id { get; set; }

    public List<MeetingDbModel>? Meetings { get; set; } = new List<MeetingDbModel>();

    [StringLength(1000)]
    public string? Name { get; set; }

    [StringLength(1000)]
    public string? Priority { get; set; }

    [StringLength(1000)]
    public string? Status { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
