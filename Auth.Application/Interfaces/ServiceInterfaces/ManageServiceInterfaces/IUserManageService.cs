using System.Security.Claims;
using Auth.Domain.Entities.Tokens;
using Auth.Domain.Entities.Users;

namespace Auth.Application.Interfaces.ServiceInterfaces.ManageServiceInterfaces
{
	public interface IUserManageService
	{
		UserToken CreateUserToken(User user);
		ValueTask<ClaimsPrincipal> GetPrincipalTokenAsync(UserToken token);
	}
}
