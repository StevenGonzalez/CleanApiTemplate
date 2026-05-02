using DotnetCleanApiTemplate.Domain.Entities;

namespace DotnetCleanApiTemplate.Application.Abstractions;

public interface IItemService
{
    Task<IReadOnlyList<Item>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Item?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Item> CreateAsync(string name, CancellationToken cancellationToken = default);
}