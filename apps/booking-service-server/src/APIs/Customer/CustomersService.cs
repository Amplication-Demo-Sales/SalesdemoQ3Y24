using BookingService.Infrastructure;

namespace BookingService.APIs;

public class CustomersService : CustomersServiceBase
{
    public CustomersService(BookingServiceDbContext context)
        : base(context) { }
}
