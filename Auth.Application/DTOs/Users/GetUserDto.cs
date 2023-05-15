namespace Auth.Application.DTOs.Users
{
	public class GetUserDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string UserName { get; set; }
		public string[] Roles { get; set; }
	}
}
