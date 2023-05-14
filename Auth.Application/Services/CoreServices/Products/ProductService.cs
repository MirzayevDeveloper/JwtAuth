using Auth.Application.Abstractions;
using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Domain.Entities;

namespace Auth.Application.Services.CoreServices.Products
{
	public class ProductService : IProductService
	{
		private readonly IApplicationDbContext _context;

		public ProductService(IApplicationDbContext context) =>
			_context = context;

		public async ValueTask<Product> AddProductAsync(Product product)
		{
			Product maybeProduct =
				await _context.AddAsync<Product>(product);

			return maybeProduct;
		}

		public async ValueTask<Product> GetProductByIdAsync(Guid productId)
		{
			Product maybeProduct =
				await _context.GetAsync<Product>(productId);

			return maybeProduct;
		}

		public IQueryable<Product> GetAllProducts()
		{
			return _context.GetAll<Product>();
		}

		public async ValueTask<Product> UpdateProductAsync(Product product)
		{
			Product maybeProduct =
				await _context.GetAsync<Product>(product.Id);

			if (maybeProduct == null) return null;

			maybeProduct =
				await _context.UpdateAsync<Product>(maybeProduct);

			return maybeProduct;
		}

		public async ValueTask<Product> DeleteProductAsync(Guid productId)
		{
			Product maybeProduct =
				await _context.GetAsync<Product>(productId);

			if (maybeProduct == null) return null;

			maybeProduct =
				await _context.DeleteAsync<Product>(maybeProduct);

			return maybeProduct;
		}
	}
}
