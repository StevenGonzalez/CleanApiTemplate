using DotnetCleanApiTemplate.Application.Abstractions;
using DotnetCleanApiTemplate.Application.Services;
using MediatR;
using System.Reflection;

namespace DotnetCleanApiTemplate.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register application use-case services in one place to keep Program.cs clean.
        services.AddScoped<IItemService, ItemService>();
        services.AddMediatR(Assembly.GetExecutingAssembly());

        return services;
    }
}