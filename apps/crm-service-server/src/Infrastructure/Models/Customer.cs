using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrmService.Infrastructure.Models;

[Table("Customers")]
public class CustomerDbModel
{
    [StringLength(1000)]
    public string? ContactInfo { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Industry { get; set; }

    public List<LeadDbModel>? Leads { get; set; } = new List<LeadDbModel>();

    public List<MeetingDbModel>? Meetings { get; set; } = new List<MeetingDbModel>();

    [StringLength(1000)]
    public string? Name { get; set; }

    public List<OpportunityDbModel>? Opportunities { get; set; } = new List<OpportunityDbModel>();

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
