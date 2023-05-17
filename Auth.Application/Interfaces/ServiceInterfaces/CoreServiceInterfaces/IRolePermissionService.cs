using Auth.Domain.Entities.RolePermissions;

namespace Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces
{
	public interface IRolePermissionService
	{
		ValueTask<RolePermission> AddRolePermissionAsync(RolePermission rolePermission);
		ValueTask<RolePermission> GetRolePermissionByIdAsync(Guid rolePermissionId);
		IQueryable<RolePermission> GetAllRolePermissions();
		ValueTask<RolePermission> UpdateRolePermissionAsync(RolePermission rolePermission);
		ValueTask<RolePermission> DeleteRolePermissionAsync(Guid rolePermissionId);
	}
}
