using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Application.Interfaces.TokenServiceInterfaces;
using Auth.Domain.Entities.Users;

namespace Auth.Application.Services.CoreServices.Security
{
	public partial class SecurityService : ISecurityService
	{
		private readonly IToken _token;

		public SecurityService(IToken token) =>
			_token = token;

		public string CreateToken(User user)
		{
			ValidateUser(user);

			return _token.GenerateJWT(user);
		}


	}
}
