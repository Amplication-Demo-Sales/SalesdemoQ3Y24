using BookingService.Infrastructure;

namespace BookingService.APIs;

public class PaymentsService : PaymentsServiceBase
{
    public PaymentsService(BookingServiceDbContext context)
        : base(context) { }
}
