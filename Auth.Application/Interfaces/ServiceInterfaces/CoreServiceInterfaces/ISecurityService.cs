using System.Security.Claims;
using Auth.Domain.Entities.Tokens;
using Auth.Domain.Entities.Users;

namespace Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces
{
	public interface ISecurityService
	{
		string CreateToken(User user);
		string CreateRefreshToken();
		ValueTask<ClaimsPrincipal> GetPrincipalToken(UserToken userToken);
	}
}
