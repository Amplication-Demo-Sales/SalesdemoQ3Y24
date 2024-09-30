using EcommerceManagement.APIs.Dtos;
using EcommerceManagement.Infrastructure.Models;

namespace EcommerceManagement.APIs.Extensions;

public static class ArticlesExtensions
{
    public static Article ToDto(this ArticleDbModel model)
    {
        return new Article
        {
            Comments = model.Comments?.Select(x => x.Id).ToList(),
            Content = model.Content,
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            PublishedAt = model.PublishedAt,
            Title = model.Title,
            UpdatedAt = model.UpdatedAt,
            User = model.UserId,
        };
    }

    public static ArticleDbModel ToModel(
        this ArticleUpdateInput updateDto,
        ArticleWhereUniqueInput uniqueId
    )
    {
        var article = new ArticleDbModel
        {
            Id = uniqueId.Id,
            Content = updateDto.Content,
            PublishedAt = updateDto.PublishedAt,
            Title = updateDto.Title
        };

        if (updateDto.CreatedAt != null)
        {
            article.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            article.UpdatedAt = updateDto.UpdatedAt.Value;
        }
        if (updateDto.User != null)
        {
            article.UserId = updateDto.User;
        }

        return article;
    }
}
