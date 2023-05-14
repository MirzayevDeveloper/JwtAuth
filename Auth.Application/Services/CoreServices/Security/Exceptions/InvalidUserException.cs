namespace Auth.Application.Services.CoreServices.Security.Exceptions
{
	public class InvalidUserException : Exception
	{
		public InvalidUserException() : base("Invalid user, please try again later.")
		{ }
	}
}
