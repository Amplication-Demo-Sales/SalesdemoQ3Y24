namespace CrmService.APIs.Dtos;

public class Opportunity
{
    public DateTime CreatedAt { get; set; }

    public string? Customer { get; set; }

    public string? Description { get; set; }

    public string Id { get; set; }

    public double? PotentialRevenue { get; set; }

    public string? Status { get; set; }

    public DateTime UpdatedAt { get; set; }
}
