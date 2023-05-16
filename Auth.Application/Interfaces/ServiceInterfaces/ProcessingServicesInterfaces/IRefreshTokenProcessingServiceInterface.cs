using Auth.Domain.Entities.Tokens;

namespace Auth.Application.Interfaces.ServiceInterfaces.ProcessingServicesInterfaces
{
	public interface IRefreshTokenProcessingServiceInterface
	{
		ValueTask<UserRefreshToken> GetRefreshToken(UserToken userToken);
		ValueTask<UserRefreshToken> GetRefreshTokenByUsername(string username);
	}
}
