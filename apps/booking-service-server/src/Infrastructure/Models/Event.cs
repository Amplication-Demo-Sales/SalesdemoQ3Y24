using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingService.Infrastructure.Models;

[Table("Events")]
public class EventDbModel
{
    public List<BookingDbModel>? Bookings { get; set; } = new List<BookingDbModel>();

    [Required()]
    public DateTime CreatedAt { get; set; }

    public DateTime? Date { get; set; }

    [StringLength(1000)]
    public string? EventName { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? Location { get; set; }

    [StringLength(1000)]
    public string? Notes { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
