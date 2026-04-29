namespace CleanApiTemplate.Domain.Entities;

public sealed record Item(Guid Id, string Name, DateTime CreatedUtc);