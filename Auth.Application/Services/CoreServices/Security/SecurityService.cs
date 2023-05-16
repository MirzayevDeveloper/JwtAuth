using System.Security.Claims;
using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Application.Interfaces.TokenServiceInterfaces;
using Auth.Domain.Entities.Tokens;
using Auth.Domain.Entities.Users;

namespace Auth.Application.Services.CoreServices.Security
{
	public partial class SecurityService : ISecurityService
	{
		private readonly IToken _token;

		public SecurityService(IToken token) =>
			_token = token;

		public string CreateRefreshToken()
		{
			return _token.GenerateRefreshToken();
		}

		public string CreateToken(User user)
		{
			ValidateUser(user);

			return _token.GenerateJWT(user);
		}

		public async ValueTask<ClaimsPrincipal> GetPrincipalToken(UserToken userToken)
		{
			string token = userToken.AccessToken;

			return await _token.GetPrincipalFromExpiredToken(token);
		}
	}
}
