namespace EcommerceManagement.APIs.Dtos;

public class CommentCreateInput
{
    public Article? Article { get; set; }

    public string? Content { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Id { get; set; }

    public DateTime UpdatedAt { get; set; }

    public User? User { get; set; }
}
