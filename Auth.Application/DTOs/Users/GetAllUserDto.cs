using System.Text.Json.Serialization;

namespace Auth.Application.DTOs.Users
{
	public class GetAllUserDto
	{
		[JsonPropertyName("user_id")]
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string UserName { get; set; }
		public string Role { get; set; }
	}
}
