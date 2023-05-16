using System.Security.Claims;
using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Application.Interfaces.ServiceInterfaces.ProcessingServicesInterfaces;
using Auth.Application.Interfaces.TokenServiceInterfaces;
using Auth.Domain.Entities.Tokens;

namespace Auth.Application.Services.ProcessingServices.RefreshTokens
{
	public class RefreshTokenProcessingInterface : IRefreshTokenProcessingInterface
	{
		private readonly IUserRefreshTokenService _userRefreshTokenService;
		private readonly IToken _token;

		public RefreshTokenProcessingInterface(
			IUserRefreshTokenService service,
			IToken token)
		{
			_userRefreshTokenService = service;
			_token = token;
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
	}
}
