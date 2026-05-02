using DotnetCleanApiTemplate.Domain.Entities;

namespace DotnetCleanApiTemplate.Domain.Repositories;

public interface IItemRepository
{
    Task<IReadOnlyList<Item>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Item?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Item> AddAsync(Item item, CancellationToken cancellationToken = default);
}