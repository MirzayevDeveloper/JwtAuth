using Auth.Domain.Entities;

namespace Auth.Application.Interfaces.ServiceInterfaces.ProcessingServices
{
	public interface IUserProcessingService
	{
		User GetUserByUserCredentials(UserCredentials userCredentials);
	}
}
