using Auth.Application.Abstractions;
using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Domain.Entities.Tokens;
using Microsoft.EntityFrameworkCore;

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
			List<UserRefreshToken> entities = await
				_context.GetAll<UserRefreshToken>().ToListAsync();

			UserRefreshToken maybeUserRefreshToken =
				entities.Find(rf => rf.UserName.Equals(username) &&
							rf.RefreshToken.Equals(refreshToken));

			//ValidateRefreshTokenIsNotNull(maybeUserRefreshToken);

			return maybeUserRefreshToken;
		}

		public async ValueTask<UserRefreshToken> UpdateUserRefreshTokenAsync(UserRefreshToken userRefresh)
		{
			UserRefreshToken maybeRefreshToken =
				await _context.UpdateAsync<UserRefreshToken>(userRefresh);

			if (maybeRefreshToken == null)
			{
				throw new ArgumentNullException(nameof(maybeRefreshToken));
			}

			return maybeRefreshToken;
		}
	}
}
