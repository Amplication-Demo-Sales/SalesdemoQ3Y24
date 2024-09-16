namespace CrmService.APIs.Dtos;

public class Lead
{
    public DateTime CreatedAt { get; set; }

    public string? Customer { get; set; }

    public string Id { get; set; }

    public List<string>? Meetings { get; set; }

    public string? Name { get; set; }

    public string? Priority { get; set; }

    public string? Status { get; set; }

    public DateTime UpdatedAt { get; set; }
}
