using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Domain.Entities
{
	public class Permission
	{
		[Column("PermissionId")]
		public Guid Id { get; set; }
		public string Action { get; set; }

		public virtual ICollection<RolePermission> RolePermissions { get; set; }
	}
}
