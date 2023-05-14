using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Domain.Entities
{
	public class Role
	{
		[Column("RoleId")]
		public Guid Id { get; set; }
		public virtual ICollection<UserRole> UserRoles { get; set; }
		public virtual ICollection<RolePermission> RolePermissions { get; set; }
	}
}
