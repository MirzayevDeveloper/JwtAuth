using System.Text.Json.Serialization;

namespace Auth.Application.DTOs.Permissions
{
	public class UpdatePermissionDto
	{
		[JsonPropertyName("permission_id")]
        public Guid Id { get; set; }
        public string ActionName { get; set; }
	}
}
