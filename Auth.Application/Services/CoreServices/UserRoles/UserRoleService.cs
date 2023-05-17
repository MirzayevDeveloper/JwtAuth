using Auth.Application.Abstractions;
using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Domain.Entities.UserRoles;
using Microsoft.EntityFrameworkCore;

namespace Auth.Application.Services.CoreServices.UserRoles
{
	public class UserRoleService : IUserRoleService
	{

		private readonly IApplicationDbContext _context;

		public UserRoleService(IApplicationDbContext applicationDbContext)
		{
			_context = applicationDbContext;
		}

		public async ValueTask<UserRole> AddUserRoleAsync(UserRole UserRole)
		{
			UserRole.Id = Guid.NewGuid();

			return await _context.AddAsync(UserRole);
		}

		public IQueryable<UserRole> GetAllUserRoles()
		{
			var result = _context.UserRoles
							.Include(rp => rp.User)
							.Include(rp => rp.Role)
							.AsQueryable();

			return result;
		}

		public async ValueTask<UserRole> GetUserRoleByIdAsync(Guid id)
		{
			var result = await _context.UserRoles
					  .Include(rp => rp.User)
					 .Include(rp => rp.Role).AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

			return result;
		}

		public async ValueTask<UserRole> UpdateUserRoleAsync(UserRole UserRole)
		{
			return await _context.UpdateAsync(UserRole);
		}

		public async ValueTask<UserRole> DeleteUserRoleAsync(Guid id)
		{
			UserRole maybeUserRole = await _context.GetAsync<UserRole>(id);

			if (maybeUserRole == null)
				throw new ArgumentNullException(nameof(maybeUserRole));

			return await _context.DeleteAsync(maybeUserRole);
		}
	}
}
