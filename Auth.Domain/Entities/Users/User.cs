using System.ComponentModel.DataAnnotations.Schema;
using Auth.Domain.Entities.UserRoles;

namespace Auth.Domain.Entities.Users
{
	public class User
	{
		[Column("UserId")]
		public Guid Id { get; set; }

		public string Name { get; set; }
		public string Email { get; set; }
		public string UserName { get; set; }
		public string Password { get; set; }
		public virtual ICollection<UserRole> UserRoles { get; set; }
	}
}
