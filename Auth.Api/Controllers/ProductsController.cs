using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Domain.Entities.Products;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
	[Route("api/products")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProductService _productService;

		public ProductsController(IProductService productService) =>
		  _productService = productService;

		[HttpPost]
		public async ValueTask<IActionResult> PostProductAsync(Product product)
		{
			product.Id = Guid.NewGuid();

			Product maybeProduct = await _productService.AddProductAsync(product);

			return Ok(maybeProduct);
		}

		[HttpGet("{id}")]
		public async ValueTask<IActionResult> GetProductAsync(Guid id)
		{
			Product product = await _productService.GetProductByIdAsync(id);

			return Ok(product);
		}

		[HttpGet]
		public IActionResult GetProductAsync()
		{
			IQueryable<Product> products = _productService.GetAllProducts();

			return Ok(products);
		}

		[HttpPut]
		public async ValueTask<IActionResult> PutProductAsync(Product product)
		{
			Product maybeProduct = await _productService.UpdateProductAsync(product);

			return Ok(maybeProduct);
		}

		[HttpDelete]
		public async ValueTask<IActionResult> DeleteProductAsync(Guid id)
		{
			Product product = await _productService.DeleteProductAsync(id);

			return Ok(product);
		}
	}
}
