using Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces;
using Auth.Application.Interfaces.ServiceInterfaces.ProcessingServices;
using Auth.Domain.Entities;

namespace Auth.Application.Services.ProcessingServices.Users
{
	public partial class UserProcessingService : IUserProcessingService
	{
		private readonly IUserService _userService;

		public UserProcessingService(IUserService userService) =>
			_userService = userService;

		public User GetUserByUserCredentials(string username, string password)
		{
			ValidateUsernameAndPassword(username, password);
			IQueryable<User> allUsers = _userService.GetAllUsers();

			return allUsers.FirstOrDefault(user => user.UserName.Equals(username)
												&& user.Password.Equals(password));
		}
	}
}
