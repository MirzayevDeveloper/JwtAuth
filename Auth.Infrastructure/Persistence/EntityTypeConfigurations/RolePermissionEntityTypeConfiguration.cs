using Auth.Domain.Entities.RolePermissions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Infrastructure.Persistence.EntityTypeConfigurations
{
	public class RolePermissionEntityTypeConfiguration : IEntityTypeConfiguration<RolePermission>
	{
		public void Configure(EntityTypeBuilder<RolePermission> builder)
		{
			builder.HasKey(rp => new
			{
				rp.RoleId,
				rp.PermissionId
			});

			builder.HasOne(rp => rp.Role)
				.WithMany(r => r.RolePermissions)
				.HasForeignKey(rp => rp.RoleId);

			builder.HasOne(rp => rp.Permission)
				.WithMany(p => p.RolePermissions)
				.HasForeignKey(rp => rp.PermissionId);
		}
	}
}
