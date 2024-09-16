using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrmService.Infrastructure.Models;

[Table("Meetings")]
public class MeetingDbModel
{
    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? CustomerId { get; set; }

    [ForeignKey(nameof(CustomerId))]
    public CustomerDbModel? Customer { get; set; } = null;

    public DateTime? Date { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public string? LeadId { get; set; }

    [ForeignKey(nameof(LeadId))]
    public LeadDbModel? Lead { get; set; } = null;

    [StringLength(1000)]
    public string? Location { get; set; }

    [StringLength(1000)]
    public string? Notes { get; set; }

    [StringLength(1000)]
    public string? Subject { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
