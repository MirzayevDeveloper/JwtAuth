using Auth.Domain.Entities.Tokens;

namespace Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces
{
	public interface IUserRefreshTokenService
	{
		ValueTask<UserRefreshToken> AddUserRefreshTokensAsync(UserRefreshToken userRefresh);
		ValueTask<UserRefreshToken> GetUserRefreshTokenByIdAsync(Guid refreshTokenId);
		ValueTask<UserRefreshToken> GetUserRefreshTokenByUsernameAndRefreshTokenAsync(string username, string refreshToken);
		IQueryable<UserRefreshToken> GetAllUserRefreshTokens();
		ValueTask<UserRefreshToken> UpdateUserRefreshTokenAsync(UserRefreshToken userRefresh);
		ValueTask<UserRefreshToken> DeleteUserRefreshTokens(Guid refreshTokenId);
	}
}
