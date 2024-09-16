namespace CrmService.APIs.Dtos;

public class CustomerCreateInput
{
    public string? ContactInfo { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? Industry { get; set; }

    public List<Lead>? Leads { get; set; }

    public List<Meeting>? Meetings { get; set; }

    public string? Name { get; set; }

    public List<Opportunity>? Opportunities { get; set; }

    public DateTime UpdatedAt { get; set; }
}
