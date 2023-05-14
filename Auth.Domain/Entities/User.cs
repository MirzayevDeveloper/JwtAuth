using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Domain.Entities
{
	public class User
	{
		[Column("UserId")]
		public Guid Id { get; set; }
		public string Name { get; set; }

		[EmailAddress]
		public string Email { get; set; }

		public string UserName { get; set; }
		public string Password { get; set; }

		public virtual ICollection<UserRole> UserRoles { get; set; }
	}
}
