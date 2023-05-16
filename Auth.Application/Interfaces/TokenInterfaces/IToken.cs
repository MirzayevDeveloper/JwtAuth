using System.Security.Claims;
using Auth.Domain.Entities.Users;

namespace Auth.Application.Interfaces.TokenServiceInterfaces
{
	public interface IToken
	{
		string GenerateJWT(User user);
		string GenerateRefreshToken();
		string HashToken(string password);
		ValueTask<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token);
		string GetTokenFromHeader(string token);
	}
}
