using CleanApiTemplate.Domain.Entities;

namespace CleanApiTemplate.Domain.Repositories;

public interface IItemRepository
{
    Task<IReadOnlyList<Item>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Item?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}