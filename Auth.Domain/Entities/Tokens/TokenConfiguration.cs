namespace Auth.Domain.Entities.Tokens
{
	public class TokenConfiguration
	{
		public string Key { get; set; }
		public string Issuer { get; set; }
		public string Audience { get; set; }
	}
}
