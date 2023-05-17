using Auth.Application.DTOs.UserRoles;
using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Domain.Entities.UserRoles;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
	[Route("api/UserRoles")]
	[ApiController]
	public class UserRolesController : ControllerBase
	{

		private readonly IUserRoleService _userRoleService;
		private readonly IMapper _mapper;

		public UserRolesController(
			IUserRoleService userRoleService,
			IMapper mapper)
		{
			_userRoleService = userRoleService;
			_mapper = mapper;
		}

		[HttpPost, Authorize("PostUserRole")]
		public async ValueTask<IActionResult> PostUserRoleAsync(PostUserRoleDto dto)
		{
			UserRole UserRole = _mapper.Map<UserRole>(dto);

			await _userRoleService.AddUserRoleAsync(UserRole);

			return Ok(dto);
		}

		[HttpGet("{id}"), Authorize(Roles = "GetUserRole")]
		public async ValueTask<IActionResult> GetUserRoleAsync(Guid id)
		{
			UserRole entity = await _userRoleService.GetUserRoleByIdAsync(id);

			if (entity == null)
			{
				return BadRequest();
			}

			GetUserRoleDto readUserRoleDTO = _mapper.Map<GetUserRoleDto>(entity);

			return Ok(readUserRoleDTO);
		}

		[HttpGet, Authorize(Roles = "GetAllUserRoles")]
		public IActionResult GetUserRoleAsync()
		{
			IQueryable<UserRole> entities = _userRoleService.GetAllUserRoles();

			List<GetUserRoleDto> UserRoleDTOs = _mapper.Map<List<GetUserRoleDto>>(entities);

			return Ok(UserRoleDTOs);
		}

		[HttpPut, Authorize(Roles = "UpdateUserRole")]
		public async ValueTask<IActionResult> PutUserRoleAsync(UpdateUserRoleDto dto)
		{
			UserRole entity = _mapper.Map<UserRole>(dto);

			await _userRoleService.UpdateUserRoleAsync(entity);

			return Ok(dto);
		}

		[HttpDelete, Authorize(Roles = "DeleteUser")]
		public async ValueTask<IActionResult> DeleteUserRoleAsync(Guid id)
		{
			UserRole entity = await _userRoleService.DeleteUserRoleAsync(id);

			DeleteUserRoleDto deleteUserRoleDTO = _mapper.Map<DeleteUserRoleDto>(entity);

			return Ok(deleteUserRoleDTO);
		}
	}
}
