using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingService.Infrastructure.Models;

[Table("Payments")]
public class PaymentDbModel
{
    [Range(-999999999, 999999999)]
    public double? Amount { get; set; }

    public string? BookingId { get; set; }

    [ForeignKey(nameof(BookingId))]
    public BookingDbModel? Booking { get; set; } = null;

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    public DateTime? PaymentDate { get; set; }

    [StringLength(1000)]
    public string? PaymentMethod { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }
}
