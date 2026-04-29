using CleanApiTemplate.Domain.Entities;
using CleanApiTemplate.Domain.Repositories;

namespace CleanApiTemplate.Infrastructure.Repositories;

public sealed class InMemoryItemRepository : IItemRepository
{
    private static readonly List<Item> Items =
    [
        new Item(Guid.Parse("e57e4bc8-8a7b-4a0e-bf8e-f56d7193b94b"), "Roadmap draft", new DateTime(2026, 01, 10, 0, 0, 0, DateTimeKind.Utc)),
        new Item(Guid.Parse("45ca059c-867b-4be9-b4e3-8209ec14f1ec"), "Architecture notes", new DateTime(2026, 01, 12, 0, 0, 0, DateTimeKind.Utc)),
        new Item(Guid.Parse("b3f93584-8602-42ba-b6d1-7a36f9b0e9e2"), "Release checklist", new DateTime(2026, 01, 14, 0, 0, 0, DateTimeKind.Utc))
    ];

    // This adapter keeps the template runnable without a database.
    public Task<IReadOnlyList<Item>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyList<Item> result = Items
            .OrderBy(x => x.CreatedUtc)
            .ToList();

        return Task.FromResult(result);
    }

    public Task<Item?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var result = Items.FirstOrDefault(x => x.Id == id);
        return Task.FromResult(result);
    }
}