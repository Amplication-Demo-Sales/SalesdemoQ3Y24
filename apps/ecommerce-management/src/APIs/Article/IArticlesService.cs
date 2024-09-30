using EcommerceManagement.APIs.Common;
using EcommerceManagement.APIs.Dtos;

namespace EcommerceManagement.APIs;

public interface IArticlesService
{
    /// <summary>
    /// Create one Article
    /// </summary>
    public Task<Article> CreateArticle(ArticleCreateInput article);

    /// <summary>
    /// Delete one Article
    /// </summary>
    public Task DeleteArticle(ArticleWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Articles
    /// </summary>
    public Task<List<Article>> Articles(ArticleFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Article records
    /// </summary>
    public Task<MetadataDto> ArticlesMeta(ArticleFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Article
    /// </summary>
    public Task<Article> Article(ArticleWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Article
    /// </summary>
    public Task UpdateArticle(ArticleWhereUniqueInput uniqueId, ArticleUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Comments records to Article
    /// </summary>
    public Task ConnectComments(
        ArticleWhereUniqueInput uniqueId,
        CommentWhereUniqueInput[] commentsId
    );

    /// <summary>
    /// Disconnect multiple Comments records from Article
    /// </summary>
    public Task DisconnectComments(
        ArticleWhereUniqueInput uniqueId,
        CommentWhereUniqueInput[] commentsId
    );

    /// <summary>
    /// Find multiple Comments records for Article
    /// </summary>
    public Task<List<Comment>> FindComments(
        ArticleWhereUniqueInput uniqueId,
        CommentFindManyArgs CommentFindManyArgs
    );

    /// <summary>
    /// Update multiple Comments records for Article
    /// </summary>
    public Task UpdateComments(
        ArticleWhereUniqueInput uniqueId,
        CommentWhereUniqueInput[] commentsId
    );

    /// <summary>
    /// Get a User record for Article
    /// </summary>
    public Task<User> GetUser(ArticleWhereUniqueInput uniqueId);
}
