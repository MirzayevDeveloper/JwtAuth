using Auth.Application.DTOs.Products;
using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Domain.Entities.Products;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
	[Route("api/products")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProductService _productService;
		private readonly IMapper _mapper;

		public ProductsController(IProductService productService, IMapper mapper)
		{
			_productService = productService;
			_mapper = mapper;
		}

		[HttpPost, Authorize(Roles = "PostProduct")]
		public async ValueTask<IActionResult> PostProductAsync(PostProductDto dto)
		{
			Product entity = _mapper.Map<Product>(dto);

			entity = await _productService.AddProductAsync(entity);

			return Ok(dto);
		}

		[HttpGet("{id}"), Authorize(Roles = "GetProduct")]
		public async ValueTask<IActionResult> GetProductAsync(Guid id)
		{
			Product product = await _productService.GetProductByIdAsync(id);

			GetProductDto dto = _mapper.Map<GetProductDto>(product);

			return Ok(dto);
		}

		[HttpGet, Authorize(Roles = "GetAllProducts")]
		public IActionResult GetProductAsync()
		{
			IQueryable<Product> products = _productService.GetAllProducts();

			List<GetProductDto> dtos = _mapper.Map<List<GetProductDto>>(products);

			return Ok(dtos);
		}

		[HttpPut, Authorize(Roles = "UpdateProduct")]
		public async ValueTask<IActionResult> PutProductAsync(UpdateProductDto dto)
		{
			Product entity = _mapper.Map<Product>(dto);

			entity = await _productService.UpdateProductAsync(entity);

			return Ok(dto);
		}

		[HttpDelete, Authorize(Roles = "DeleteProduct")]
		public async ValueTask<IActionResult> DeleteProductAsync(Guid id)
		{
			Product product = await _productService.DeleteProductAsync(id);

			GetProductDto dto = _mapper.Map<GetProductDto>(product);

			return Ok(dto);
		}
	}
}
