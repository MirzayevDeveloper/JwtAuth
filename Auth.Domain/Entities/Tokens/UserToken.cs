namespace Auth.Domain.Entities.Tokens
{
	public class UserToken
	{
		public Guid Id { get; set; }
		public string AccessToken { get; set; }
		public string RefreshToken { get; set; }
	}
}
