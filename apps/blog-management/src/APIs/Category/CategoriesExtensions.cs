using BlogManagement.APIs.Dtos;
using BlogManagement.Infrastructure.Models;

namespace BlogManagement.APIs.Extensions;

public static class CategoriesExtensions
{
    public static Category ToDto(this CategoryDbModel model)
    {
        return new Category
        {
            CreatedAt = model.CreatedAt,
            Description = model.Description,
            Id = model.Id,
            Name = model.Name,
            Products = model.Products?.Select(x => x.Id).ToList(),
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static CategoryDbModel ToModel(
        this CategoryUpdateInput updateDto,
        CategoryWhereUniqueInput uniqueId
    )
    {
        var category = new CategoryDbModel
        {
            Id = uniqueId.Id,
            Description = updateDto.Description,
            Name = updateDto.Name
        };

        if (updateDto.CreatedAt != null)
        {
            category.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            category.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return category;
    }
}
