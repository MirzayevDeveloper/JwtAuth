using Auth.Domain.Entities;

namespace Auth.Application.Services.ManageServices.Users
{
	public partial class UserManageService
	{
		private static void ValidateUsernameAndPassword(string username, string password)
		{
			if (username == null || password == null)
			{
				throw new ArgumentNullException();
			}
		}

		private static void ValidateUserExists(User maybeUser)
		{
			if (maybeUser == null)
			{
				throw new ArgumentNullException("User is null");
			}
		}
	}
}
