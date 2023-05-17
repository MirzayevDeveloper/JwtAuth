using Auth.Domain.Entities.Roles;
using Auth.Domain.Entities.Users;

namespace Auth.Application.DTOs.UserRoles
{
	public class GetUserRoleDto
	{
		public User User { get; set; }

		public Role Role { get; set; }
	}
}
