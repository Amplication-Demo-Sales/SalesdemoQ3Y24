using Microsoft.AspNetCore.Mvc;

namespace CrmService.APIs;

[ApiController()]
public class LeadsController : LeadsControllerBase
{
    public LeadsController(ILeadsService service)
        : base(service) { }
}
