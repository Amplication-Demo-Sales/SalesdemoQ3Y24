using EcommerceManagement.APIs.Common;
using EcommerceManagement.APIs.Dtos;

namespace EcommerceManagement.APIs;

public interface IUsersService
{
    /// <summary>
    /// Create one User
    /// </summary>
    public Task<User> CreateUser(UserCreateInput user);

    /// <summary>
    /// Delete one User
    /// </summary>
    public Task DeleteUser(UserWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Users
    /// </summary>
    public Task<List<User>> Users(UserFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about User records
    /// </summary>
    public Task<MetadataDto> UsersMeta(UserFindManyArgs findManyArgs);

    /// <summary>
    /// Get one User
    /// </summary>
    public Task<User> User(UserWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one User
    /// </summary>
    public Task UpdateUser(UserWhereUniqueInput uniqueId, UserUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Articles records to User
    /// </summary>
    public Task ConnectArticles(
        UserWhereUniqueInput uniqueId,
        ArticleWhereUniqueInput[] articlesId
    );

    /// <summary>
    /// Disconnect multiple Articles records from User
    /// </summary>
    public Task DisconnectArticles(
        UserWhereUniqueInput uniqueId,
        ArticleWhereUniqueInput[] articlesId
    );

    /// <summary>
    /// Find multiple Articles records for User
    /// </summary>
    public Task<List<Article>> FindArticles(
        UserWhereUniqueInput uniqueId,
        ArticleFindManyArgs ArticleFindManyArgs
    );

    /// <summary>
    /// Update multiple Articles records for User
    /// </summary>
    public Task UpdateArticles(UserWhereUniqueInput uniqueId, ArticleWhereUniqueInput[] articlesId);

    /// <summary>
    /// Connect multiple Comments records to User
    /// </summary>
    public Task ConnectComments(
        UserWhereUniqueInput uniqueId,
        CommentWhereUniqueInput[] commentsId
    );

    /// <summary>
    /// Disconnect multiple Comments records from User
    /// </summary>
    public Task DisconnectComments(
        UserWhereUniqueInput uniqueId,
        CommentWhereUniqueInput[] commentsId
    );

    /// <summary>
    /// Find multiple Comments records for User
    /// </summary>
    public Task<List<Comment>> FindComments(
        UserWhereUniqueInput uniqueId,
        CommentFindManyArgs CommentFindManyArgs
    );

    /// <summary>
    /// Update multiple Comments records for User
    /// </summary>
    public Task UpdateComments(UserWhereUniqueInput uniqueId, CommentWhereUniqueInput[] commentsId);

    /// <summary>
    /// Connect multiple Profiles records to User
    /// </summary>
    public Task ConnectProfiles(
        UserWhereUniqueInput uniqueId,
        ProfileWhereUniqueInput[] profilesId
    );

    /// <summary>
    /// Disconnect multiple Profiles records from User
    /// </summary>
    public Task DisconnectProfiles(
        UserWhereUniqueInput uniqueId,
        ProfileWhereUniqueInput[] profilesId
    );

    /// <summary>
    /// Find multiple Profiles records for User
    /// </summary>
    public Task<List<Profile>> FindProfiles(
        UserWhereUniqueInput uniqueId,
        ProfileFindManyArgs ProfileFindManyArgs
    );

    /// <summary>
    /// Update multiple Profiles records for User
    /// </summary>
    public Task UpdateProfiles(UserWhereUniqueInput uniqueId, ProfileWhereUniqueInput[] profilesId);
}
