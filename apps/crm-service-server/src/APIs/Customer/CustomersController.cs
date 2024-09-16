using Microsoft.AspNetCore.Mvc;

namespace CrmService.APIs;

[ApiController()]
public class CustomersController : CustomersControllerBase
{
    public CustomersController(ICustomersService service)
        : base(service) { }
}
