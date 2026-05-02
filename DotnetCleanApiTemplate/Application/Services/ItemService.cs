using DotnetCleanApiTemplate.Application.Abstractions;
using DotnetCleanApiTemplate.Domain.Entities;
using DotnetCleanApiTemplate.Domain.Repositories;

namespace DotnetCleanApiTemplate.Application.Services;

public sealed class ItemService : IItemService
{
    private readonly IItemRepository _repository;

    public ItemService(IItemRepository repository)
    {
        _repository = repository;
    }

    public Task<IReadOnlyList<Item>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return _repository.GetAllAsync(cancellationToken);
    }

    public Task<Item?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _repository.GetByIdAsync(id, cancellationToken);
    }

    public Task<Item> CreateAsync(string name, CancellationToken cancellationToken = default)
    {
        var item = new Item
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            CreatedUtc = DateTime.UtcNow
        };

        return _repository.AddAsync(item, cancellationToken);
    }
}