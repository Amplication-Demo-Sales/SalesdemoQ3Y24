namespace EcommerceManagement.APIs.Dtos;

public class CommentWhereInput
{
    public string? Article { get; set; }

    public string? Content { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Id { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? User { get; set; }
}
