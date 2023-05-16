using System.Security.Claims;
using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Application.Interfaces.ServiceInterfaces.ProcessingServicesInterfaces;
using Auth.Application.Interfaces.TokenServiceInterfaces;
using Auth.Domain.Entities.Tokens;
using Microsoft.EntityFrameworkCore;

namespace Auth.Application.Services.ProcessingServices.RefreshTokens
{
	public class RefreshTokenProcessingInterface : IRefreshTokenProcessingServiceInterface
	{
		private readonly IToken _token;
		private readonly IUserService _userService;
		private readonly IUserRefreshTokenService _userRefreshTokenService;

		public RefreshTokenProcessingInterface(
			IToken token,
			IUserService userService,
			IUserRefreshTokenService service)
		{
			_token = token;
			_userService = userService;
			_userRefreshTokenService = service;
		}

		public async ValueTask<UserRefreshToken> GetRefreshToken(UserToken userToken)
		{
			string token = userToken.AccessToken;
			string refreshToken = userToken.RefreshToken;

			ClaimsPrincipal principals =
				await _token.GetPrincipalFromExpiredToken(token);

			string username = principals.Identity.Name;

			UserRefreshToken maybeRefreshToken = await _userRefreshTokenService
				.GetUserRefreshTokenByUsernameAndRefreshTokenAsync(username, refreshToken);

			return maybeRefreshToken;
		}

		public async ValueTask<UserRefreshToken> GetRefreshTokenByUsername(string username)
		{
			UserRefreshToken refreshToken = await _userRefreshTokenService.GetAllUserRefreshTokens()
				.SingleOrDefaultAsync(refresh => refresh.UserName.Equals(username));

			return refreshToken;
		}
	}
}
