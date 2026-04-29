using CleanApiTemplate.Application.Abstractions;

namespace CleanApiTemplate.Api.Endpoints;

public static class ItemEndpoints
{
    public static IEndpointRouteBuilder MapItemEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/items");

        group.MapGet("/", async (IItemService service, CancellationToken cancellationToken) =>
        {
            var items = await service.GetAllAsync(cancellationToken);
            return Results.Ok(items);
        });

        group.MapGet("/{id:guid}", async (Guid id, IItemService service, CancellationToken cancellationToken) =>
        {
            var item = await service.GetByIdAsync(id, cancellationToken);
            return item is null ? Results.NotFound() : Results.Ok(item);
        });

        return app;
    }
}