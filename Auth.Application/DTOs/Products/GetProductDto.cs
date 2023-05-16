namespace Auth.Application.DTOs.Products
{
	public class GetProductDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
	}
}
