using Auth.Application.DTOs.Permissions;
using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
	[Route("api/permissions")]
	[ApiController]
	public class PermissionsController : ControllerBase
	{
		private readonly IPermissionService _permissionService;
		private readonly IMapper _mapper;

		public PermissionsController(
			IPermissionService permissionService,
			IMapper mapper)
		{
			_permissionService = permissionService;
			_mapper = mapper;
		}

		[HttpPost]
		public async ValueTask<IActionResult> PostPermissionAsync(PostPermissionDto permission)
		{
			Permission entity = _mapper.Map<Permission>(permission);

			entity = await
				_permissionService.AddPermissionAsync(entity);

			return Ok(entity);
		}

		[HttpGet("{id}")]
		public async ValueTask<IActionResult> GetPermissionAsync(Guid id)
		{
			Permission entity = await
				_permissionService.GetPermissionByIdAsync(id);

			GetPermissionDto dto = 
				_mapper.Map<GetPermissionDto>(entity);

			return Ok(dto);
		}

		[HttpGet]
		public IActionResult GetAllPermissions()
		{
			IQueryable<Permission> permissions = 
				_permissionService.GetAllPermissions();

			List<GetPermissionDto> entities =
				_mapper.Map<List<GetPermissionDto>>(permissions);

			return Ok(entities);
		}

		[HttpPut]
		public async ValueTask<IActionResult> PutPermissionAsync(UpdatePermissionDto permission)
		{ 
			Permission entity =
				_mapper.Map<Permission>(permission);

			await _permissionService.UpdatePermissionAsync(entity);

			return Ok(permission);
		}

		[HttpDelete]
		public async ValueTask<IActionResult> DeletePermissionAsync(Guid id)
		{
			Permission entity = await
				_permissionService.DeletePermissionAsync(id);

			return Ok(entity);
		}
	}
}
