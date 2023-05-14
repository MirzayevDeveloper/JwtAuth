using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
	[Route("api/permissions")]
	[ApiController]
	public class PermissionsController : ControllerBase
	{
		private readonly IPermissionService _permissionService;

		public PermissionsController(
			IPermissionService permissionService)
		{
			_permissionService = permissionService;
		}

		[HttpPost]
		public async ValueTask<IActionResult> PostPermissionAsync(Permission permission)
		{
			permission.Id = Guid.NewGuid();

			Permission entity = await
				_permissionService.AddPermissionAsync(permission);

			return Ok(entity);
		}

		[HttpGet("{id}")]
		public async ValueTask<IActionResult> GetPermissionAsync(Guid id)
		{
			Permission entity = await
				_permissionService.GetPermissionByIdAsync(id);

			return Ok(entity);
		}

		[HttpGet]
		public IActionResult GetAllPermissionsAsync()
		{
			IQueryable<Permission> entities = _permissionService.GetAllPermissions();

			return Ok(entities);
		}

		[HttpPut]
		public async ValueTask<IActionResult> PutPermissiosAsync(Permission permission)
		{
			Permission entity = await
				_permissionService.UpdatePermissionAsync(permission);

			return Ok(entity);
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
