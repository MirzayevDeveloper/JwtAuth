using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Auth.Domain.Entities
{
	public class Permission
	{
		[Column("PermissionId")]
		public Guid Id { get; set; }
		public string ActionName { get; set; }

		[JsonIgnore]
		public virtual ICollection<RolePermission> RolePermissions { get; set; }
	}
}
