using Auth.Application.DTOs.Users;
using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Application.Interfaces.ServiceInterfaces.ManageServiceInterfaces;
using Auth.Application.Interfaces.ServiceInterfaces.ProcessingServices;
using Auth.Domain.Entities.Tokens;
using Auth.Domain.Entities.Users;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
	[Route("api/users")]
	[ApiController]
	[Authorize]
	public class UsersController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly IUserProcessingService _userProcessingService;
		private readonly IUserManageService _userManageService;
		private readonly IMapper _mapper;

		public UsersController(
			IUserService userService,
			IUserProcessingService userProcessingService,
			IUserManageService userManageService,
			IMapper mapper)
		{
			_userService = userService;
			_userProcessingService = userProcessingService;
			_userManageService = userManageService;
			_mapper = mapper;
		}

		[HttpPost]
		[AllowAnonymous]
		public async ValueTask<IActionResult> PostUserAsync([FromBody] PostUserDto user)
		{
			User entity = _mapper.Map<User>(user);

			User isExistsUser =
				_userProcessingService
					.GetUserByUserName(entity.UserName);

			if (isExistsUser != null)
			{
				return BadRequest($"{entity.UserName} is already exists please, check and try again!");
			}

			entity = await _userService.AddUserAsync(entity);

			return Ok(user);
		}

		[HttpGet("{id}")]
		public async ValueTask<IActionResult> GetUserAsync([FromBody] Guid id)
		{
			User maybeUser = await _userService.GetUserByIdAsync(id);

			GetUserDto entity = _mapper.Map<GetUserDto>(maybeUser);

			return Ok(maybeUser);
		}

		[HttpGet, Authorize(Roles = "GetAll")]
		public IActionResult GetAllUsers()
		{
			IQueryable<User> users = _userService.GetAllUsers();

			List<GetUserDto> entities =
				_mapper.Map<List<GetUserDto>>(users.ToList());

			return Ok(entities);
		}

		[HttpPut]
		public async ValueTask<IActionResult> PutUserAsync([FromBody] UpdateUserDto user)
		{
			User entity = _mapper.Map<User>(user);

			entity = await _userService.UpdateUserAsync(entity);

			return Ok(user);
		}

		[HttpDelete]
		public async ValueTask<IActionResult> DeleteUserAsync([FromBody] DeleteUserDto user)
		{
			Request.Headers.TryGetValue("Authorization", out var authorization);

			User maybeUser = await _userProcessingService
				.ValidateTokenForDeleteUser(authorization, user.Password);

			if (maybeUser == null)
			{
				return Unauthorized(user.Password);
			}

			await _userService.DeleteUserAsync(maybeUser.Id);

			return Ok($"{maybeUser.UserName} user successfully deleted");
		}

		[HttpPost("login"), AllowAnonymous]
		public IActionResult LoginAsync([FromBody] UserCredentials userCredentials)
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
