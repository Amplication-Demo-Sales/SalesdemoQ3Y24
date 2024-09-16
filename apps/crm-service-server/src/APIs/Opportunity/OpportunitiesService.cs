using CrmService.Infrastructure;

namespace CrmService.APIs;

public class OpportunitiesService : OpportunitiesServiceBase
{
    public OpportunitiesService(CrmServiceDbContext context)
        : base(context) { }
}
