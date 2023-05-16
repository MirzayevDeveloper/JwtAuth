using Auth.Domain.Entities.Roles;
using Auth.Domain.Entities.Users;

namespace Auth.Domain.Entities.UserRoles
{
	public class UserRole
	{
		public Guid UserId { get; set; }
		public User User { get; set; }

		public Guid RoleId { get; set; }
		public Role Role { get; set; }
	}
}
