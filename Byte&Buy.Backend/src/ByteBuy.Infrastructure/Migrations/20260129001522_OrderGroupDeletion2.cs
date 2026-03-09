using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteBuy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrderGroupDeletion2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderGroups_OrderGroupId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentOrders_OrderGroups_OrderGroupId",
                table: "PaymentOrders");

            migrationBuilder.DropTable(
                name: "OrderGroups");

            migrationBuilder.DropIndex(
                name: "IX_PaymentOrders_OrderGroupId",
                table: "PaymentOrders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderGroupId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderGroupId",
                table: "PaymentOrders");

            migrationBuilder.DropColumn(
                name: "OrderGroupId",
                table: "Orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrderGroupId",
                table: "PaymentOrders",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrderGroupId",
                table: "Orders",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BuyerId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateDeleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DateEdited = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    TotalPrice_Amount = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    TotalPrice_Currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderGroups_AspNetUsers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "SellerId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentOrders_OrderGroupId",
                table: "PaymentOrders",
                column: "OrderGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderGroupId",
                table: "Orders",
                column: "OrderGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderGroups_BuyerId",
                table: "OrderGroups",
                column: "BuyerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderGroups_OrderGroupId",
                table: "Orders",
                column: "OrderGroupId",
                principalTable: "OrderGroups",
                principalColumn: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentOrders_OrderGroups_OrderGroupId",
                table: "PaymentOrders",
                column: "OrderGroupId",
                principalTable: "OrderGroups",
                principalColumn: "SellerId");
        }
    }
}
