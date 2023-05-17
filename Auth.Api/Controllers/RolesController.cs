using Auth.Application.DTOs.Roles;
using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Domain.Entities.Permissions;
using Auth.Domain.Entities.RolePermissions;
using Auth.Domain.Entities.Roles;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
	[Route("api/roles")]
	[ApiController]
	[Authorize]
	public class RolesController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IRoleService _roleService;
		private readonly IPermissionService _permissionService;
		private readonly IRolePermissionService _rolePermissionService;

		public RolesController(
			IMapper mapper,
			IRoleService roleService,
			IPermissionService permissionService,
			IRolePermissionService rolePermissionService)
		{
			_mapper = mapper;
			_roleService = roleService;
			_permissionService = permissionService;
			_rolePermissionService = rolePermissionService;
		}

		[HttpPost, Authorize(Roles = "PostRole"), AllowAnonymous]
		public async ValueTask<IActionResult> PostRoleAsync(PostRoleDto dto)
		{
			Role entity = _mapper.Map<Role>(dto);

			entity.Id = Guid.NewGuid();

			IQueryable<Permission> permissions =
				_permissionService.GetAllPermissions();

			var listOfPermission = new List<Permission>();

			foreach (var item in dto.Permissions)
			{
				Permission per =
					permissions.FirstOrDefault(
						x => x.ActionName.Equals(item));

				if (per == null)
				{
					return NotFound($"Not found {item} permission!");
				}
				else
				{
					listOfPermission.Add(per);
				}
			}

			entity = await _roleService.AddRoleAsync(entity);

			foreach (var item in listOfPermission)
			{
				await _rolePermissionService.AddRolePermissionAsync(new RolePermission
				{
					Permission = item,
					Role = entity,
				});
			}

			return Ok(dto);
		}

		[HttpGet("{id}"), Authorize(Roles = "GetRole")]
		public async ValueTask<IActionResult> GetRoleAsync(Guid id)
		{
			Role entity = await _roleService.GetRoleByIdAsync(id);

			GetRoleDto dto = _mapper.Map<GetRoleDto>(entity);

			return Ok(dto);
		}

		[HttpGet, Authorize(Roles = "GetAllRoles"), AllowAnonymous]
		public IActionResult GetAllRoles()
		{
			IQueryable<Role> entities = _roleService.GetAllRoles();

			List<GetRoleDto> roles =
				_mapper.Map<List<GetRoleDto>>(entities);

			return Ok(roles);
		}


		[HttpPut, Authorize(Roles = "UpdateRole")]
		public async ValueTask<IActionResult> PutRoleAsync(UpdateRoleDto dto)
		{
			Role entity = _mapper.Map<Role>(dto);

			entity = await _roleService.UpdateRoleAsync(entity);

			return Ok(entity);
		}

		[HttpDelete, Authorize(Roles = "DeleteRole")]
		public async ValueTask<IActionResult> DeleteRoleAsync(Guid id)
		{
			Role entity = await _roleService.DeleteRoleAsync(id);

			DeleteRoleDto dto = _mapper.Map<DeleteRoleDto>(entity);

			return Ok(dto);
		}
	}
}
