using BlogManagement.APIs.Common;
using BlogManagement.APIs.Dtos;

namespace BlogManagement.APIs;

public interface ICategoriesService
{
    /// <summary>
    /// Create one Category
    /// </summary>
    public Task<Category> CreateCategory(CategoryCreateInput category);

    /// <summary>
    /// Delete one Category
    /// </summary>
    public Task DeleteCategory(CategoryWhereUniqueInput uniqueId);

    /// <summary>
    /// Find many Categories
    /// </summary>
    public Task<List<Category>> Categories(CategoryFindManyArgs findManyArgs);

    /// <summary>
    /// Meta data about Category records
    /// </summary>
    public Task<MetadataDto> CategoriesMeta(CategoryFindManyArgs findManyArgs);

    /// <summary>
    /// Get one Category
    /// </summary>
    public Task<Category> Category(CategoryWhereUniqueInput uniqueId);

    /// <summary>
    /// Update one Category
    /// </summary>
    public Task UpdateCategory(CategoryWhereUniqueInput uniqueId, CategoryUpdateInput updateDto);

    /// <summary>
    /// Connect multiple Products records to Category
    /// </summary>
    public Task ConnectProducts(
        CategoryWhereUniqueInput uniqueId,
        ProductWhereUniqueInput[] productsId
    );

    /// <summary>
    /// Disconnect multiple Products records from Category
    /// </summary>
    public Task DisconnectProducts(
        CategoryWhereUniqueInput uniqueId,
        ProductWhereUniqueInput[] productsId
    );

    /// <summary>
    /// Find multiple Products records for Category
    /// </summary>
    public Task<List<Product>> FindProducts(
        CategoryWhereUniqueInput uniqueId,
        ProductFindManyArgs ProductFindManyArgs
    );

    /// <summary>
    /// Update multiple Products records for Category
    /// </summary>
    public Task UpdateProducts(
        CategoryWhereUniqueInput uniqueId,
        ProductWhereUniqueInput[] productsId
    );
}
