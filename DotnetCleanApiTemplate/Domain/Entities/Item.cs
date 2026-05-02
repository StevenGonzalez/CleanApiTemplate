namespace DotnetCleanApiTemplate.Domain.Entities;

public sealed class Item
{
	public Guid Id { get; set; }

	public string Name { get; set; } = string.Empty;

	public DateTime CreatedUtc { get; set; }
}