using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Auth.Domain.Entities
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
