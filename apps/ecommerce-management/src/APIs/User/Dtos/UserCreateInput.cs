namespace EcommerceManagement.APIs.Dtos;

public class UserCreateInput
{
    public List<Article>? Articles { get; set; }

    public List<Comment>? Comments { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    public string? FirstName { get; set; }

    public string? Id { get; set; }

    public string? LastName { get; set; }

    public string Password { get; set; }

    public List<Profile>? Profiles { get; set; }

    public string Roles { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string Username { get; set; }
}
