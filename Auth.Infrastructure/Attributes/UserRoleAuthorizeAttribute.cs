using Microsoft.AspNetCore.Authorization;

namespace Auth.Infrastructure.Attributes
{
	[AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
	public class UserRoleAuthorizeAttribute : AuthorizeAttribute
	{
		public string Role { get; set; }

		public override bool Match(object obj)
		{
			return base.Match(obj);
		}

	}
}
