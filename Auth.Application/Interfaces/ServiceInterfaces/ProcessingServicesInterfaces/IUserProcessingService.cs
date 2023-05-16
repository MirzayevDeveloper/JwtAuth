using Auth.Application.DTOs.Users;
using Auth.Domain.Entities;

namespace Auth.Application.Interfaces.ServiceInterfaces.ProcessingServices
{
	public interface IUserProcessingService
	{
		User GetUserByUserCredentials(UserCredentials userCredentials);
		User GetUserByUserName(string userName);
	}
}
