using Auth.Application.DTOs.Permissions;
using Auth.Application.DTOs.Roles;

namespace Auth.Application.DTOs.RolePermissions
{
	public class GetRolePermissionDto
	{
		public PostRoleDto Role { get; set; }
		public PostPermissionDto Permission { get; set; }
	}
}
