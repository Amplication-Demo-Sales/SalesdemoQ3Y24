namespace EcommerceManagement.APIs.Dtos;

public class ProfileCreateInput
{
    public string? Bio { get; set; }

    public DateTime CreatedAt { get; set; }

    public string? FirstName { get; set; }

    public string? Id { get; set; }

    public string? LastName { get; set; }

    public DateTime UpdatedAt { get; set; }

    public User? User { get; set; }
}
