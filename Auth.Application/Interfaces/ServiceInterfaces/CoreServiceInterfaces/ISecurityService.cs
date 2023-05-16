using Auth.Domain.Entities.Users;

namespace Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces
{
	public interface ISecurityService
	{
		string CreateToken(User user);
	}
}
