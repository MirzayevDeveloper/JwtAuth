using Auth.Domain.Entities.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Infrastructure.Persistence.EntityTypeConfigurations
{
	public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
	{
		public void Configure(EntityTypeBuilder<Role> builder)
		{
			//builder.Navigation(e => e.UserRoles).AutoInclude();

			builder.HasIndex(x => x.Name).IsUnique();
		}
	}
}
