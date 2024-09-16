using Microsoft.AspNetCore.Mvc;

namespace CrmService.APIs;

[ApiController()]
public class OpportunitiesController : OpportunitiesControllerBase
{
    public OpportunitiesController(IOpportunitiesService service)
        : base(service) { }
}
