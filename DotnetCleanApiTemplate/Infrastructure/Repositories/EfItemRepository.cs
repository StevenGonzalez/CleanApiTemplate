using DotnetCleanApiTemplate.Domain.Entities;
using DotnetCleanApiTemplate.Domain.Repositories;
using DotnetCleanApiTemplate.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DotnetCleanApiTemplate.Infrastructure.Repositories;

public sealed class EfItemRepository : IItemRepository
{
    private readonly AppDbContext _dbContext;

    public EfItemRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyList<Item>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.Items
            .AsNoTracking()
            .OrderBy(x => x.CreatedUtc)
            .ToListAsync(cancellationToken);
    }

    public async Task<Item?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Items
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Item> AddAsync(Item item, CancellationToken cancellationToken = default)
    {
        _dbContext.Items.Add(item);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return item;
    }
}
