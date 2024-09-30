using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceManagement.Infrastructure.Models;

[Table("Comments")]
public class CommentDbModel
{
    public string? ArticleId { get; set; }

    [ForeignKey(nameof(ArticleId))]
    public ArticleDbModel? Article { get; set; } = null;

    [StringLength(1000)]
    public string? Content { get; set; }

    [Required()]
    public DateTime CreatedAt { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    public string? UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public UserDbModel? User { get; set; } = null;
}
