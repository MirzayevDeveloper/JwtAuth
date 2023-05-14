using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Application.Interfaces.ServiceInterfaces.ManageServiceInterfaces;
using Auth.Domain.Entities;

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

		public UserToken CreateUserToken(string username, string password)
		{
			ValidateUsernameAndPassword(username, password);

			User maybeUser =
				RetrieveUserByEmailAndPassword(username, password);

			ValidateUserExists(maybeUser);
			string token = _securityService.CreateToken(maybeUser);

			return new UserToken
			{
				Id = maybeUser.Id,
				Token = token
			};
		}

		private User RetrieveUserByEmailAndPassword(string username, string password)
		{
			IQueryable<User> allUsers = _userService.GetAllUsers();

			return allUsers.FirstOrDefault(retrievedUser => retrievedUser.UserName.Equals(username)
					&& retrievedUser.Password.Equals(password));
		}
	}
}
