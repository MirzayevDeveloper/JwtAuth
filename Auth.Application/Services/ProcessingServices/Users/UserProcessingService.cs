using Auth.Application.DTOs.Users;
using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Application.Interfaces.ServiceInterfaces.ProcessingServices;
using Auth.Application.Interfaces.TokenServiceInterfaces;
using Auth.Domain.Entities;

namespace Auth.Application.Services.ProcessingServices.Users
{
	public partial class UserProcessingService : IUserProcessingService
	{
		private readonly IUserService _userService;
		private readonly IToken _token;

		public UserProcessingService(
			IUserService userService,
			IToken token)
		{
			_userService = userService;
			_token = token;
		}

		public User GetUserByUserCredentials(UserCredentials userCredentials)
		{
			string username = userCredentials.UserName;
			string password = userCredentials.Password;
			string hashedPassword = _token.HashToken(password);

			ValidateUsernameAndPassword(username, password);
			IQueryable<User> allUsers = _userService.GetAllUsers();

			return allUsers.FirstOrDefault(user => user.UserName.Equals(username)
												&& user.Password.Equals(hashedPassword));
		}

		public User GetUserByUserName(string userName)
		{
			User maybeUser = _userService.GetAllUsers()
				.SingleOrDefault(user => user.UserName.Equals(userName));

			return maybeUser;
		}


	}
}
