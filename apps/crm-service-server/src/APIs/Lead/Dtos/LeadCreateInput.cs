namespace CrmService.APIs.Dtos;

public class LeadCreateInput
{
    public DateTime CreatedAt { get; set; }

    public Customer? Customer { get; set; }

    public string? Id { get; set; }

    public List<Meeting>? Meetings { get; set; }

    public string? Name { get; set; }

    public string? Priority { get; set; }

    public string? Status { get; set; }

    public DateTime UpdatedAt { get; set; }
}
