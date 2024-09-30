using EcommerceManagement.APIs;
using EcommerceManagement.APIs.Common;
using EcommerceManagement.APIs.Dtos;
using EcommerceManagement.APIs.Errors;
using EcommerceManagement.APIs.Extensions;
using EcommerceManagement.Infrastructure;
using EcommerceManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceManagement.APIs;

public abstract class UsersServiceBase : IUsersService
{
    protected readonly EcommerceManagementDbContext _context;

    public UsersServiceBase(EcommerceManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one User
    /// </summary>
    public async Task<User> CreateUser(UserCreateInput createDto)
    {
        var user = new UserDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Email = createDto.Email,
            FirstName = createDto.FirstName,
            LastName = createDto.LastName,
            Password = createDto.Password,
            Roles = createDto.Roles,
            UpdatedAt = createDto.UpdatedAt,
            Username = createDto.Username
        };

        if (createDto.Id != null)
        {
            user.Id = createDto.Id;
        }
        if (createDto.Articles != null)
        {
            user.Articles = await _context
                .Articles.Where(article =>
                    createDto.Articles.Select(t => t.Id).Contains(article.Id)
                )
                .ToListAsync();
        }

        if (createDto.Comments != null)
        {
            user.Comments = await _context
                .Comments.Where(comment =>
                    createDto.Comments.Select(t => t.Id).Contains(comment.Id)
                )
                .ToListAsync();
        }

        if (createDto.Profiles != null)
        {
            user.Profiles = await _context
                .Profiles.Where(profile =>
                    createDto.Profiles.Select(t => t.Id).Contains(profile.Id)
                )
                .ToListAsync();
        }

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<UserDbModel>(user.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one User
    /// </summary>
    public async Task DeleteUser(UserWhereUniqueInput uniqueId)
    {
        var user = await _context.Users.FindAsync(uniqueId.Id);
        if (user == null)
        {
            throw new NotFoundException();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Users
    /// </summary>
    public async Task<List<User>> Users(UserFindManyArgs findManyArgs)
    {
        var users = await _context
            .Users.Include(x => x.Articles)
            .Include(x => x.Profiles)
            .Include(x => x.Comments)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return users.ConvertAll(user => user.ToDto());
    }

    /// <summary>
    /// Meta data about User records
    /// </summary>
    public async Task<MetadataDto> UsersMeta(UserFindManyArgs findManyArgs)
    {
        var count = await _context.Users.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one User
    /// </summary>
    public async Task<User> User(UserWhereUniqueInput uniqueId)
    {
        var users = await this.Users(
            new UserFindManyArgs { Where = new UserWhereInput { Id = uniqueId.Id } }
        );
        var user = users.FirstOrDefault();
        if (user == null)
        {
            throw new NotFoundException();
        }

        return user;
    }

    /// <summary>
    /// Update one User
    /// </summary>
    public async Task UpdateUser(UserWhereUniqueInput uniqueId, UserUpdateInput updateDto)
    {
        var user = updateDto.ToModel(uniqueId);

        if (updateDto.Articles != null)
        {
            user.Articles = await _context
                .Articles.Where(article => updateDto.Articles.Select(t => t).Contains(article.Id))
                .ToListAsync();
        }

        if (updateDto.Comments != null)
        {
            user.Comments = await _context
                .Comments.Where(comment => updateDto.Comments.Select(t => t).Contains(comment.Id))
                .ToListAsync();
        }

        if (updateDto.Profiles != null)
        {
            user.Profiles = await _context
                .Profiles.Where(profile => updateDto.Profiles.Select(t => t).Contains(profile.Id))
                .ToListAsync();
        }

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Users.Any(e => e.Id == user.Id))
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
    /// Connect multiple Articles records to User
    /// </summary>
    public async Task ConnectArticles(
        UserWhereUniqueInput uniqueId,
        ArticleWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Users.Include(x => x.Articles)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Articles.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Articles);

        foreach (var child in childrenToConnect)
        {
            parent.Articles.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Articles records from User
    /// </summary>
    public async Task DisconnectArticles(
        UserWhereUniqueInput uniqueId,
        ArticleWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Users.Include(x => x.Articles)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Articles.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Articles?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Articles records for User
    /// </summary>
    public async Task<List<Article>> FindArticles(
        UserWhereUniqueInput uniqueId,
        ArticleFindManyArgs userFindManyArgs
    )
    {
        var articles = await _context
            .Articles.Where(m => m.UserId == uniqueId.Id)
            .ApplyWhere(userFindManyArgs.Where)
            .ApplySkip(userFindManyArgs.Skip)
            .ApplyTake(userFindManyArgs.Take)
            .ApplyOrderBy(userFindManyArgs.SortBy)
            .ToListAsync();

        return articles.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Articles records for User
    /// </summary>
    public async Task UpdateArticles(
        UserWhereUniqueInput uniqueId,
        ArticleWhereUniqueInput[] childrenIds
    )
    {
        var user = await _context
            .Users.Include(t => t.Articles)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (user == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Articles.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        user.Articles = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Connect multiple Comments records to User
    /// </summary>
    public async Task ConnectComments(
        UserWhereUniqueInput uniqueId,
        CommentWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Users.Include(x => x.Comments)
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
    /// Disconnect multiple Comments records from User
    /// </summary>
    public async Task DisconnectComments(
        UserWhereUniqueInput uniqueId,
        CommentWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Users.Include(x => x.Comments)
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
    /// Find multiple Comments records for User
    /// </summary>
    public async Task<List<Comment>> FindComments(
        UserWhereUniqueInput uniqueId,
        CommentFindManyArgs userFindManyArgs
    )
    {
        var comments = await _context
            .Comments.Where(m => m.UserId == uniqueId.Id)
            .ApplyWhere(userFindManyArgs.Where)
            .ApplySkip(userFindManyArgs.Skip)
            .ApplyTake(userFindManyArgs.Take)
            .ApplyOrderBy(userFindManyArgs.SortBy)
            .ToListAsync();

        return comments.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Comments records for User
    /// </summary>
    public async Task UpdateComments(
        UserWhereUniqueInput uniqueId,
        CommentWhereUniqueInput[] childrenIds
    )
    {
        var user = await _context
            .Users.Include(t => t.Comments)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (user == null)
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

        user.Comments = children;
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Connect multiple Profiles records to User
    /// </summary>
    public async Task ConnectProfiles(
        UserWhereUniqueInput uniqueId,
        ProfileWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Users.Include(x => x.Profiles)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Profiles.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Profiles);

        foreach (var child in childrenToConnect)
        {
            parent.Profiles.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Profiles records from User
    /// </summary>
    public async Task DisconnectProfiles(
        UserWhereUniqueInput uniqueId,
        ProfileWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Users.Include(x => x.Profiles)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Profiles.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Profiles?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Profiles records for User
    /// </summary>
    public async Task<List<Profile>> FindProfiles(
        UserWhereUniqueInput uniqueId,
        ProfileFindManyArgs userFindManyArgs
    )
    {
        var profiles = await _context
            .Profiles.Where(m => m.UserId == uniqueId.Id)
            .ApplyWhere(userFindManyArgs.Where)
            .ApplySkip(userFindManyArgs.Skip)
            .ApplyTake(userFindManyArgs.Take)
            .ApplyOrderBy(userFindManyArgs.SortBy)
            .ToListAsync();

        return profiles.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Profiles records for User
    /// </summary>
    public async Task UpdateProfiles(
        UserWhereUniqueInput uniqueId,
        ProfileWhereUniqueInput[] childrenIds
    )
    {
        var user = await _context
            .Users.Include(t => t.Profiles)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (user == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Profiles.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        user.Profiles = children;
        await _context.SaveChangesAsync();
    }
}
