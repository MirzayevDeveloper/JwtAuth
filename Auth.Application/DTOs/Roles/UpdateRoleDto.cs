using System.Text.Json.Serialization;
using Auth.Application.DTOs.Permissions;

namespace Auth.Application.DTOs.Roles
{
	public class UpdateRoleDto
	{
		[JsonPropertyName("role_id")]
		public Guid Id { get; set; }

		public string Name { get; set; }
		public UpdatePermissionDto[] Permissions { get; set; }
	}
}
