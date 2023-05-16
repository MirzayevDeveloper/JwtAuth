using Auth.Domain.Entities.Permissions;
using Auth.Domain.Entities.Roles;

namespace Auth.Domain.Entities.RolePermissions
{
	public class RolePermission
	{
		public Guid RoleId { get; set; }
		public Role Role { get; set; }

		public Guid PermissionId { get; set; }
		public Permission Permission { get; set; }
	}
}
