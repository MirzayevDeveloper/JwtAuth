using Auth.Application.Abstractions;
using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Domain.Entities.Tokens;

namespace Auth.Application.Services.CoreServices.UserRefreshTokens
{
	public partial class UserRefreshTokenService : IUserRefreshTokenService
	{
		private readonly IApplicationDbContext _context;

		public UserRefreshTokenService(
			IApplicationDbContext context)
		{
			_context = context;
		}

		public async ValueTask<UserRefreshToken> AddUserRefreshTokensAsync(UserRefreshToken userRefresh)
		{
			ValidateRefreshTokenIsNotNull(userRefresh);

			userRefresh.Id = Guid.NewGuid();

			UserRefreshToken maybeUserRefreshToken =
				await _context.AddAsync<UserRefreshToken>(userRefresh);

			return maybeUserRefreshToken;
		}

		public async ValueTask<UserRefreshToken> DeleteUserRefreshTokens(Guid refreshTokenId)
		{
			UserRefreshToken maybeUserRefreshToken =
				await _context.GetAsync<UserRefreshToken>(refreshTokenId);

			ValidateRefreshTokenIsNotNull(maybeUserRefreshToken);

			maybeUserRefreshToken = await _context
				.DeleteAsync<UserRefreshToken>(maybeUserRefreshToken);

			return maybeUserRefreshToken;
		}

		public IQueryable<UserRefreshToken> GetAllUserRefreshTokens()
		{
			return _context.GetAll<UserRefreshToken>();
		}

		public async ValueTask<UserRefreshToken> GetUserRefreshTokenByIdAsync(Guid refreshTokenId)
		{
			UserRefreshToken maybeUserRefreshToken =
				await _context.GetAsync<UserRefreshToken>(refreshTokenId);

			ValidateRefreshTokenIsNotNull(maybeUserRefreshToken);

			return maybeUserRefreshToken;
		}

		public async ValueTask<UserRefreshToken> GetUserRefreshTokenByUsernameAndRefreshTokenAsync(string username, string refreshToken)
		{
			IQueryable<UserRefreshToken> entities =
				_context.GetAll<UserRefreshToken>();

			UserRefreshToken maybeUserRefreshToken =
				entities.SingleOrDefault(refresh =>
					refresh.UserName.Equals(username) &&
					refresh.RefreshToken.Equals(refresh));

			ValidateRefreshTokenIsNotNull(maybeUserRefreshToken);

			return maybeUserRefreshToken;
		}

		public ValueTask<UserRefreshToken> UpdateUserRefreshTokenAsync(UserRefreshToken userRefresh)
		{
			throw new NotImplementedException();
		}
	}
}
