using BookingService.Infrastructure;

namespace BookingService.APIs;

public class BookingsService : BookingsServiceBase
{
    public BookingsService(BookingServiceDbContext context)
        : base(context) { }
}
