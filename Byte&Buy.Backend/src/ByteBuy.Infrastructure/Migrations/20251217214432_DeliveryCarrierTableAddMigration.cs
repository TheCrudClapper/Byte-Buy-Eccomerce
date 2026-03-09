using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteBuy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeliveryCarrierTableAddMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DeliveryCarrierId",
                table: "Deliveries",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "DeliveryCarrier",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DateDeleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateEdited = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryCarrier", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deliveries_DeliveryCarrierId",
                table: "Deliveries",
                column: "DeliveryCarrierId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deliveries_DeliveryCarrier_DeliveryCarrierId",
                table: "Deliveries",
                column: "DeliveryCarrierId",
                principalTable: "DeliveryCarrier",
                principalColumn: "SellerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deliveries_DeliveryCarrier_DeliveryCarrierId",
                table: "Deliveries");

            migrationBuilder.DropTable(
                name: "DeliveryCarrier");

            migrationBuilder.DropIndex(
                name: "IX_Deliveries_DeliveryCarrierId",
                table: "Deliveries");

            migrationBuilder.DropColumn(
                name: "DeliveryCarrierId",
                table: "Deliveries");
        }
    }
}
