using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Auth.Domain.Entities
{
	public class Role
	{
		[Column("RoleId")]
		public Guid Id { get; set; }

		[JsonIgnore]
		public virtual ICollection<UserRole> UserRoles { get; set; }

		[JsonIgnore]
		public virtual ICollection<RolePermission> RolePermissions { get; set; }
	}
}
