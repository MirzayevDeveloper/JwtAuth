using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Auth.Application.Interfaces.TokenServiceInterfaces;
using Auth.Domain.Entities.Tokens;
using Auth.Domain.Entities.Users;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Auth.Application.Services.Tokens
{
	public class Token : IToken
	{
		private readonly TokenConfiguration _tokenConfiguration;

		public Token(IConfiguration configuration)
		{
			_tokenConfiguration = new TokenConfiguration();
			configuration.Bind("Jwt", _tokenConfiguration);
		}

		public string GenerateJWT(User user)
		{
			byte[] convertKeyToBytes =
				Encoding.UTF8.GetBytes(_tokenConfiguration.Key);

			var securityKey =
				new SymmetricSecurityKey(convertKeyToBytes);

			var credentials =
				new SigningCredentials(
					securityKey, SecurityAlgorithms.HmacSha256);

			var claims = new Claim[]
			{
				new Claim(ClaimTypes.Name, user.UserName),
				new Claim("Password", user.Password),
				new Claim(ClaimTypes.Email, user.Email),
				new Claim(ClaimTypes.Role, "GetAll")
			};

			var token = new JwtSecurityToken(
				issuer: _tokenConfiguration.Issuer,
				audience: _tokenConfiguration.Audience,
				claims: claims,
				expires: DateTime.UtcNow.ToLocalTime().AddMinutes(_tokenConfiguration.AccessTokenExpires),
				signingCredentials: credentials
				);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		public string GenerateRefreshToken()
		{
			string key = HashToken(_tokenConfiguration.Key);
			string dateTime = HashToken(DateTime.UtcNow.ToString());
			string refreshToken = key + dateTime;

			return refreshToken;
		}

		public string HashToken(string password)
		{
			using (SHA256 sha256 = SHA256.Create())
			{
				byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
				return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
			}
		}

		public async ValueTask<ClaimsPrincipal> GetPrincipalFromExpiredToken(string token)
		{
			var Key = Encoding.UTF8.GetBytes(_tokenConfiguration.Key);

			var tokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = false,
				ValidateAudience = false,
				ValidateLifetime = false,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Key),
				ClockSkew = TimeSpan.Zero
			};

			var tokenHandler = new JwtSecurityTokenHandler();
			var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
			JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
			if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
			{
				throw new SecurityTokenException("Invalid token");
			}

			return await Task.FromResult(principal);
		}

		public string GetTokenFromHeader(string token)
		{
			int startIndex = token.IndexOf(' ') + 1;
			int subStringLength = token.Length - startIndex;

			string subString = token.Substring(startIndex, subStringLength);

			return subString;
		}
	}
}
