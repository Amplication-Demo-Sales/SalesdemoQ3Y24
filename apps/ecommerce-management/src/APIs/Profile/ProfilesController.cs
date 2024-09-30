using Microsoft.AspNetCore.Mvc;

namespace EcommerceManagement.APIs;

[ApiController()]
public class ProfilesController : ProfilesControllerBase
{
    public ProfilesController(IProfilesService service)
        : base(service) { }
}
