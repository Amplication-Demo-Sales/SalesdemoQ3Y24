using CrmService.Infrastructure;

namespace CrmService.APIs;

public class CustomersService : CustomersServiceBase
{
    public CustomersService(CrmServiceDbContext context)
        : base(context) { }
}
