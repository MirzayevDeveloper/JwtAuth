using Auth.Domain.Entities;

namespace Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces
{
	public interface ISecurityService
	{
		string CreateToken(User user);
	}
}
