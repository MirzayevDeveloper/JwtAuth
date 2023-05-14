using Auth.Domain.Entities;

namespace Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces
{
	public interface IProductService
	{
		ValueTask<Product> AddProductAsync(Product product);
		ValueTask<Product> GetProductByIdAsync(Guid productId);
		IQueryable<Product> GetAllProducts();
		ValueTask<Product> UpdateProductAsync(Product product);
		ValueTask<Product> DeleteProductAsync(Guid productId);
	}
}
