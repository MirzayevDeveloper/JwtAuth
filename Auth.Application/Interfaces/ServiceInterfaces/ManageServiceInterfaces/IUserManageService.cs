using System.Security.Claims;
using Auth.Domain.Entities;

namespace Auth.Application.Interfaces.ServiceInterfaces.ManageServiceInterfaces
{
	public interface IUserManageService
	{
		UserToken CreateUserToken(User user);
	}
}
