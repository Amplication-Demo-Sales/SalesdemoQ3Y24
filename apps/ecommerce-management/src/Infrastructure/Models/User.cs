using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceManagement.Infrastructure.Models;

[Table("Users")]
public class UserDbModel
{
    public List<ArticleDbModel>? Articles { get; set; } = new List<ArticleDbModel>();

    public List<CommentDbModel>? Comments { get; set; } = new List<CommentDbModel>();

    [Required()]
    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    [StringLength(256)]
    public string? FirstName { get; set; }

    [Key()]
    [Required()]
    public string Id { get; set; }

    [StringLength(256)]
    public string? LastName { get; set; }

    [Required()]
    public string Password { get; set; }

    public List<ProfileDbModel>? Profiles { get; set; } = new List<ProfileDbModel>();

    [Required()]
    public string Roles { get; set; }

    [Required()]
    public DateTime UpdatedAt { get; set; }

    [Required()]
    public string Username { get; set; }
}
