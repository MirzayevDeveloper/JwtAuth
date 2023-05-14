namespace Auth.Application.Services.ProcessingServices.Users.Exceptions
{
	public class InvalidUserProcessingException : Exception
	{
		public InvalidUserProcessingException()
			: base("Invalid username and password, Please correct the errors and try again.")
		{ }
	}
}
