namespace BookingService.APIs.Dtos;

public class CustomerCreateInput
{
    public List<Booking>? Bookings { get; set; }

    public string? ContactInfo { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? Name { get; set; }

    public DateTime UpdatedAt { get; set; }
}
