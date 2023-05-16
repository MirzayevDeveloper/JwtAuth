using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Auth.Application.DTOs.Users
{
	public class DeleteUserDto
	{
		[RegularExpression("^(?=.*[a-z])(?=.*[A-Z]).{8,}$", ErrorMessage = "Password is invalid")]
		[Compare("ConfirmPassword", ErrorMessage = "Password do not match")]
		public string Password { get; set; }

		[JsonPropertyName("confirm_password")]
		public string ConfirmPassword { get; set; }
	}
}
