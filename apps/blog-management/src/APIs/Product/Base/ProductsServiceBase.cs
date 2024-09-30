using BlogManagement.APIs;
using BlogManagement.APIs.Common;
using BlogManagement.APIs.Dtos;
using BlogManagement.APIs.Errors;
using BlogManagement.APIs.Extensions;
using BlogManagement.Infrastructure;
using BlogManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogManagement.APIs;

public abstract class ProductsServiceBase : IProductsService
{
    protected readonly BlogManagementDbContext _context;

    public ProductsServiceBase(BlogManagementDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create one Product
    /// </summary>
    public async Task<Product> CreateProduct(ProductCreateInput createDto)
    {
        var product = new ProductDbModel
        {
            CreatedAt = createDto.CreatedAt,
            Name = createDto.Name,
            Price = createDto.Price,
            UpdatedAt = createDto.UpdatedAt
        };

        if (createDto.Id != null)
        {
            product.Id = createDto.Id;
        }
        if (createDto.Category != null)
        {
            product.Category = await _context
                .Categories.Where(category => createDto.Category.Id == category.Id)
                .FirstOrDefaultAsync();
        }

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        var result = await _context.FindAsync<ProductDbModel>(product.Id);

        if (result == null)
        {
            throw new NotFoundException();
        }

        return result.ToDto();
    }

    /// <summary>
    /// Delete one Product
    /// </summary>
    public async Task DeleteProduct(ProductWhereUniqueInput uniqueId)
    {
        var product = await _context.Products.FindAsync(uniqueId.Id);
        if (product == null)
        {
            throw new NotFoundException();
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Find many Products
    /// </summary>
    public async Task<List<Product>> Products(ProductFindManyArgs findManyArgs)
    {
        var products = await _context
            .Products.Include(x => x.Category)
            .ApplyWhere(findManyArgs.Where)
            .ApplySkip(findManyArgs.Skip)
            .ApplyTake(findManyArgs.Take)
            .ApplyOrderBy(findManyArgs.SortBy)
            .ToListAsync();
        return products.ConvertAll(product => product.ToDto());
    }

    /// <summary>
    /// Meta data about Product records
    /// </summary>
    public async Task<MetadataDto> ProductsMeta(ProductFindManyArgs findManyArgs)
    {
        var count = await _context.Products.ApplyWhere(findManyArgs.Where).CountAsync();

        return new MetadataDto { Count = count };
    }

    /// <summary>
    /// Get one Product
    /// </summary>
    public async Task<Product> Product(ProductWhereUniqueInput uniqueId)
    {
        var products = await this.Products(
            new ProductFindManyArgs { Where = new ProductWhereInput { Id = uniqueId.Id } }
        );
        var product = products.FirstOrDefault();
        if (product == null)
        {
            throw new NotFoundException();
        }

        return product;
    }

    /// <summary>
    /// Update one Product
    /// </summary>
    public async Task UpdateProduct(ProductWhereUniqueInput uniqueId, ProductUpdateInput updateDto)
    {
        var product = updateDto.ToModel(uniqueId);

        if (updateDto.Category != null)
        {
            product.Category = await _context
                .Categories.Where(category => updateDto.Category == category.Id)
                .FirstOrDefaultAsync();
        }

        _context.Entry(product).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Products.Any(e => e.Id == product.Id))
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
    /// Get a category record for Product
    /// </summary>
    public async Task<Category> GetCategory(ProductWhereUniqueInput uniqueId)
    {
        var product = await _context
            .Products.Where(product => product.Id == uniqueId.Id)
            .Include(product => product.Category)
            .FirstOrDefaultAsync();
        if (product == null)
        {
            throw new NotFoundException();
        }
        return product.Category.ToDto();
    }
}
