using Auth.Domain.Entities.Users;

namespace Auth.Application.Interfaces.ServiceInterfaces.CoreServiceInterfaces
{
	public interface IUserService
	{
		ValueTask<User> AddUserAsync(User user);
		ValueTask<User> GetUserByIdAsync(Guid userId);
		IQueryable<User> GetAllUsers();
		ValueTask<User> UpdateUserAsync(User user);
		ValueTask<User> DeleteUserAsync(Guid userId);
	}
}
