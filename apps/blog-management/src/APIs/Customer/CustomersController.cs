using Microsoft.AspNetCore.Mvc;

namespace BlogManagement.APIs;

[ApiController()]
public class CustomersController : CustomersControllerBase
{
    public CustomersController(ICustomersService service)
        : base(service) { }
}
