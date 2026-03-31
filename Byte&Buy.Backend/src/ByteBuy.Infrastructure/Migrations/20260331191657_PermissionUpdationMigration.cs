using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteBuy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PermissionUpdationMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Permissions",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Permissions",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RentCartOffers_CartOffers_Id",
                table: "RentCartOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_RentOffers_Offers_Id",
                table: "RentOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleCartOffers_CartOffers_Id",
                table: "SaleCartOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleOffers_Offers_Id",
                table: "SaleOffers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "UserPermissions",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ShippingAddresses",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SaleOffers",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SaleCartOffers",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RolePermissions",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RentOffers",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RentCartOffers",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Rentals",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Permissions",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Payments",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PaymentOrders",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PaymentDetails",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Orders",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OrderLines",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OrderDeliveries",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Offers",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "OfferDeliveries",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Items",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Images",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DeliveryCarriers",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Deliveries",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Countries",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Conditions",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Company",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categories",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Carts",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CartOffers",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AspNetUsers",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AspNetUserClaims",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AspNetRoles",
                newName: "SellerId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AspNetRoleClaims",
                newName: "SellerId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Permissions",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Permissions",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddForeignKey(
                name: "FK_RentCartOffers_CartOffers_SellerId",
                table: "RentCartOffers",
                column: "SellerId",
                principalTable: "CartOffers",
                principalColumn: "SellerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentOffers_Offers_SellerId",
                table: "RentOffers",
                column: "SellerId",
                principalTable: "Offers",
                principalColumn: "SellerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleCartOffers_CartOffers_SellerId",
                table: "SaleCartOffers",
                column: "SellerId",
                principalTable: "CartOffers",
                principalColumn: "SellerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOffers_Offers_SellerId",
                table: "SaleOffers",
                column: "SellerId",
                principalTable: "Offers",
                principalColumn: "SellerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
