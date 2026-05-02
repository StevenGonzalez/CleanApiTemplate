using DotnetCleanApiTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotnetCleanApiTemplate.Infrastructure.Persistence;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Item> Items => Set<Item>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Name)
                .HasMaxLength(200)
                .IsRequired();
            entity.Property(x => x.CreatedUtc)
                .IsRequired();

            entity.HasData(
                new Item
                {
                    Id = Guid.Parse("e57e4bc8-8a7b-4a0e-bf8e-f56d7193b94b"),
                    Name = "Roadmap draft",
                    CreatedUtc = new DateTime(2026, 1, 10, 0, 0, 0, DateTimeKind.Utc)
                },
                new Item
                {
                    Id = Guid.Parse("45ca059c-867b-4be9-b4e3-8209ec14f1ec"),
                    Name = "Architecture notes",
                    CreatedUtc = new DateTime(2026, 1, 12, 0, 0, 0, DateTimeKind.Utc)
                },
                new Item
                {
                    Id = Guid.Parse("b3f93584-8602-42ba-b6d1-7a36f9b0e9e2"),
                    Name = "Release checklist",
                    CreatedUtc = new DateTime(2026, 1, 14, 0, 0, 0, DateTimeKind.Utc)
                });
        });
    }
}
