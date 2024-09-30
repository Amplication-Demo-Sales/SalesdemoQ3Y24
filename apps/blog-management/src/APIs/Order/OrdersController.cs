using Microsoft.AspNetCore.Mvc;

namespace BlogManagement.APIs;

[ApiController()]
public class OrdersController : OrdersControllerBase
{
    public OrdersController(IOrdersService service)
        : base(service) { }
}
