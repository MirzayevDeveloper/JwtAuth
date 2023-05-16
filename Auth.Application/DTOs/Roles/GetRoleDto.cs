using Auth.Application.DTOs.Permissions;

namespace Auth.Application.DTOs.Roles
{
	public class GetRoleDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }

		public GetPermissionDto[] Permissions { get; set; }
	}
}
