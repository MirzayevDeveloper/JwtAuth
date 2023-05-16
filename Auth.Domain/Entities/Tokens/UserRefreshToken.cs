using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auth.Domain.Entities.Tokens
{
	public class UserRefreshToken
	{
		[Column("UserRefreshTokenId")]
		public Guid Id { get; set; }

		[Required]
		public string UserName { get; set; }
		[Required]
		public string RefreshToken { get; set; }
		public DateTime ExpiredDate { get; set; }
	}
}
