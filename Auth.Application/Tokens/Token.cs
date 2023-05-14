﻿using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Auth.Application.Interfaces.TokenServiceInterfaces;
using Auth.Domain.Entities;
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
				new Claim("UserName", user.UserName),
				new Claim(ClaimTypes.Email, user.Email),
				new Claim("Password", user.Password)
			};

			var token = new JwtSecurityToken(
				issuer: _tokenConfiguration.Issuer,
				audience: _tokenConfiguration.Audience,
				claims: claims,
				expires: DateTime.UtcNow.AddDays(1),
				signingCredentials: credentials
				);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}

		public string HashToken(string password)
		{
			using (SHA256 sha256 = SHA256.Create())
			{
				byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
				return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
			}
		}
	}
}