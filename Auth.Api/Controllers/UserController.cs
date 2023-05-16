using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Auth.Application.DTOs.Users;
using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Application.Interfaces.ServiceInterfaces.ManageServiceInterfaces;
using Auth.Application.Interfaces.ServiceInterfaces.ProcessingServices;
using Auth.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Any;

namespace Auth.Api.Controllers
{
	[Route("api/users")]
	[ApiController]
	[Authorize]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly IUserProcessingService _userProcessingService;
		private readonly IUserManageService _userManageService;
		private readonly IMapper _mapper;

		public UserController(
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
			//User entity = await _userService.DeleteUserAsync();

			Request.Headers.TryGetValue("Authorization", out var s);

			string a = s.ToString().Substring(
					s.ToString().IndexOf(' '),
					s.ToString().Length - s.ToString()
					.IndexOf(' '));

			///var deserialize = ///DeserializeJwt(a, "dh%8sadjGfjh&657HVD%NfrUNHG5689hgfgfasdn98q273bfq987wbecaubcq043n");

			

			return Ok();
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
