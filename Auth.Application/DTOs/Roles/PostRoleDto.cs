using Auth.Application.DTOs.Permissions;

namespace Auth.Application.DTOs.Roles
{
	public class PostRoleDto
	{
		public string Name { get; set; }
		public string[] Permissions { get; set; }
	}
}
