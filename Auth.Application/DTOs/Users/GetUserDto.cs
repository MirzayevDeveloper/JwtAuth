using System.Text.Json.Serialization;

namespace Auth.Application.DTOs.Users
{
	public class GetUserDto
	{
		[JsonPropertyName("user_id")]
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string UserName { get; set; }
		public List<string> Roles { get; set; }
	}
}
