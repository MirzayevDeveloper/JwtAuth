using Auth.Application.Abstractions;
using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Domain.Entities;

namespace Auth.Application.Services.CoreServices.Users
{
	public class UserService : IUserService
	{
		private readonly IApplicationDbContext _context;

		public UserService(IApplicationDbContext context) =>
			_context = context;

		public async ValueTask<User> AddUserAsync(User user)
		{
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
