using EcommerceManagement.APIs.Dtos;
using EcommerceManagement.Infrastructure.Models;

namespace EcommerceManagement.APIs.Extensions;

public static class ProfilesExtensions
{
    public static Profile ToDto(this ProfileDbModel model)
    {
        return new Profile
        {
            Bio = model.Bio,
            CreatedAt = model.CreatedAt,
            FirstName = model.FirstName,
            Id = model.Id,
            LastName = model.LastName,
            UpdatedAt = model.UpdatedAt,
            User = model.UserId,
        };
    }

    public static ProfileDbModel ToModel(
        this ProfileUpdateInput updateDto,
        ProfileWhereUniqueInput uniqueId
    )
    {
        var profile = new ProfileDbModel
        {
            Id = uniqueId.Id,
            Bio = updateDto.Bio,
            FirstName = updateDto.FirstName,
            LastName = updateDto.LastName
        };

        if (updateDto.CreatedAt != null)
        {
            profile.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            profile.UpdatedAt = updateDto.UpdatedAt.Value;
        }
        if (updateDto.User != null)
        {
            profile.UserId = updateDto.User;
        }

        return profile;
    }
}
