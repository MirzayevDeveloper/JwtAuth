using Auth.Domain.Entities;

namespace Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces
{
	public interface IRoleService
	{
		ValueTask<Role> AddRoleAsync(Role role);
		ValueTask<Role> GetRoleByIdAsync(Guid roleId);
		IQueryable<Role> GetAllRoles();
		ValueTask<Role> UpdateRoleAsync(Role role);
		ValueTask<Role> DeleteRoleAsync(Guid roleId);
	}
}
