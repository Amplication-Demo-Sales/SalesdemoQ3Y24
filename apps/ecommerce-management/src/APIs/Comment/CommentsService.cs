using EcommerceManagement.Infrastructure;

namespace EcommerceManagement.APIs;

public class CommentsService : CommentsServiceBase
{
    public CommentsService(EcommerceManagementDbContext context)
        : base(context) { }
}
