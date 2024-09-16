using CrmService.APIs;

namespace CrmService;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ICustomersService, CustomersService>();
        services.AddScoped<ILeadsService, LeadsService>();
        services.AddScoped<IMeetingsService, MeetingsService>();
        services.AddScoped<IOpportunitiesService, OpportunitiesService>();
    }
}
