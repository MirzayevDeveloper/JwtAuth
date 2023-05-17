using Auth.Application.Abstractions;
using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Domain.Entities.RolePermissions;
using Microsoft.EntityFrameworkCore;

namespace Auth.Application.Services.CoreServices.RolePermissions
{
	public class RolePermissionService : IRolePermissionService
	{

		private readonly IApplicationDbContext _context;

		public RolePermissionService(IApplicationDbContext applicationDbContext)
		{
			this._context = applicationDbContext;
		}

		public async ValueTask<RolePermission> AddRolePermissionAsync(RolePermission rolePermission)
		{
			rolePermission.Id = Guid.NewGuid();

			return await _context.AddAsync(rolePermission);
		}

		public IQueryable<RolePermission> GetAllRolePermissions()
		{
			var result = _context.RolePermissions
						.Include(rp => rp.Role)
						.Include(rp => rp.Permission)
							  .AsQueryable();

			return result;
		}

		public async ValueTask<RolePermission> GetRolePermissionByIdAsync(Guid id)
		{
			RolePermission result = await _context.RolePermissions
					   .Include(rp => rp.Role)
					   .Include(rp => rp.Permission)
					   .AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

			return result;
		}

		public async ValueTask<RolePermission> UpdateRolePermissionAsync(RolePermission rolePermission)
		{
			return await _context.UpdateAsync(rolePermission);
		}

		public async ValueTask<RolePermission> DeleteRolePermissionAsync(Guid id)
		{
			RolePermission maybeRolePermission = await _context.GetAsync<RolePermission>(id);

			if (maybeRolePermission == null)
				throw new ArgumentNullException(nameof(maybeRolePermission));

			return await _context.DeleteAsync(maybeRolePermission);
		}
	}
}
