namespace Auth.Domain.Entities.Tokens
{
	public class TokenConfiguration
	{
		public string Key { get; set; }
		public string Issuer { get; set; }
		public string Audience { get; set; }
        public int AccessTokenExpires { get; set; }
		public int RefreshTokenExpires { get; set;}
    }
}
