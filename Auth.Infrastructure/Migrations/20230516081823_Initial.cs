using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auth.Infrastructure.Migrations
{
	/// <inheritdoc />
	public partial class Initial : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Permissions",
				columns: table => new
				{
					PermissionId = table.Column<Guid>(type: "uuid", nullable: false),
					ActionName = table.Column<string>(type: "text", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Permissions", x => x.PermissionId);
				});

			migrationBuilder.CreateTable(
				name: "Products",
				columns: table => new
				{
					ProductId = table.Column<Guid>(type: "uuid", nullable: false),
					Name = table.Column<string>(type: "text", nullable: true),
					Description = table.Column<string>(type: "text", nullable: true),
					Price = table.Column<decimal>(type: "numeric", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Products", x => x.ProductId);
				});

			migrationBuilder.CreateTable(
				name: "Roles",
				columns: table => new
				{
					RoleId = table.Column<Guid>(type: "uuid", nullable: false),
					Name = table.Column<string>(type: "text", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Roles", x => x.RoleId);
				});

			migrationBuilder.CreateTable(
				name: "UserRefreshTokens",
				columns: table => new
				{
					UserRefreshTokenId = table.Column<Guid>(type: "uuid", nullable: false),
					UserName = table.Column<string>(type: "text", nullable: false),
					RefreshToken = table.Column<string>(type: "text", nullable: false),
					IsActive = table.Column<bool>(type: "boolean", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_UserRefreshTokens", x => x.UserRefreshTokenId);
				});

			migrationBuilder.CreateTable(
				name: "Users",
				columns: table => new
				{
					UserId = table.Column<Guid>(type: "uuid", nullable: false),
					Name = table.Column<string>(type: "text", nullable: true),
					Email = table.Column<string>(type: "text", nullable: true),
					UserName = table.Column<string>(type: "text", nullable: true),
					Password = table.Column<string>(type: "text", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Users", x => x.UserId);
				});

			migrationBuilder.CreateTable(
				name: "RolePermissions",
				columns: table => new
				{
					RoleId = table.Column<Guid>(type: "uuid", nullable: false),
					PermissionId = table.Column<Guid>(type: "uuid", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_RolePermissions", x => new { x.RoleId, x.PermissionId });
					table.ForeignKey(
						name: "FK_RolePermissions_Permissions_PermissionId",
						column: x => x.PermissionId,
						principalTable: "Permissions",
						principalColumn: "PermissionId",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_RolePermissions_Roles_RoleId",
						column: x => x.RoleId,
						principalTable: "Roles",
						principalColumn: "RoleId",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "UserRoles",
				columns: table => new
				{
					UserId = table.Column<Guid>(type: "uuid", nullable: false),
					RoleId = table.Column<Guid>(type: "uuid", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
					table.ForeignKey(
						name: "FK_UserRoles_Roles_RoleId",
						column: x => x.RoleId,
						principalTable: "Roles",
						principalColumn: "RoleId",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_UserRoles_Users_UserId",
						column: x => x.UserId,
						principalTable: "Users",
						principalColumn: "UserId",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_RolePermissions_PermissionId",
				table: "RolePermissions",
				column: "PermissionId");

			migrationBuilder.CreateIndex(
				name: "IX_UserRefreshTokens_UserName",
				table: "UserRefreshTokens",
				column: "UserName",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_UserRoles_RoleId",
				table: "UserRoles",
				column: "RoleId");

			migrationBuilder.CreateIndex(
				name: "IX_Users_UserName",
				table: "Users",
				column: "UserName",
				unique: true);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Products");

			migrationBuilder.DropTable(
				name: "RolePermissions");

			migrationBuilder.DropTable(
				name: "UserRefreshTokens");

			migrationBuilder.DropTable(
				name: "UserRoles");

			migrationBuilder.DropTable(
				name: "Permissions");

			migrationBuilder.DropTable(
				name: "Roles");

			migrationBuilder.DropTable(
				name: "Users");
		}
	}
}
