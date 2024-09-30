using EcommerceManagement.APIs;

namespace EcommerceManagement;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IArticlesService, ArticlesService>();
        services.AddScoped<ICommentsService, CommentsService>();
        services.AddScoped<IProfilesService, ProfilesService>();
        services.AddScoped<IUsersService, UsersService>();
    }
}
