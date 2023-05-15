using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Auth.Domain.Entities
{
	public class User
	{
		[Column("UserId")]
		public Guid Id { get; set; }
		public string Name { get; set; }

		[EmailAddress(ErrorMessage = "Invalid email address")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Username is required")]
		public string UserName { get; set; }

		[RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[^\\da-zA-Z]).{8,15}$", ErrorMessage = "Password is invalid")]
		[Compare("Password", ErrorMessage = "Password do not match")]
		public string Password { get; set; }

		[JsonIgnore]
		public virtual ICollection<UserRole> UserRoles { get; set; }
	}
}
