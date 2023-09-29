
namespace WebAPI.Models
{
	public record Ingredient
	{
		public required string Name { get; init; }
		public string Amount { get; init; }
		public string Unit { get; init; }
	}
}
