using BlogManagement.Infrastructure;

namespace BlogManagement.APIs;

public class CategoriesService : CategoriesServiceBase
{
    public CategoriesService(BlogManagementDbContext context)
        : base(context) { }
}
