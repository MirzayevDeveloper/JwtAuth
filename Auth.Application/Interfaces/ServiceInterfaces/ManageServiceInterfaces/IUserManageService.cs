using Auth.Domain.Entities;

namespace Auth.Application.Interfaces.ServiceInterfaces.ManageServiceInterfaces
{
	public interface IUserManageService
	{
		UserToken CreateUserToken(string username, string password);
	}
}
