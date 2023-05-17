using Auth.Application.DTOs.Permissions;
using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Domain.Entities.Permissions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

		[HttpPost, Authorize(Roles = "PostPermission"), AllowAnonymous]
		public async ValueTask<IActionResult> PostPermissionAsync(PostPermissionDto permission)
		{
			Permission entity = _mapper.Map<Permission>(permission);

			bool isExists = _permissionService.GetAllPermissions()
				.FirstOrDefault(x => x.ActionName.Equals(permission.ActionName)) != null;

			if(isExists)
			{
				return BadRequest("Already exists!");
			}

			entity = await
				_permissionService.AddPermissionAsync(entity);

			return Ok(permission);
		}

		[HttpGet("{id}"), Authorize(Roles = "GetPermission")]
		public async ValueTask<IActionResult> GetPermissionAsync(Guid id)
		{
			Permission entity = await
				_permissionService.GetPermissionByIdAsync(id);

			GetPermissionDto dto =
				_mapper.Map<GetPermissionDto>(entity);

			return Ok(dto);
		}

		[HttpGet, Authorize(Roles = "GetAllPermissions"), AllowAnonymous]
		public IActionResult GetAllPermissions()
		{
			IQueryable<Permission> permissions =
				_permissionService.GetAllPermissions();

			List<GetPermissionDto> entities =
				_mapper.Map<List<GetPermissionDto>>(permissions);

			return Ok(entities);
		}

		[HttpPut, Authorize(Roles = "UpdatePermission")]
		public async ValueTask<IActionResult> PutPermissionAsync(UpdatePermissionDto permission)
		{
			Permission entity =
				_mapper.Map<Permission>(permission);

			await _permissionService.UpdatePermissionAsync(entity);

			return Ok(permission);
		}

		[HttpDelete, Authorize(Roles = "DeletePermission")]
		public async ValueTask<IActionResult> DeletePermissionAsync(Guid id)
		{
			Permission entity = await
				_permissionService.DeletePermissionAsync(id);

			return Ok(entity);
		}
	}
}
