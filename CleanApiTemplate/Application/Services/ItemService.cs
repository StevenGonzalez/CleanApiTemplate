using CleanApiTemplate.Application.Abstractions;
using CleanApiTemplate.Domain.Entities;
using CleanApiTemplate.Domain.Repositories;

namespace CleanApiTemplate.Application.Services;

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
}