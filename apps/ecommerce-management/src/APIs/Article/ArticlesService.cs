using EcommerceManagement.Infrastructure;

namespace EcommerceManagement.APIs;

public class ArticlesService : ArticlesServiceBase
{
    public ArticlesService(EcommerceManagementDbContext context)
        : base(context) { }
}
