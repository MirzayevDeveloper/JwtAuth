using Auth.Domain.Entities;

namespace Auth.Application.Interfaces.TokenServiceInterfaces
{
	public interface IToken
	{
		string GenerateJWT(User user);
		string HashToken(string password);
	}
}
