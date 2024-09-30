using BlogManagement.APIs;
using BlogManagement.APIs.Common;
using BlogManagement.APIs.Dtos;
using BlogManagement.APIs.Errors;
using BlogManagement.APIs.Extensions;
using BlogManagement.Infrastructure;
using BlogManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.APIs;

public abstract class CategoriesServiceBase : ICategoriesService
{
    protected readonly BlogManagementDbContext _context;

    public CategoriesServiceBase(BlogManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Category
    /// </summary>
    public async Task<Category> CreateCategory(CategoryCreateInput createDto)
    {
        var category = new CategoryDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Description = createDto.Description,
            Name = createDto.Name,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            category.Id = createDto.Id;
        }
        if (createDto.Products != null)
        {
            category.Products = await _context
                .Products.Where(product =>
                    createDto.Products.Select(t => t.Id).Contains(product.Id)
                )
                .ToListAsync();
        }

        _context.Categories.Add(category);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<CategoryDbModel>(category.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Category
    /// </summary>
    public async Task DeleteCategory(CategoryWhereUniqueInput uniqueId)
    {
        var category = await _context.Categories.FindAsync(uniqueId.Id);
        if (category == null)
        {
            throw new NotFoundException();
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Categories
    /// </summary>
    public async Task<List<Category>> Categories(CategoryFindManyArgs findManyArgs)
    {
        var categories = await _context
            .Categories.Include(x => x.Products)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return categories.ConvertAll(category => category.ToDto());
    }

    /// <summary>
    /// Meta data about Category records
    /// </summary>
    public async Task<MetadataDto> CategoriesMeta(CategoryFindManyArgs findManyArgs)
    {
        var count = await _context.Categories.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Category
    /// </summary>
    public async Task<Category> Category(CategoryWhereUniqueInput uniqueId)
    {
        var categories = await this.Categories(
            new CategoryFindManyArgs { Where = new CategoryWhereInput { Id = uniqueId.Id } }
        );
        var category = categories.FirstOrDefault();
        if (category == null)
        {
            throw new NotFoundException();
        }

        return category;
    }

    /// <summary>
    /// Update one Category
    /// </summary>
    public async Task UpdateCategory(
        CategoryWhereUniqueInput uniqueId,
        CategoryUpdateInput updateDto
    )
    {
        var category = updateDto.ToModel(uniqueId);

        if (updateDto.Products != null)
        {
            category.Products = await _context
                .Products.Where(product => updateDto.Products.Select(t => t).Contains(product.Id))
                .ToListAsync();
        }

        _context.Entry(category).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Categories.Any(e => e.Id == category.Id))
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
    /// Connect multiple Products records to Category
    /// </summary>
    public async Task ConnectProducts(
        CategoryWhereUniqueInput uniqueId,
        ProductWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Categories.Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Products.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();
        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        var childrenToConnect = children.Except(parent.Products);

        foreach (var child in childrenToConnect)
        {
            parent.Products.Add(child);
        }

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Disconnect multiple Products records from Category
    /// </summary>
    public async Task DisconnectProducts(
        CategoryWhereUniqueInput uniqueId,
        ProductWhereUniqueInput[] childrenIds
    )
    {
        var parent = await _context
            .Categories.Include(x => x.Products)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (parent == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Products.Where(t => childrenIds.Select(x => x.Id).Contains(t.Id))
            .ToListAsync();

        foreach (var child in children)
        {
            parent.Products?.Remove(child);
        }
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find multiple Products records for Category
    /// </summary>
    public async Task<List<Product>> FindProducts(
        CategoryWhereUniqueInput uniqueId,
        ProductFindManyArgs categoryFindManyArgs
    )
    {
        var products = await _context
            .Products.Where(m => m.CategoryId == uniqueId.Id)
            .ApplyWhere(categoryFindManyArgs.Where)
            .ApplySkip(categoryFindManyArgs.Skip)
            .ApplyTake(categoryFindManyArgs.Take)
            .ApplyOrderBy(categoryFindManyArgs.SortBy)
            .ToListAsync();

        return products.Select(x => x.ToDto()).ToList();
    }

    /// <summary>
    /// Update multiple Products records for Category
    /// </summary>
    public async Task UpdateProducts(
        CategoryWhereUniqueInput uniqueId,
        ProductWhereUniqueInput[] childrenIds
    )
    {
        var category = await _context
            .Categories.Include(t => t.Products)
            .FirstOrDefaultAsync(x => x.Id == uniqueId.Id);
        if (category == null)
        {
            throw new NotFoundException();
        }

        var children = await _context
            .Products.Where(a => childrenIds.Select(x => x.Id).Contains(a.Id))
            .ToListAsync();

        if (children.Count == 0)
        {
            throw new NotFoundException();
        }

        category.Products = children;
        await _context.SaveChangesAsync();
    }
}
