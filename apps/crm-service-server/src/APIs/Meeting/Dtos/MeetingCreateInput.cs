namespace CrmService.APIs.Dtos;

public class MeetingCreateInput
{
    public DateTime CreatedAt { get; set; }

    public Customer? Customer { get; set; }

    public DateTime? Date { get; set; }

    public string? Id { get; set; }

    public Lead? Lead { get; set; }

    public string? Location { get; set; }

    public string? Notes { get; set; }

    public string? Subject { get; set; }

    public DateTime UpdatedAt { get; set; }
}
