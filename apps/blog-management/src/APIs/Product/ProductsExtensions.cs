using BlogManagement.APIs.Dtos;
using BlogManagement.Infrastructure.Models;

namespace BlogManagement.APIs.Extensions;

public static class ProductsExtensions
{
    public static Product ToDto(this ProductDbModel model)
    {
        return new Product
        {
            Category = model.CategoryId,
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            Name = model.Name,
            Price = model.Price,
            UpdatedAt = model.UpdatedAt,
        };
    }

    public static ProductDbModel ToModel(
        this ProductUpdateInput updateDto,
        ProductWhereUniqueInput uniqueId
    )
    {
        var product = new ProductDbModel
        {
            Id = uniqueId.Id,
            Name = updateDto.Name,
            Price = updateDto.Price
        };

        if (updateDto.Category != null)
        {
            product.CategoryId = updateDto.Category;
        }
        if (updateDto.CreatedAt != null)
        {
            product.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            product.UpdatedAt = updateDto.UpdatedAt.Value;
        }

        return product;
    }
}
