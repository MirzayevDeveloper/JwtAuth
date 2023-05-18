using Auth.Application.Abstractions;
using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Application.Interfaces.TokenServiceInterfaces;
using Auth.Domain.Entities.RolePermissions;
using Auth.Domain.Entities.Roles;
using Auth.Domain.Entities.UserRoles;
using Auth.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

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

			var roles =
				_context.GetAll<UserRole>().ToList();

			var userRoles = roles.Where(
					x => x.UserId.Equals(userId)).ToList();

			var getUserRoles = new List<Role>();

			foreach (var item in userRoles)
			{
				getUserRoles.Add(_context.GetAll<Role>()
					.FirstOrDefault(x => x.Id.Equals(item.RoleId)));
			}

			var rolePermissions = _context.GetAll<RolePermission>().Include(x => x.Role)
				.Include(a => a.Permission).ToList();

			maybeUser.Roles = new List<string>();

			if (getUserRoles != null)
			{
				foreach (var item in getUserRoles)
				{
					foreach (var item1 in rolePermissions)
					{
						if (item1.Role.Name == item.Name)
						{
							maybeUser.Roles.Add(item1.Permission.ActionName);
						}
					}
				}
			}

			return maybeUser;
		}

		public IQueryable<User> GetAllUsers()
		{
			List<User> list = _context.GetAll<User>().Include(x => x.UserRoles).
				ThenInclude(a =>a.Role).ToList();

			return list.AsQueryable();
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
