using Microsoft.AspNetCore.Mvc;

namespace BookingService.APIs;

[ApiController()]
public class EventsController : EventsControllerBase
{
    public EventsController(IEventsService service)
        : base(service) { }
}
