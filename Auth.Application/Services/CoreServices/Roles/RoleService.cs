using Auth.Application.Abstractions;
using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Domain.Entities.Roles;

namespace Auth.Application.Services.CoreServices.Roles
{
	public class RoleService : IRoleService
	{
		private readonly IApplicationDbContext _context;

		public RoleService(IApplicationDbContext context) =>
			_context = context;

		public async ValueTask<Role> AddRoleAsync(Role role)
		{
			return await _context.AddAsync<Role>(role); ;
		}

		public async ValueTask<Role> GetRoleByIdAsync(Guid roleId)
		{
			Role maybeRole = await _context.GetAsync<Role>(roleId);

			if (maybeRole == null) return null;

			return maybeRole;
		}

		public IQueryable<Role> GetAllRoles()
		{
			return _context.GetAll<Role>();
		}

		public async ValueTask<Role> UpdateRoleAsync(Role role)
		{
			Role maybeRole = await _context.GetAsync<Role>(role.Id);

			if (maybeRole == null) return null;

			maybeRole = await _context.UpdateAsync<Role>(role);

			return maybeRole;
		}

		public async ValueTask<Role> DeleteRoleAsync(Guid roleId)
		{
			Role maybeRole = await _context.GetAsync<Role>(roleId);

			if (maybeRole == null) return null;

			maybeRole = await _context.DeleteAsync<Role>(maybeRole);

			return maybeRole;
		}
	}
}
