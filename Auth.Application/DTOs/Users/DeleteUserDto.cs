using System.ComponentModel.DataAnnotations;

namespace Auth.Application.DTOs.Users
{
	public class DeleteUserDto
	{
		[Required(ErrorMessage = "Username is required")]
		public string UserName { get; set; }

		[RegularExpression("^(?=.*[a-z])(?=.*[A-Z]).{8,}$", ErrorMessage = "Password is invalid")]
		[Compare("ConfirmPassword", ErrorMessage = "Password do not match")]
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
	}
}
