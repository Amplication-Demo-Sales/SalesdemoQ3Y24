using CrmService.Infrastructure;

namespace CrmService.APIs;

public class LeadsService : LeadsServiceBase
{
    public LeadsService(CrmServiceDbContext context)
        : base(context) { }
}
