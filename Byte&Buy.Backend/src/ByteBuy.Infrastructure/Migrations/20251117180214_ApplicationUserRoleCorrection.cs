using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteBuy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationUserRoleCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "DateDeleted",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "DateEdited",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetUserRoles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "AspNetUserRoles",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDeleted",
                table: "AspNetUserRoles",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEdited",
                table: "AspNetUserRoles",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetUserRoles",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
