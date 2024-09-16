using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingService.Infrastructure.Models;

[Table("Rooms")]
public class RoomDbModel
{
    public List<BookingDbModel>? Bookings { get; set; } = new List<BookingDbModel>();

    [Range(-999999999, 999999999)]
    public int? Capacity { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [StringLength(1000)]
    public string? Features { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(1000)]
    public string? RoomNumber { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
