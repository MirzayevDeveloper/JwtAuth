using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Auth.Application.DTOs.Users
{
	public class UpdateUserDto
	{
		[JsonPropertyName("user_id")]
		public Guid Id { get; set; }

		public string Name { get; set; }

		[Required(ErrorMessage = "Username is required")]
		public string UserName { get; set; }

		[RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{8,15}$", ErrorMessage = "Password is invalid")]
		[Compare("ConfirmPassword", ErrorMessage = "Password do not match")]
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
	}
}
