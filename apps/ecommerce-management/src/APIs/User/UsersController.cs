using Microsoft.AspNetCore.Mvc;

namespace EcommerceManagement.APIs;

[ApiController()]
public class UsersController : UsersControllerBase
{
    public UsersController(IUsersService service)
        : base(service) { }
}
