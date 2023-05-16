using Auth.Application.DTOs.Permissions;

namespace Auth.Application.DTOs.Roles
{
	public class PostRoleDto
	{
		public string Name { get; set; }
		public PostPermissionDto[] Permissions { get; set; }
	}
}
