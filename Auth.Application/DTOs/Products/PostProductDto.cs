using System.ComponentModel.DataAnnotations;

namespace Auth.Application.DTOs.Products
{
	public class PostProductDto
	{
		[DataType(DataType.Text, ErrorMessage = "Product name is required")]
		public string Name { get; set; }
		public string Description { get; set; }

		[Required(ErrorMessage = "Product price is required")]
		public decimal Price { get; set; }
	}
}
