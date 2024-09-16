namespace BookingService.APIs.Dtos;

public class RoomUpdateInput
{
    public List<string>? Bookings { get; set; }

    public int? Capacity { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Features { get; set; }

    public string? Id { get; set; }

    public string? RoomNumber { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
