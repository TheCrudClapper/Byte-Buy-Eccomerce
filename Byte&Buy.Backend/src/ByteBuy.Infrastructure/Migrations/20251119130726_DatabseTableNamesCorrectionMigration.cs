using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteBuy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DatabseTableNamesCorrectionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Category_CategoryId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Condition_ConditionId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferDelivery_Delivery_DeliveryId",
                table: "OfferDelivery");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferDelivery_Offers_OfferId",
                table: "OfferDelivery");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfferDelivery",
                table: "OfferDelivery");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Condition",
                table: "Condition");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.RenameTable(
                name: "OfferDelivery",
                newName: "OfferDeliveries");

            migrationBuilder.RenameTable(
                name: "Condition",
                newName: "Conditions");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "Categories");

            migrationBuilder.RenameIndex(
                name: "IX_OfferDelivery_OfferId",
                table: "OfferDeliveries",
                newName: "IX_OfferDeliveries_OfferId");

            migrationBuilder.RenameIndex(
                name: "IX_OfferDelivery_DeliveryId",
                table: "OfferDeliveries",
                newName: "IX_OfferDeliveries_DeliveryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfferDeliveries",
                table: "OfferDeliveries",
                column: "SellerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conditions",
                table: "Conditions",
                column: "SellerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Conditions_ConditionId",
                table: "Items",
                column: "ConditionId",
                principalTable: "Conditions",
                principalColumn: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferDeliveries_Delivery_DeliveryId",
                table: "OfferDeliveries",
                column: "DeliveryId",
                principalTable: "Delivery",
                principalColumn: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferDeliveries_Offers_OfferId",
                table: "OfferDeliveries",
                column: "OrderId",
                principalTable: "Offers",
                principalColumn: "SellerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Conditions_ConditionId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferDeliveries_Delivery_DeliveryId",
                table: "OfferDeliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferDeliveries_Offers_OfferId",
                table: "OfferDeliveries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OfferDeliveries",
                table: "OfferDeliveries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conditions",
                table: "Conditions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            migrationBuilder.RenameTable(
                name: "OfferDeliveries",
                newName: "OfferDelivery");

            migrationBuilder.RenameTable(
                name: "Conditions",
                newName: "Condition");

            migrationBuilder.RenameTable(
                name: "Categories",
                newName: "Category");

            migrationBuilder.RenameIndex(
                name: "IX_OfferDeliveries_OfferId",
                table: "OfferDelivery",
                newName: "IX_OfferDelivery_OfferId");

            migrationBuilder.RenameIndex(
                name: "IX_OfferDeliveries_DeliveryId",
                table: "OfferDelivery",
                newName: "IX_OfferDelivery_DeliveryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OfferDelivery",
                table: "OfferDelivery",
                column: "SellerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Condition",
                table: "Condition",
                column: "SellerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Category_CategoryId",
                table: "Items",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Condition_ConditionId",
                table: "Items",
                column: "ConditionId",
                principalTable: "Condition",
                principalColumn: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferDelivery_Delivery_DeliveryId",
                table: "OfferDelivery",
                column: "DeliveryId",
                principalTable: "Delivery",
                principalColumn: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferDelivery_Offers_OfferId",
                table: "OfferDelivery",
                column: "OrderId",
                principalTable: "Offers",
                principalColumn: "SellerId");
        }
    }
}
