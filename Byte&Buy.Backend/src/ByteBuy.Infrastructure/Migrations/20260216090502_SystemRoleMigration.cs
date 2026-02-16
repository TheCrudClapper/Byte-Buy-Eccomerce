using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteBuy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SystemRoleMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSystemRole",
                table: "AspNetRoles",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSystemRole",
                table: "AspNetRoles");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Rentals",
                newName: "OrderStatus");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Payments",
                newName: "OrderStatus");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Orders",
                newName: "OrderStatus");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Offers",
                newName: "OrderStatus");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "AspNetUsers",
                newName: "CustomersFullName");
        }
    }
}
