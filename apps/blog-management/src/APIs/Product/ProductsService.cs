using BlogManagement.Infrastructure;

namespace BlogManagement.APIs;

public class ProductsService : ProductsServiceBase
{
    public ProductsService(BlogManagementDbContext context)
        : base(context) { }
}
