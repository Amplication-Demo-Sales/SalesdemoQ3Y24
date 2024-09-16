using BookingService.Infrastructure;

namespace BookingService.APIs;

public class EventsService : EventsServiceBase
{
    public EventsService(BookingServiceDbContext context)
        : base(context) { }
}
