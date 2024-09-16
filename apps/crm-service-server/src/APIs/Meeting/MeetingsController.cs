using Microsoft.AspNetCore.Mvc;

namespace CrmService.APIs;

[ApiController()]
public class MeetingsController : MeetingsControllerBase
{
    public MeetingsController(IMeetingsService service)
        : base(service) { }
}
