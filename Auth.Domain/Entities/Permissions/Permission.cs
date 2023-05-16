using System.ComponentModel.DataAnnotations.Schema;
using Auth.Domain.Entities.RolePermissions;

namespace Auth.Domain.Entities.Permissions
{
	public class Permission
	{
		[Column("PermissionId")]
		public Guid Id { get; set; }
		public string ActionName { get; set; }
		public virtual ICollection<RolePermission> RolePermissions { get; set; }
	}
}
