using EcommerceManagement.Infrastructure;

namespace EcommerceManagement.APIs;

public class UsersService : UsersServiceBase
{
    public UsersService(EcommerceManagementDbContext context)
        : base(context) { }
}
