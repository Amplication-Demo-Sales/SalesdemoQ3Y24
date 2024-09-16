namespace BookingService.APIs.Dtos;

public class BookingWhereInput
{
    public DateTime? BookingDate { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Customer { get; set; }

    public string? Event { get; set; }

    public string? Id { get; set; }

    public List<string>? Payments { get; set; }

    public string? Room { get; set; }

    public string? Status { get; set; }

    public double? TotalAmount { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
