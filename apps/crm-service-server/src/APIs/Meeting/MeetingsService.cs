using CrmService.Infrastructure;

namespace CrmService.APIs;

public class MeetingsService : MeetingsServiceBase
{
    public MeetingsService(CrmServiceDbContext context)
        : base(context) { }
}
