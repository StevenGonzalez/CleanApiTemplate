using DotnetCleanApiTemplate.Application.Abstractions;
using DotnetCleanApiTemplate.Domain.Entities;
using MediatR;

namespace DotnetCleanApiTemplate.Application.Items.Queries;

public sealed record GetItemsQuery : IRequest<IReadOnlyList<Item>>;

public sealed class GetItemsQueryHandler : IRequestHandler<GetItemsQuery, IReadOnlyList<Item>>
{
    private readonly IItemService _itemService;

    public GetItemsQueryHandler(IItemService itemService)
    {
        _itemService = itemService;
    }

    public Task<IReadOnlyList<Item>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
    {
        return _itemService.GetAllAsync(cancellationToken);
    }
}
