using System.ComponentModel.DataAnnotations;

namespace Auth.Domain.Entities.Tokens
{
	public class UserRefreshToken
	{
		public Guid Id { get; set; }

		[Required]
		public string UserName { get; set; }
		[Required]
		public string RefreshToken { get; set; }
		public bool IsActive { get; set; } = true;
	}
}
