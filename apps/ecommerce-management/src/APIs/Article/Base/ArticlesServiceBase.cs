using EcommerceManagement.APIs;
using EcommerceManagement.APIs.Common;
using EcommerceManagement.APIs.Dtos;
using EcommerceManagement.APIs.Errors;
using EcommerceManagement.APIs.Extensions;
using EcommerceManagement.Infrastructure;
using EcommerceManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceManagement.APIs;

public abstract class ArticlesServiceBase : IArticlesService
{
    protected readonly EcommerceManagementDbContext _context;

    public ArticlesServiceBase(EcommerceManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Article
    /// </summary>
    public async Task<Article> CreateArticle(ArticleCreateInput createDto)
    {
        var article = new ArticleDbModel
        {
            Content = createDto.Content,
            CreatedAt = createDto.CreatedAt,
            PublishedAt = createDto.PublishedAt,
            Title = createDto.Title,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            article.Id = createDto.Id;
        }
        if (createDto.Comments != null)
        {
            article.Comments = await _context
                .Comments.Where(comment =>
                    createDto.Comments.Select(t => t.Id).Contains(comment.Id)
                )
                .ToListAsync();
        }

        if (createDto.User != null)
        {
            article.User = await _context
                .Users.Where(user => createDto.User.Id == user.Id)
                .FirstOrDefaultAsync();
        }

        _context.Articles.Add(article);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ArticleDbModel>(article.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Article
    /// </summary>
    public async Task DeleteArticle(ArticleWhereUniqueInput uniqueId)
    {
        var article = await _context.Articles.FindAsync(uniqueId.Id);
        if (article == null)
        {
            throw new NotFoundException();
        }

        _context.Articles.Remove(article);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Articles
    /// </summary>
    public async Task<List<Article>> Articles(ArticleFindManyArgs findManyArgs)
    {
        var articles = await _context
            .Articles.Include(x => x.Comments)
            .Include(x => x.User)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return articles.ConvertAll(article => article.ToDto());
    }

    /// <summary>
    /// Meta data about Article records
    /// </summary>
    public async Task<MetadataDto> ArticlesMeta(ArticleFindManyArgs findManyArgs)
    {
        var count = await _context.Articles.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Article
    /// </summary>
    public async Task<Article> Article(ArticleWhereUniqueInput uniqueId)
    {
        var articles = await this.Articles(
            new ArticleFindManyArgs { Where = new ArticleWhereInput { Id = uniqueId.Id } }
        );
        var article = articles.FirstOrDefault();
        if (article == null)
        {
            throw new NotFoundException();
        }

        return article;
    }

    /// <summary>
    /// Update one Article
    /// </summary>
    public async Task UpdateArticle(ArticleWhereUniqueInput uniqueId, ArticleUpdateInput updateDto)
    {
        var article = updateDto.ToModel(uniqueId);

        if (updateDto.Comments != null)
        {
            article.Comments = await _context
                .Comments.Where(comment => updateDto.Comments.Select(t => t).Contains(comment.Id))
                .ToListAsync();
        }

        if (updateDto.User != null)
        {
            article.User = await _context
                .Users.Where(user => updateDto.User == user.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(article).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Articles.Any(e => e.Id == article.Id))
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
    /// Connect multiple Comments records to Article
    /// </summary>
    public async Task ConnectComments(
        ArticleWhereUniqueInput uniqueId,
        CommentWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Articles.Include(x => x.Comments)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Comments.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Comments);

        foreach (var child in childrenToConnect)
        {
            parent.Comments.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Comments records from Article
    /// </summary>
    public async Task DisconnectComments(
        ArticleWhereUniqueInput uniqueId,
        CommentWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Articles.Include(x => x.Comments)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Comments.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Comments?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Comments records for Article
    /// </summary>
    public async Task<List<Comment>> FindComments(
        ArticleWhereUniqueInput uniqueId,
        CommentFindManyArgs articleFindManyArgs
    )
    {
        var comments = await _context
            .Comments.Where(m => m.ArticleId == uniqueId.Id)
            .ApplyWhere(articleFindManyArgs.Where)
            .ApplySkip(articleFindManyArgs.Skip)
            .ApplyTake(articleFindManyArgs.Take)
            .ApplyOrderBy(articleFindManyArgs.SortBy)
            .ToListAsync();

        return comments.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Comments records for Article
    /// </summary>
    public async Task UpdateComments(
        ArticleWhereUniqueInput uniqueId,
        CommentWhereUniqueInput[] childrenIds
    )
    {
        var article = await _context
            .Articles.Include(t => t.Comments)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (article == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Comments.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        article.Comments = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Get a User record for Article
    /// </summary>
    public async Task<User> GetUser(ArticleWhereUniqueInput uniqueId)
    {
        var article = await _context
            .Articles.Where(article => article.Id == uniqueId.Id)
            .Include(article => article.User)
            .FirstOrDefaultAsync();
        if (article == null)
        {
            throw new NotFoundException();
        }
        return article.User.ToDto();
    }
}
