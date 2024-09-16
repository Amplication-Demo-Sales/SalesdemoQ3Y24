using BookingService.Infrastructure;

namespace BookingService.APIs;

public class RoomsService : RoomsServiceBase
{
    public RoomsService(BookingServiceDbContext context)
        : base(context) { }
}
