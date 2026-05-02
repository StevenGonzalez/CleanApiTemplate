using DotnetCleanApiTemplate.Application.Abstractions;
using DotnetCleanApiTemplate.Domain.Entities;
using MediatR;

namespace DotnetCleanApiTemplate.Application.Items.Queries;

public sealed record GetItemByIdQuery(Guid Id) : IRequest<Item?>;

public sealed class GetItemByIdQueryHandler : IRequestHandler<GetItemByIdQuery, Item?>
{
    private readonly IItemService _itemService;

    public GetItemByIdQueryHandler(IItemService itemService)
    {
        _itemService = itemService;
    }

    public Task<Item?> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
    {
        return _itemService.GetByIdAsync(request.Id, cancellationToken);
    }
}
