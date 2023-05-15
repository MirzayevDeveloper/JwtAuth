using System.ComponentModel.DataAnnotations;

namespace Auth.Application.DTOs.Users
{
	public class PostUserDto
	{
		public string Name { get; set; }

		[EmailAddress(ErrorMessage = "Invalid email address")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Username is required")]
		public string UserName { get; set; }

		[RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{8,15}$", ErrorMessage = "Password is invalid")]
		[Compare("ConfirmPassword", ErrorMessage = "Password do not match")]
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
	}
}
