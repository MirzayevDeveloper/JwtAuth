using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Auth.Application.Abstractions
{
	public interface IApplicationDbContext
	{
		DbSet<User> Users { get; set; }
		DbSet<Permission> Permissions { get; set; }
		DbSet<Role> Roles { get; set; }
		DbSet<RolePermission> RolePermissions { get; set; }
		DbSet<UserRole> UserRoles { get; set; }
		DbSet<Product> Products { get; set; }


		public ValueTask<T> AddAsync<T>(T @object);
		public ValueTask<T> GetAsync<T>(params object[] objectIds) where T : class;
		public IQueryable<T> GetAll<T>() where T : class;
		public ValueTask<T> UpdateAsync<T>(T @object);
		public ValueTask<T> DeleteAsync<T>(T @object);
	}
}
