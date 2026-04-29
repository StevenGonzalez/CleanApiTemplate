using CleanApiTemplate.Domain.Repositories;
using CleanApiTemplate.Infrastructure.Repositories;

namespace CleanApiTemplate.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Register infrastructure adapters that fulfill domain contracts.
        services.AddSingleton<IItemRepository, InMemoryItemRepository>();

        return services;
    }
}