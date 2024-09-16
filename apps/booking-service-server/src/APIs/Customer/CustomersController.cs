using Microsoft.AspNetCore.Mvc;

namespace BookingService.APIs;

[ApiController()]
public class CustomersController : CustomersControllerBase
{
    public CustomersController(ICustomersService service)
        : base(service) { }
}
