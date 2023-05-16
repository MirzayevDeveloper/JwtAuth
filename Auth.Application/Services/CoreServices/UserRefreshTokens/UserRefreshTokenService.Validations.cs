using Auth.Domain.Entities.Tokens;

namespace Auth.Application.Services.CoreServices.UserRefreshTokens
{
	public partial class UserRefreshTokenService
	{
		private static void ValidateRefreshTokenIsNotNull(UserRefreshToken userRefresh)
		{
			if (userRefresh == null)
			{
				throw new ArgumentNullException(nameof(userRefresh));
			}
		}
	}
}
