using DotnetCleanApiTemplate.Application.Services;
using DotnetCleanApiTemplate.Domain.Entities;
using DotnetCleanApiTemplate.Domain.Repositories;

namespace DotnetCleanApiTemplate.Tests.Application.Services;

public sealed class ItemServiceTests
{
    [Fact]
    public async Task GetByIdAsync_WhenItemExists_ReturnsItem()
    {
        var first = new Item { Id = Guid.NewGuid(), Name = "First", CreatedUtc = DateTime.UtcNow.AddDays(-1) };
        var second = new Item { Id = Guid.NewGuid(), Name = "Second", CreatedUtc = DateTime.UtcNow };
        var repository = new FakeItemRepository([first, second]);
        var service = new ItemService(repository);

        var result = await service.GetByIdAsync(second.Id);

        Assert.NotNull(result);
        Assert.Equal(second.Id, result.Id);
        Assert.Equal("Second", result.Name);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllItems()
    {
        var items = new List<Item>
        {
            new() { Id = Guid.NewGuid(), Name = "First", CreatedUtc = DateTime.UtcNow.AddDays(-1) },
            new() { Id = Guid.NewGuid(), Name = "Second", CreatedUtc = DateTime.UtcNow }
        };

        var repository = new FakeItemRepository(items);
        var service = new ItemService(repository);

        var result = await service.GetAllAsync();

        Assert.Equal(2, result.Count);
    }

    private sealed class FakeItemRepository : IItemRepository
    {
        private readonly List<Item> _items;

        public FakeItemRepository(List<Item> items)
        {
            _items = items;
        }

        public Task<IReadOnlyList<Item>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult((IReadOnlyList<Item>)_items);
        }

        public Task<Item?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_items.FirstOrDefault(x => x.Id == id));
        }

        public Task<Item> AddAsync(Item item, CancellationToken cancellationToken = default)
        {
            _items.Add(item);
            return Task.FromResult(item);
        }
    }
}