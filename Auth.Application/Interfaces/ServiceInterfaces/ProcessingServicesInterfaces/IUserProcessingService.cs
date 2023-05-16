using Auth.Application.DTOs.Users;
using Auth.Domain.Entities.Users;
using Microsoft.Extensions.Primitives;

namespace Auth.Application.Interfaces.ServiceInterfaces.ProcessingServices
{
	public interface IUserProcessingService
	{
		User GetUserByUserCredentials(UserCredentials userCredentials);
		User GetUserByUserName(string userName);
		ValueTask<User> ValidateTokenByUserTokenForDeleteUser(StringValues tokenValue, string password);
		ValueTask<bool> CheckPasswordAsync(User inputUser, User findUser);
	}
}
