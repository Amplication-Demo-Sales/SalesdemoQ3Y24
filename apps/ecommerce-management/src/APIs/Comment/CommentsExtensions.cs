using EcommerceManagement.APIs.Dtos;
using EcommerceManagement.Infrastructure.Models;

namespace EcommerceManagement.APIs.Extensions;

public static class CommentsExtensions
{
    public static Comment ToDto(this CommentDbModel model)
    {
        return new Comment
        {
            Article = model.ArticleId,
            Content = model.Content,
            CreatedAt = model.CreatedAt,
            Id = model.Id,
            UpdatedAt = model.UpdatedAt,
            User = model.UserId,
        };
    }

    public static CommentDbModel ToModel(
        this CommentUpdateInput updateDto,
        CommentWhereUniqueInput uniqueId
    )
    {
        var comment = new CommentDbModel { Id = uniqueId.Id, Content = updateDto.Content };

        if (updateDto.Article != null)
        {
            comment.ArticleId = updateDto.Article;
        }
        if (updateDto.CreatedAt != null)
        {
            comment.CreatedAt = updateDto.CreatedAt.Value;
        }
        if (updateDto.UpdatedAt != null)
        {
            comment.UpdatedAt = updateDto.UpdatedAt.Value;
        }
        if (updateDto.User != null)
        {
            comment.UserId = updateDto.User;
        }

        return comment;
    }
}
