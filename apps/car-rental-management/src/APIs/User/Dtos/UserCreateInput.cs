namespace CarRentalManagementMobile.APIs.Dtos;

public class UserCreateInput
{
    public DateTime CreatedAt { get; set; }

    public string? Email { get; set; }

    public string? Id { get; set; }

    public string? Password { get; set; }

    public Role? Role { get; set; }

    public DateTime UpdatedAt { get; set; }

    public string? Username { get; set; }
}
