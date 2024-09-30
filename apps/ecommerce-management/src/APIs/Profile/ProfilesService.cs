using EcommerceManagement.Infrastructure;

namespace EcommerceManagement.APIs;

public class ProfilesService : ProfilesServiceBase
{
    public ProfilesService(EcommerceManagementDbContext context)
        : base(context) { }
}
