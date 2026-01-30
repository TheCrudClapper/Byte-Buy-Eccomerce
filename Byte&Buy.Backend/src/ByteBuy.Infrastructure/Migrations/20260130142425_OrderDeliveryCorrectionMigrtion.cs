using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteBuy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrderDeliveryCorrectionMigrtion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyerFullName",
                table: "OrderDeliveries");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "OrderDeliveries");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BuyerFullName",
                table: "OrderDeliveries",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "OrderDeliveries",
                type: "text",
                nullable: true);
        }
    }
}
