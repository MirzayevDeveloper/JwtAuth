using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Domain.Entities
{
	public class Product
	{
		[Column("ProductId")]
		public Guid Id { get; set; }

		public string Name { get; set; }
		public string Description { get; set; }
		public decimal Price { get; set; }
	}
}
