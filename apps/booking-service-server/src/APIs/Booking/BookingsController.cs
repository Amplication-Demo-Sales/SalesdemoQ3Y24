using Microsoft.AspNetCore.Mvc;

namespace BookingService.APIs;

[ApiController()]
public class BookingsController : BookingsControllerBase
{
    public BookingsController(IBookingsService service)
        : base(service) { }
}
