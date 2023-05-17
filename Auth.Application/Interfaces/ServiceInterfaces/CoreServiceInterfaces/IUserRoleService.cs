using Auth.Domain.Entities.UserRoles;

namespace Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces
{
	public interface IUserRoleService
	{
		ValueTask<UserRole> AddUserRoleAsync(UserRole userRole);
		ValueTask<UserRole> GetUserRoleByIdAsync(Guid userRoleId);
		IQueryable<UserRole> GetAllUserRoles();
		ValueTask<UserRole> UpdateUserRoleAsync(UserRole userRole);
		ValueTask<UserRole> DeleteUserRoleAsync(Guid userRoleId);
	}
}
