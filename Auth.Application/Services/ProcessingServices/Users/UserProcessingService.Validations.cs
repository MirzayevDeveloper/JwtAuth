using Auth.Application.Services.ProcessingServices.Users.Exceptions;

namespace Auth.Application.Services.ProcessingServices.Users
{
	public partial class UserProcessingService
	{
		private static void ValidateUsernameAndPassword(string username, string password)
		{
			if (username == null || password == null)
			{
				throw new InvalidUserProcessingException();
			}
		}
	}
}
