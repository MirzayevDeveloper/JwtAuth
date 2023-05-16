using System.Text.Json.Serialization;

namespace Auth.Application.DTOs.Roles
{
	public class DeleteRoleDto
	{
		[JsonPropertyName("role_id")]
		public Guid Id { get; set; }
	}
}
