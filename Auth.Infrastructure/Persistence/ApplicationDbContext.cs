using Auth.Application.Abstractions;
using Auth.Domain.Entities;
using Auth.Infrastructure.Persistence.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Persistence
{
	public class ApplicationDbContext : DbContext, IApplicationDbContext
	{
		private readonly DbContextOptions<ApplicationDbContext> options;

		public ApplicationDbContext(
			DbContextOptions<ApplicationDbContext> options)
			: base(options) => this.options = options;

		public DbSet<User> Users { get; set; }
		public DbSet<Permission> Permissions { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<RolePermission> RolePermissions { get; set; }
		public DbSet<UserRole> UserRoles { get; set; }
		public DbSet<Product> Products { get; set; }

		public async ValueTask<T> AddAsync<T>(T @object)
		{
			var context = new ApplicationDbContext(this.options);
			context.Entry(@object).State = EntityState.Added;
			await context.SaveChangesAsync();

			return @object;
		}

		public async ValueTask<T> GetAsync<T>(params object[] objectIds) where T : class
		{
			var context = new ApplicationDbContext(this.options);
			return await context.FindAsync<T>(objectIds);
		}

		public IQueryable<T> GetAll<T>() where T : class
		{
			var context = new ApplicationDbContext(this.options);
			return context.Set<T>();
		}

		public async ValueTask<T> UpdateAsync<T>(T @object)
		{
			var context = new ApplicationDbContext(this.options);
			context.Entry(@object).State = EntityState.Modified;
			await context.SaveChangesAsync();

			return @object;
		}

		public async ValueTask<T> DeleteAsync<T>(T @object)
		{
			var context = new ApplicationDbContext(this.options);
			context.Entry(@object).State = EntityState.Deleted;
			await context.SaveChangesAsync();

			return @object;
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Ignore<UserCredentials>();

			modelBuilder.ApplyConfiguration(
				new UserRoleEntityTypeConfiguration());

			modelBuilder.ApplyConfiguration(
				new RolePermissionEntityTypeConfiguration());

			modelBuilder.ApplyConfiguration(
				new RoleEntityTypeConfiguration());

			modelBuilder.ApplyConfiguration(
				new UserEntityTypeConfiguration());

			base.OnModelCreating(modelBuilder);
		}
	}
}
