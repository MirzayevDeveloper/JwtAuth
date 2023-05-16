using System.Text.Json.Serialization;

namespace Auth.Application.DTOs.Products
{
	public class UpdateProductDto
	{
		[JsonPropertyName("product_id")]
		public Guid Id { get; set; }

		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
	}
}
