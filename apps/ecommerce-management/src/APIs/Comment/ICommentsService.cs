using EcommerceManagement.APIs.Common;
using EcommerceManagement.APIs.Dtos;

namespace EcommerceManagement.APIs;

public interface ICommentsService
{
    /// <summary>
    /// Create one Comment
    /// </summary>
    public Task<Comment> CreateComment(CommentCreateInput comment);

    /// <summary>
    /// Delete one Comment
    /// </summary>
    public Task DeleteComment(CommentWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Comments
    /// </summary>
    public Task<List<Comment>> Comments(CommentFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Comment records
    /// </summary>
    public Task<MetadataDto> CommentsMeta(CommentFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Comment
    /// </summary>
    public Task<Comment> Comment(CommentWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Comment
    /// </summary>
    public Task UpdateComment(CommentWhereUniqueInput uniqueId, CommentUpdateInput updateDto);

    /// <summary>
    /// Get a Article record for Comment
    /// </summary>
    public Task<Article> GetArticle(CommentWhereUniqueInput uniqueId);

    /// <summary>
    /// Get a User record for Comment
    /// </summary>
    public Task<User> GetUser(CommentWhereUniqueInput uniqueId);
}
