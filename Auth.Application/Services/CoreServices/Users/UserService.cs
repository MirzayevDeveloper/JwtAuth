using Auth.Application.Abstractions;
using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Application.Interfaces.TokenServiceInterfaces;
using Auth.Domain.Entities;

namespace Auth.Application.Services.CoreServices.Users
{
	public class UserService : IUserService
	{
		private readonly IApplicationDbContext _context;
		private readonly IToken _token;

		public UserService(
			IApplicationDbContext context,
			IToken token)
		{
			_context = context;
			_token = token;
		}

		public async ValueTask<User> AddUserAsync(User user)
		{
			if (user == null) throw new ArgumentNullException(nameof(user));

			user.Password = _token.HashToken(user.Password);
			user.Id = Guid.NewGuid();

			User maybeUser =
				await _context.AddAsync(user);

			return user;
		}

		public async ValueTask<User> GetUserByIdAsync(Guid userId)
		{
			User maybeUser =
				await _context.GetAsync<User>(userId);

			if (maybeUser == null) return null;

			return maybeUser;
		}

		public IQueryable<User> GetAllUsers()
		{
			return _context.GetAll<User>();
		}

		public async ValueTask<User> UpdateUserAsync(User user)
		{
			User maybeUser =
				await _context.GetAsync<User>(user.Id);

			if (maybeUser == null) return null;

			maybeUser = await _context.UpdateAsync(maybeUser);

			return maybeUser;
		}

		public async ValueTask<User> DeleteUserAsync(Guid userId)
		{
			User maybeUser =
				await _context.GetAsync<User>(userId);

			if (maybeUser == null) return null;

			maybeUser = await _context.DeleteAsync(maybeUser);

			return maybeUser;
		}
	}
}
