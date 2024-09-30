using BlogManagement.Infrastructure;

namespace BlogManagement.APIs;

public class CustomersService : CustomersServiceBase
{
    public CustomersService(BlogManagementDbContext context)
        : base(context) { }
}
