using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteBuy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeliveryCarrierCorrectionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_DeliveryCarrier_DeliveryCarrierId",
                table: "Deliveries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryCarrier",
                table: "DeliveryCarrier");

            migrationBuilder.RenameTable(
                name: "DeliveryCarrier",
                newName: "DeliveryCarriers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryCarriers",
                table: "DeliveryCarriers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_DeliveryCarriers_DeliveryCarrierId",
                table: "Deliveries",
                column: "DeliveryCarrierId",
                principalTable: "DeliveryCarriers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_DeliveryCarriers_DeliveryCarrierId",
                table: "Deliveries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeliveryCarriers",
                table: "DeliveryCarriers");

            migrationBuilder.RenameTable(
                name: "DeliveryCarriers",
                newName: "DeliveryCarrier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeliveryCarrier",
                table: "DeliveryCarrier",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_DeliveryCarrier_DeliveryCarrierId",
                table: "Deliveries",
                column: "DeliveryCarrierId",
                principalTable: "DeliveryCarrier",
                principalColumn: "Id");
        }
    }
}
