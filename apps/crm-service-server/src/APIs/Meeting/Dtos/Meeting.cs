namespace CrmService.APIs.Dtos;

public class Meeting
{
    public DateTime CreatedAt { get; set; }

    public string? Customer { get; set; }

    public DateTime? Date { get; set; }

    public string Id { get; set; }

    public string? Lead { get; set; }

    public string? Location { get; set; }

    public string? Notes { get; set; }

    public string? Subject { get; set; }

    public DateTime UpdatedAt { get; set; }
}
