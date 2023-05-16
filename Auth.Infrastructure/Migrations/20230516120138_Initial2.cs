using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auth.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "UserRefreshTokens");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiredDate",
                table: "UserRefreshTokens",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpiredDate",
                table: "UserRefreshTokens");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "UserRefreshTokens",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
