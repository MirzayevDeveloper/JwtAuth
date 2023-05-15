using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Application.Interfaces.ServiceInterfaces.ManageServiceInterfaces;
using Auth.Application.Interfaces.ServiceInterfaces.ProcessingServices;
using Auth.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
	[Route("api/users")]
	[ApiController]
	//[Authorize]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly IUserProcessingService _userProcessingService;
		private readonly IUserManageService _userManageService;

		public UserController(
			IUserService userService,
			IUserProcessingService userProcessingService,
			IUserManageService userManageService)
		{
			_userService = userService;
			_userProcessingService = userProcessingService;
			_userManageService = userManageService;
		}

		[HttpPost]
		//[AllowAnonymous]
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

		[HttpPost("login")]
		//[AllowAnonymous]
		public IActionResult LoginAsync(UserCredentials userCredentials)
		{
			User maybeUser =
				_userProcessingService
					.GetUserByUserCredentials(userCredentials);

			if (maybeUser == null)
			{
				return NotFound(userCredentials);
			}

			UserToken userToken =
				_userManageService.CreateUserToken(maybeUser);

			return Ok(userToken);
		}
	}
}
