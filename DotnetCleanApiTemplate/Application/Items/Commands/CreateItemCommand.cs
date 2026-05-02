using DotnetCleanApiTemplate.Application.Abstractions;
using DotnetCleanApiTemplate.Domain.Entities;
using MediatR;

namespace DotnetCleanApiTemplate.Application.Items.Commands;

public sealed record CreateItemCommand(string Name) : IRequest<Item>;

public sealed class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, Item>
{
    private readonly IItemService _itemService;

    public CreateItemCommandHandler(IItemService itemService)
    {
        _itemService = itemService;
    }

    public Task<Item> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        return _itemService.CreateAsync(request.Name, cancellationToken);
    }
}
