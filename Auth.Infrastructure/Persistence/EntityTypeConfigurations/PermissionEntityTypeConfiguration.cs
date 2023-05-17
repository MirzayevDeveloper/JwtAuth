using Auth.Domain.Entities.Permissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Infrastructure.Persistence.EntityTypeConfigurations
{
	public class PermissionEntityTypeConfiguration : IEntityTypeConfiguration<Permission>
	{
		public void Configure(EntityTypeBuilder<Permission> builder)
		{
			builder.HasIndex(x => x.ActionName).IsUnique();
		}
	}
}
