using Auth.Application.DTOs.RolePermissions;
using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Domain.Entities.RolePermissions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RolePermissionsController : ControllerBase
	{
		private readonly IRolePermissionService _rolePermissionService;
		private readonly IMapper _mapper;

		public RolePermissionsController(
			IRolePermissionService rolePermissionService,
			IMapper mapper)
		{
			_rolePermissionService = rolePermissionService;
			_mapper = mapper;
		}

		[HttpPost, Authorize(Roles = "PostRolePermission")]
		public async ValueTask<IActionResult> PostRolePermissionAsync(PostRolePermissionDto dto)
		{
			RolePermission RolePermission = _mapper.Map<RolePermission>(dto);

			await _rolePermissionService.AddRolePermissionAsync(RolePermission);

			return Ok(dto);
		}

		[HttpGet("{id}"), Authorize(Roles = "GetRolePermission")]
		public async ValueTask<IActionResult> GetRolePermissionAsync(Guid id)
		{
			RolePermission entity = await _rolePermissionService.GetRolePermissionByIdAsync(id);

			if (entity == null)
			{
				return BadRequest();
			}

			GetRolePermissionDto readRolePermissionDTO = _mapper.Map<GetRolePermissionDto>(entity);

			return Ok(readRolePermissionDTO);
		}


		[HttpGet, Authorize(Roles = "GetAllRolePermissions")]
		public IActionResult GetRolePermissionAsync()
		{
			IQueryable<RolePermission> entities = _rolePermissionService.GetAllRolePermissions();

			List<GetRolePermissionDto> RolePermissionDTOs = _mapper.Map<List<GetRolePermissionDto>>(entities);

			return Ok(RolePermissionDTOs);
		}

		[HttpPut, Authorize(Roles = "UpdateRolePermission")]
		public async ValueTask<IActionResult> PutRolePermissionAsync(UpdateRolePermissionDto dto)
		{
			RolePermission entity = _mapper.Map<RolePermission>(dto);

			await _rolePermissionService.UpdateRolePermissionAsync(entity);

			return Ok(dto);
		}

		[HttpDelete, Authorize(Roles = "DeleteRolePermission")]
		public async ValueTask<IActionResult> DeleteRolePermissionAsync(Guid id)
		{
			RolePermission entity = await _rolePermissionService.DeleteRolePermissionAsync(id);

			DeleteRolePermissionDto deleteRolePermissionDTO = _mapper.Map<DeleteRolePermissionDto>(entity);

			return Ok(deleteRolePermissionDTO);
		}
	}
}
