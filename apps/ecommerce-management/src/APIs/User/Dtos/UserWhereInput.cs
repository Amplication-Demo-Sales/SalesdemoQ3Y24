namespace EcommerceManagement.APIs.Dtos;

public class UserWhereInput
{
    public List<string>? Articles { get; set; }

    public List<string>? Comments { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Email { get; set; }

    public string? FirstName { get; set; }

    public string? Id { get; set; }

    public string? LastName { get; set; }

    public string? Password { get; set; }

    public List<string>? Profiles { get; set; }

    public string? Roles { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Username { get; set; }
}
