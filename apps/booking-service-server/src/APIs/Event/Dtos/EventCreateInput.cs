namespace BookingService.APIs.Dtos;

public class EventCreateInput
{
    public List<Booking>? Bookings { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? Date { get; set; }

    public string? EventName { get; set; }

    public string? Id { get; set; }

    public string? Location { get; set; }

    public string? Notes { get; set; }

    public DateTime UpdatedAt { get; set; }
}
