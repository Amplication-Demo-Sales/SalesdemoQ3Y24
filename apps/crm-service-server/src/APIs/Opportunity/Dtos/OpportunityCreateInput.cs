namespace CrmService.APIs.Dtos;

public class OpportunityCreateInput
{
    public DateTime CreatedAt { get; set; }

    public Customer? Customer { get; set; }

    public string? Description { get; set; }

    public string? Id { get; set; }

    public double? PotentialRevenue { get; set; }

    public string? Status { get; set; }

    public DateTime UpdatedAt { get; set; }
}
