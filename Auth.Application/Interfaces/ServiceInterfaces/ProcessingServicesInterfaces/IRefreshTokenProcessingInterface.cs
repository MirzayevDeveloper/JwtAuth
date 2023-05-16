using Auth.Domain.Entities.Tokens;

namespace Auth.Application.Interfaces.ServiceInterfaces.ProcessingServicesInterfaces
{
	public interface IRefreshTokenProcessingInterface
	{
		ValueTask<UserRefreshToken> GetRefreshToken(UserToken userToken);
	}
}
