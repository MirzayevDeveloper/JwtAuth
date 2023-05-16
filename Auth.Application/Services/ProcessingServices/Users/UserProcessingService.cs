using System.Security.Claims;
using Auth.Application.DTOs.Users;
using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Application.Interfaces.ServiceInterfaces.ProcessingServices;
using Auth.Application.Interfaces.TokenServiceInterfaces;
using Auth.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

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

		public async ValueTask<User> ValidateTokenByUserTokenForDeleteUser(StringValues tokenValue, string password)
		{
			string token = tokenValue.ToString();
			password = _token.HashToken(password);

			if (token == null || password == null)
			{
				return null;
			}

			token = _token.GetTokenFromHeader(token);

			ClaimsPrincipal principals =
				await _token.GetPrincipalFromExpiredToken(token);

			var keys = new Dictionary<string, string>();

			foreach (var item in principals.Claims)
			{
				keys.TryAdd(item.Type, item.Value);
			}

			keys.TryGetValue("UserName", out string maybeUsername);

			keys.TryGetValue("Password", out string maybePassword);


			if (maybePassword != password && maybeUsername != null)
			{
				return null;
			}

			User maybeUser = GetUserByUserName(maybeUsername);

			return maybeUser;
		}

		public async ValueTask<bool> CheckPasswordAsync(User inputUser, User findUser)
		{
			User maybeUser = await _userService.GetAllUsers()
									.SingleOrDefaultAsync(user => user.UserName
									.Equals(inputUser.UserName));

			if (maybeUser == null)
			{
				throw new ArgumentNullException(nameof(inputUser));
			}

			bool isTrue = maybeUser.Password.Equals(findUser.Password);

			return isTrue;
		}
	}
}
