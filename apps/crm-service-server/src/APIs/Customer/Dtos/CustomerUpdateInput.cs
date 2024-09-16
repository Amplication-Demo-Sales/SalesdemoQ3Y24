namespace CrmService.APIs.Dtos;

public class CustomerUpdateInput
{
    public string? ContactInfo { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public string? Industry { get; set; }

    public List<string>? Leads { get; set; }

    public List<string>? Meetings { get; set; }

    public string? Name { get; set; }

    public List<string>? Opportunities { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
