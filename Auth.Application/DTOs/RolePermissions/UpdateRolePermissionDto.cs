namespace Auth.Application.DTOs.RolePermissions
{
	public class UpdateRolePermissionDto
	{
		public Guid Id { get; set; }
		public Guid RoleId { get; set; }
		public Guid PermissionId { get; set; }
	}
}
