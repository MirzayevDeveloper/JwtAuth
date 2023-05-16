using Auth.Domain.Entities.Permissions;
using Auth.Domain.Entities.Products;
using Auth.Domain.Entities.RolePermissions;
using Auth.Domain.Entities.Roles;
using Auth.Domain.Entities.Tokens;
using Auth.Domain.Entities.UserRoles;
using Auth.Domain.Entities.Users;
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
		DbSet<UserRefreshToken> UserRefreshTokens { get; set; }


		public ValueTask<T> AddAsync<T>(T @object);
		public ValueTask<T> GetAsync<T>(params object[] objectIds) where T : class;
		public IQueryable<T> GetAll<T>() where T : class;
		public ValueTask<T> UpdateAsync<T>(T @object);
		public ValueTask<T> DeleteAsync<T>(T @object);
	}
}
