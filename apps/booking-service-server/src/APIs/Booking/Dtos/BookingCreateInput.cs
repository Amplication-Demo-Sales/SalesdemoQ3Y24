namespace BookingService.APIs.Dtos;

public class BookingCreateInput
{
    public DateTime? BookingDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public Customer? Customer { get; set; }

    public Event? Event { get; set; }

    public string? Id { get; set; }

    public List<Payment>? Payments { get; set; }

    public Room? Room { get; set; }

    public string? Status { get; set; }

    public double? TotalAmount { get; set; }

    public DateTime UpdatedAt { get; set; }
}
