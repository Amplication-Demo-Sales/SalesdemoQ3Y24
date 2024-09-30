namespace BlogManagement.APIs.Dtos;

public class Category
{
    public DateTime CreatedAt { get; set; }

    public string? Description { get; set; }

    public string Id { get; set; }

    public string? Name { get; set; }

    public List<string>? Products { get; set; }

    public DateTime UpdatedAt { get; set; }
}
