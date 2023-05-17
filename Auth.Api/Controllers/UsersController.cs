﻿using System.Security.Claims;
using Auth.Application.DTOs.Users;
using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Application.Interfaces.ServiceInterfaces.ManageServiceInterfaces;
using Auth.Application.Interfaces.ServiceInterfaces.ProcessingServices;
using Auth.Application.Interfaces.ServiceInterfaces.ProcessingServicesInterfaces;
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
		private readonly IMapper _mapper;
		private readonly IUserService _userService;
		private readonly ISecurityService _securityService;
		private readonly IUserManageService _userManageService;
		private readonly TokenConfiguration _tokenConfiguration;
		private readonly IUserProcessingService _userProcessingService;
		private readonly IUserRefreshTokenService _userRefreshTokenService;
		private readonly IRefreshTokenProcessingServiceInterface _refreshTokenProcessingService;

		public UsersController(
			IMapper mapper,
			IUserService userService,
			IConfiguration configuration,
			ISecurityService securityService,
			IUserManageService userManageService,
			IUserProcessingService userProcessingService,
			IUserRefreshTokenService userRefreshTokenService,
			IRefreshTokenProcessingServiceInterface refreshTokenProcessingInterface)
		{
			_mapper = mapper;
			_userService = userService;
			_securityService = securityService;
			_userManageService = userManageService;
			_userProcessingService = userProcessingService;
			_userRefreshTokenService = userRefreshTokenService;
			_refreshTokenProcessingService = refreshTokenProcessingInterface;

			_tokenConfiguration = new TokenConfiguration();
			configuration.Bind("Jwt", _tokenConfiguration);
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
		public async ValueTask<IActionResult> GetUserAsync(Guid id)
		{
			User maybeUser = await _userService.GetUserByIdAsync(id);

			GetUserDto entity = _mapper.Map<GetUserDto>(maybeUser);

			return Ok(maybeUser);
		}

		[HttpGet, Authorize]
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
				.ValidateTokenByUserTokenForDeleteUser(authorization, user.Password);

			if (maybeUser == null)
			{
				return Unauthorized(user.Password);
			}

			await _userService.DeleteUserAsync(maybeUser.Id);

			return Ok($"{maybeUser.UserName} user successfully deleted");
		}

		[HttpPost("login"), AllowAnonymous]
		public async ValueTask<IActionResult> LoginAsync([FromBody] UserCredentials userCredentials)
		{
			User maybeUser =
				_userProcessingService
					.GetUserByUserCredentials(userCredentials);

			if (maybeUser == null)
			{
				return NotFound(userCredentials);
			}

			UserRefreshToken userRefresh = await _refreshTokenProcessingService
							.GetRefreshTokenByUsername(maybeUser.UserName);

			UserToken userToken =
				_userManageService.CreateUserToken(maybeUser);

			if (userRefresh == null)
			{
				var refreshToken = new UserRefreshToken
				{
					UserName = userCredentials.UserName,
					RefreshToken = userToken.RefreshToken,
					ExpiredDate = DateTimeOffset.UtcNow.AddMinutes(_tokenConfiguration.AccessTokenExpires)
				};

				UserRefreshToken userRefreshToken =
					await _userRefreshTokenService
						.AddUserRefreshTokensAsync(refreshToken);

				return Ok(userToken);
			}

			userRefresh.RefreshToken = userToken.RefreshToken;

			userRefresh.ExpiredDate =
				DateTimeOffset.UtcNow.AddMinutes(_tokenConfiguration.AccessTokenExpires);

			await _userRefreshTokenService.UpdateUserRefreshTokenAsync(userRefresh);

			return Ok(userToken);
		}

		[HttpPost("refresh/token"), AllowAnonymous]
		public async ValueTask<IActionResult> RefreshAsync(UserToken token)
		{
			ClaimsPrincipal principals = await
				_securityService.GetPrincipalToken(token);

			string username = principals.Identity.Name;
			string inputRefreshToken = token.RefreshToken;

			UserRefreshToken maybeRefreshTokenModel =
				await _refreshTokenProcessingService.GetRefreshToken(token);

			if(maybeRefreshTokenModel == null)
			{
				return Unauthorized("Invalid attempt!");
			}

			if (!inputRefreshToken.Equals(maybeRefreshTokenModel.RefreshToken))
			{
				return Unauthorized("Invalid attempt!");
			}

			DateTimeOffset tokenExpired = maybeRefreshTokenModel.ExpiredDate;

			DateTimeOffset refreshTokenExpired =
				tokenExpired - TimeSpan.FromMinutes(
					_tokenConfiguration.AccessTokenExpires);

			refreshTokenExpired = refreshTokenExpired
				.AddMinutes(_tokenConfiguration.RefreshTokenExpires);

			DateTimeOffset currentDatetime = DateTimeOffset.UtcNow.ToLocalTime();

			if (currentDatetime >= refreshTokenExpired)
			{
				await _userRefreshTokenService
					.DeleteUserRefreshTokens(maybeRefreshTokenModel.Id);

				return StatusCode(405, "Refresh token expired!");
			}
			else if (currentDatetime >= tokenExpired)
			{
				User maybeUser =
					_userProcessingService.GetUserByUserName(username);

				UserToken newUserToken =
					_userManageService.CreateUserToken(maybeUser);

				if (newUserToken == null)
				{
					return Unauthorized("Invalid attempt!");
				}

				var userRefresh = new UserRefreshToken
				{
					Id = maybeRefreshTokenModel.Id,
					RefreshToken = newUserToken.RefreshToken,
					UserName = username,
					ExpiredDate = currentDatetime.AddMinutes(
									_tokenConfiguration.AccessTokenExpires)
				};

				await _userRefreshTokenService.UpdateUserRefreshTokenAsync(userRefresh);

				return Ok(newUserToken);
			}

			return Ok("Normal");
		}
	}
}
