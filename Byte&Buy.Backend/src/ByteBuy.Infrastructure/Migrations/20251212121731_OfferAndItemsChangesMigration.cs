using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteBuy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OfferAndItemsChangesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_AspNetUsers_OwnerId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_OwnerId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Items");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserId",
                table: "Offers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "QuantityAvailable",
                table: "Offers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompanyItem",
                table: "Items",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Offers_CreatedByUserId",
                table: "Offers",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offers_AspNetUsers_CreatedByUserId",
                table: "Offers",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offers_AspNetUsers_CreatedByUserId",
                table: "Offers");

            migrationBuilder.DropIndex(
                name: "IX_Offers_CreatedByUserId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "QuantityAvailable",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "IsCompanyItem",
                table: "Items");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedByUserId",
                table: "Items",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Items_CreatedByUserId",
                table: "Items",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_AspNetUsers_CreatedByUserId",
                table: "Items",
                column: "CreatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
