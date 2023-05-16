using System.ComponentModel.DataAnnotations.Schema;
using Auth.Domain.Entities.RolePermissions;
using Auth.Domain.Entities.UserRoles;

namespace Auth.Domain.Entities.Roles
{
	public class Role
	{
		[Column("RoleId")]
		public Guid Id { get; set; }

		public string Name { get; set; }
		public virtual ICollection<UserRole> UserRoles { get; set; }
		public virtual ICollection<RolePermission> RolePermissions { get; set; }
	}
}
