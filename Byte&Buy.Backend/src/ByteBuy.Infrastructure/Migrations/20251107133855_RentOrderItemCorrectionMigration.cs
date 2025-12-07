using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteBuy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RentOrderItemCorrectionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateReturned",
                table: "RentOrderItems");

            migrationBuilder.DropColumn(
                name: "IsReturned",
                table: "RentOrderItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateReturned",
                table: "RentOrderItems",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsReturned",
                table: "RentOrderItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
