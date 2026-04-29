using CleanApiTemplate.Application.Abstractions;
using CleanApiTemplate.Application.Services;

namespace CleanApiTemplate.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register application use-case services in one place to keep Program.cs clean.
        services.AddScoped<IItemService, ItemService>();

        return services;
    }
}