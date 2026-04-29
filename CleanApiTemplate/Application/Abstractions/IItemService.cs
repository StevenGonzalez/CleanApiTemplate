using CleanApiTemplate.Domain.Entities;

namespace CleanApiTemplate.Application.Abstractions;

public interface IItemService
{
    Task<IReadOnlyList<Item>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Item?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}