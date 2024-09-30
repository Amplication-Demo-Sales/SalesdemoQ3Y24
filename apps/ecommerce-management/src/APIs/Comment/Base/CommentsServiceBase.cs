using EcommerceManagement.APIs;
using EcommerceManagement.APIs.Common;
using EcommerceManagement.APIs.Dtos;
using EcommerceManagement.APIs.Errors;
using EcommerceManagement.APIs.Extensions;
using EcommerceManagement.Infrastructure;
using EcommerceManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceManagement.APIs;

public abstract class CommentsServiceBase : ICommentsService
{
    protected readonly EcommerceManagementDbContext _context;

    public CommentsServiceBase(EcommerceManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Comment
    /// </summary>
    public async Task<Comment> CreateComment(CommentCreateInput createDto)
    {
        var comment = new CommentDbModel
        {
            Content = createDto.Content,
            CreatedAt = createDto.CreatedAt,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            comment.Id = createDto.Id;
        }
        if (createDto.Article != null)
        {
            comment.Article = await _context
                .Articles.Where(article => createDto.Article.Id == article.Id)
                .FirstOrDefaultAsync();
        }

        if (createDto.User != null)
        {
            comment.User = await _context
                .Users.Where(user => createDto.User.Id == user.Id)
                .FirstOrDefaultAsync();
        }

        _context.Comments.Add(comment);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<CommentDbModel>(comment.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Comment
    /// </summary>
    public async Task DeleteComment(CommentWhereUniqueInput uniqueId)
    {
        var comment = await _context.Comments.FindAsync(uniqueId.Id);
        if (comment == null)
        {
            throw new NotFoundException();
        }

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Comments
    /// </summary>
    public async Task<List<Comment>> Comments(CommentFindManyArgs findManyArgs)
    {
        var comments = await _context
            .Comments.Include(x => x.Article)
            .Include(x => x.User)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return comments.ConvertAll(comment => comment.ToDto());
    }

    /// <summary>
    /// Meta data about Comment records
    /// </summary>
    public async Task<MetadataDto> CommentsMeta(CommentFindManyArgs findManyArgs)
    {
        var count = await _context.Comments.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Comment
    /// </summary>
    public async Task<Comment> Comment(CommentWhereUniqueInput uniqueId)
    {
        var comments = await this.Comments(
            new CommentFindManyArgs { Where = new CommentWhereInput { Id = uniqueId.Id } }
        );
        var comment = comments.FirstOrDefault();
        if (comment == null)
        {
            throw new NotFoundException();
        }

        return comment;
    }

    /// <summary>
    /// Update one Comment
    /// </summary>
    public async Task UpdateComment(CommentWhereUniqueInput uniqueId, CommentUpdateInput updateDto)
    {
        var comment = updateDto.ToModel(uniqueId);

        if (updateDto.Article != null)
        {
            comment.Article = await _context
                .Articles.Where(article => updateDto.Article == article.Id)
                .FirstOrDefaultAsync();
        }

        if (updateDto.User != null)
        {
            comment.User = await _context
                .Users.Where(user => updateDto.User == user.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(comment).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Comments.Any(e => e.Id == comment.Id))
            {
                throw new NotFoundException();
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// Get a Article record for Comment
    /// </summary>
    public async Task<Article> GetArticle(CommentWhereUniqueInput uniqueId)
    {
        var comment = await _context
            .Comments.Where(comment => comment.Id == uniqueId.Id)
            .Include(comment => comment.Article)
            .FirstOrDefaultAsync();
        if (comment == null)
        {
            throw new NotFoundException();
        }
        return comment.Article.ToDto();
    }

    /// <summary>
    /// Get a User record for Comment
    /// </summary>
    public async Task<User> GetUser(CommentWhereUniqueInput uniqueId)
    {
        var comment = await _context
            .Comments.Where(comment => comment.Id == uniqueId.Id)
            .Include(comment => comment.User)
            .FirstOrDefaultAsync();
        if (comment == null)
        {
            throw new NotFoundException();
        }
        return comment.User.ToDto();
    }
}
