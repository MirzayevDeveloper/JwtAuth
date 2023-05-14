using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
	[Route("api/users")]
	[ApiController]
	[Authorize]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService) =>
			_userService = userService;

		[HttpPost]
		[AllowAnonymous]
		public async ValueTask<IActionResult> PostUserAsync(User user)
		{
			user.Id = Guid.NewGuid();

			User entity = await _userService.AddUserAsync(user);

			return Ok(entity);
		}

		[HttpGet("{id}")]
		public async ValueTask<IActionResult> GetUserAsync(Guid id)
		{
			User entity = await _userService.GetUserByIdAsync(id);

			return Ok(entity);
		}

		[HttpGet]
		[Authorize(Roles = "get_all_users")]
		public IActionResult GetUserAsync()
		{
			IQueryable<User> entities = _userService.GetAllUsers();

			return Ok(entities);
		}

		[HttpPut]
		public async ValueTask<IActionResult> PutUserAsync(User user)
		{
			User entity = await _userService.UpdateUserAsync(user);

			return Ok(entity);
		}

		[HttpDelete]
		public async ValueTask<IActionResult> DeleteUserAsync(Guid id)
		{
			User entity = await _userService.DeleteUserAsync(id);

			return Ok(entity);
		}
	}
}
