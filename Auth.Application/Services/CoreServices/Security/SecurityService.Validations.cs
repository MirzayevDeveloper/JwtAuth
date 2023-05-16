using Auth.Application.Services.CoreServices.Security.Exceptions;
using Auth.Domain.Entities.Users;

namespace Auth.Application.Services.CoreServices.Security
{
	public partial class SecurityService
	{
		private static void ValidateUser(User user)
		{
			ValidateUserIsNotNull(user);
			IsInvalid(user.Id);
			IsInvalid(user.Email);
			IsInvalid(user.UserName);
			IsInvalid(user.Password);

		}

		private static void IsInvalid(Guid userId)
		{
			if (userId == default)
			{
				throw new InvalidUserException();
			}
		}

		private static void IsInvalid(string text)
		{
			if (string.IsNullOrWhiteSpace(text))
			{
				throw new InvalidUserException();
			}
		}

		private static void ValidateUserIsNotNull(User user)
		{
			if (user == null)
			{
				throw new ArgumentNullException("Invalid User, please try later");
			}
		}
	}
}
