using BookingService.APIs;

namespace BookingService;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IBookingsService, BookingsService>();
        services.AddScoped<ICustomersService, CustomersService>();
        services.AddScoped<IEventsService, EventsService>();
        services.AddScoped<IPaymentsService, PaymentsService>();
        services.AddScoped<IRoomsService, RoomsService>();
    }
}
