using Auth.Application.DTOs.Permissions;
using Auth.Application.DTOs.Roles;

namespace Auth.Application.DTOs.RolePermissions
{
	public class PostRolePermissionDto
	{
		public Guid RoleId { get; set; }
		public PostRoleDto Role { get; set; }

		public Guid PermissionId { get; set; }

		public PostPermissionDto Permission { get; set; }
	}
}
