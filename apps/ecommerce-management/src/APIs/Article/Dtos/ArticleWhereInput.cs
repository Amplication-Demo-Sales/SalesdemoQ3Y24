namespace EcommerceManagement.APIs.Dtos;

public class ArticleWhereInput
{
    public List<string>? Comments { get; set; }

    public string? Content { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public DateTime? PublishedAt { get; set; }

    public string? Title { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? User { get; set; }
}
