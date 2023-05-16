using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Application.Interfaces.ServiceInterfaces.ManageServiceInterfaces;
using Auth.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

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

			return new UserToken
			{
				Id = user.Id,
				Token = token
			};
		}

		
	}
}
