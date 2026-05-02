using DotnetCleanApiTemplate.Domain.Repositories;
using DotnetCleanApiTemplate.Infrastructure.Persistence;
using DotnetCleanApiTemplate.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DotnetCleanApiTemplate.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddScoped<IItemRepository, EfItemRepository>();

        return services;
    }
}