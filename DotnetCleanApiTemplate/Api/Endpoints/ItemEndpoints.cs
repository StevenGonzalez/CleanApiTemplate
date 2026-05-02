using DotnetCleanApiTemplate.Application.Items.Commands;
using DotnetCleanApiTemplate.Application.Items.Queries;
using MediatR;

namespace DotnetCleanApiTemplate.Api.Endpoints;

public static class ItemEndpoints
{
    public static IEndpointRouteBuilder MapItemEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/items");

        group.MapGet("/", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var items = await sender.Send(new GetItemsQuery(), cancellationToken);
            return Results.Ok(items);
        });

        group.MapGet("/{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var item = await sender.Send(new GetItemByIdQuery(id), cancellationToken);
            return item is null ? Results.NotFound() : Results.Ok(item);
        });

        group.MapPost("/", async (
            CreateItemRequest request,
            ISender sender,
            CancellationToken cancellationToken) =>
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return Results.ValidationProblem(new Dictionary<string, string[]>
                {
                    [nameof(request.Name)] = ["Name is required."]
                });
            }

            var item = await sender.Send(new CreateItemCommand(request.Name), cancellationToken);
            return Results.Created($"/items/{item.Id}", item);
        })
        .RequireAuthorization();

        return app;
    }
}

public sealed record CreateItemRequest(string Name);