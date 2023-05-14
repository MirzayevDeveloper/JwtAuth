using Auth.Application.Abstractions;
using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Domain.Entities;

namespace Auth.Application.Services.CoreServices.Permissions
{
	public class PermissionService : IPermissionService
	{
		private readonly IApplicationDbContext _context;

		public PermissionService(IApplicationDbContext context) =>
			_context = context;

		public async ValueTask<Permission> AddPermissionAsync(Permission permission)
		{
			Permission maybePermission = await _context.AddAsync(permission);

			return maybePermission;
		}

		public async ValueTask<Permission> GetPermissionByIdAsync(Guid permissionId)
		{
			Permission maybePermission =
				await _context.GetAsync<Permission>(permissionId);

			if (maybePermission == null) return null;

			return maybePermission;
		}

		public IQueryable<Permission> GetAllPermissions()
		{
			return _context.GetAll<Permission>();
		}


		public async ValueTask<Permission> UpdatePermissionAsync(Permission permission)
		{
			Permission maybePermission =
				await _context.GetAsync<Permission>(permission.Id);

			if (maybePermission == null) return null;

			maybePermission = await _context.UpdateAsync<Permission>(permission);

			return maybePermission;
		}

		public async ValueTask<Permission> DeletePermissionAsync(Guid permissionId)
		{
			Permission maybePermission =
				await _context.GetAsync<Permission>(permissionId);

			if (maybePermission == null) return null;

			maybePermission = await _context.DeleteAsync<Permission>(maybePermission);

			return maybePermission;
		}
	}
}
