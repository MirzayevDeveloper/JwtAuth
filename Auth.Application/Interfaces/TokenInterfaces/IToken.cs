﻿using System.Security.Claims;
using Auth.Domain.Entities;

namespace Auth.Application.Interfaces.TokenServiceInterfaces
{
	public interface IToken
	{
		string GenerateJWT(User user);
		string GenerateRefreshToken();
		string HashToken(string password);
		ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
	}
}
