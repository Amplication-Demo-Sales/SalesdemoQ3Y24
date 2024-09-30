using BlogManagement.Infrastructure;

namespace BlogManagement.APIs;

public class OrdersService : OrdersServiceBase
{
    public OrdersService(BlogManagementDbContext context)
        : base(context) { }
}
