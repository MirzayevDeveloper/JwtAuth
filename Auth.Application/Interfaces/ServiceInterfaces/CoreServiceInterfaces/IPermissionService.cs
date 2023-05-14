using Auth.Domain.Entities;

namespace Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces
{
	public interface IPermissionService
	{
		ValueTask<Permission> AddPermissionAsync(Permission permission);
		ValueTask<Permission> GetPermissionByIdAsync(Guid permissionId);
		IQueryable<Permission> GetAllPermissions();
		ValueTask<Permission> UpdatePermissionAsync(Permission permission);
		ValueTask<Permission> DeletePermissionAsync(Guid permissionId);
	}
}
