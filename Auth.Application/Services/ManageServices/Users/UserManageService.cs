using System.Security.Claims;
using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Application.Interfaces.ServiceInterfaces.ManageServiceInterfaces;
using Auth.Domain.Entities.Tokens;
using Auth.Domain.Entities.Users;

namespace Auth.Application.Services.ManageServices.Users
{
	public partial class UserManageService : IUserManageService
	{
		private readonly IUserService _userService;
		private readonly ISecurityService _securityService;

		public UserManageService(
			IUserService userService,
			ISecurityService securityService)
		{
			_userService = userService;
			_securityService = securityService;
		}

		public UserToken CreateUserToken(User user)
		{
			string username = user.UserName;
			string password = user.Password;

			ValidateUsernameAndPassword(username, password);

			ValidateUserExists(user);

			string token = _securityService.CreateToken(user);
			string refreshToken = _securityService.CreateRefreshToken();

			return new UserToken
			{
				Id = user.Id,
				AccessToken = token,
				RefreshToken = refreshToken,
			};
		}

		public async ValueTask<ClaimsPrincipal> GetPrincipalTokenAsync(UserToken token)
		{
			string accessToken = token.AccessToken;

			return await _securityService.GetPrincipalToken(accessToken);
		}
	}
}
