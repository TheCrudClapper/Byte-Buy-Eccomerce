using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteBuy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrdersAndRentalsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentOrder_Orders_OrderId",
                table: "PaymentOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentOrder_Payment_PaymentId",
                table: "PaymentOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_Rental_RentOrderItems_RentOrderItemId",
                table: "Rental");

            migrationBuilder.DropTable(
                name: "RentOrderItems");

            migrationBuilder.DropTable(
                name: "SaleOrderItems");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_Rental_RentOrderItemId",
                table: "Rental");

            migrationBuilder.DropIndex(
                name: "IX_Orders_UserId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentOrder",
                table: "PaymentOrder");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payment",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "AmountMinor",
                table: "PaymentOrder");

            migrationBuilder.DropColumn(
                name: "AmountMinor",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "StripeCheckoutSessionId",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "StripePaymentIntentId",
                table: "Payment");

            migrationBuilder.RenameTable(
                name: "PaymentOrder",
                newName: "PaymentOrders");

            migrationBuilder.RenameTable(
                name: "Payment",
                newName: "Payments");

            migrationBuilder.RenameColumn(
                name: "RentOrderItemId",
                table: "Rental",
                newName: "RentOrderLineId");

            migrationBuilder.RenameColumn(
                name: "TotalAmount_Currency",
                table: "Orders",
                newName: "Total_Currency");

            migrationBuilder.RenameColumn(
                name: "TotalAmount_Amount",
                table: "Orders",
                newName: "Total_Amount");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress_Street",
                table: "Orders",
                newName: "SellerSnapshot_Address_Street");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress_PostalCode",
                table: "Orders",
                newName: "SellerSnapshot_Address_PostalCode");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress_PostalCity",
                table: "Orders",
                newName: "SellerSnapshot_Address_PostalCity");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress_HouseNumber",
                table: "Orders",
                newName: "SellerSnapshot_Address_HouseNumber");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress_FlatNumber",
                table: "Orders",
                newName: "SellerSnapshot_Address_FlatNumber");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress_Country",
                table: "Orders",
                newName: "SellerSnapshot_Address_Country");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress_City",
                table: "Orders",
                newName: "SellerSnapshot_Address_City");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Orders",
                newName: "Seller_Id");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "PaymentOrders",
                newName: "OrderGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentOrder_PaymentId",
                table: "PaymentOrders",
                newName: "IX_PaymentOrders_PaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentOrder_OrderId",
                table: "PaymentOrders",
                newName: "IX_PaymentOrders_OrderGroupId");

            migrationBuilder.RenameColumn(
                name: "Currency",
                table: "Payments",
                newName: "Amount_Currency");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RentalEndDate",
                table: "Rental",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<Guid>(
                name: "BorrowerId",
                table: "Rental",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "Lender_Id",
                table: "Rental",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Lender_Type",
                table: "Rental",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "PricePerDay_Amount",
                table: "Rental",
                type: "numeric(18,3)",
                precision: 18,
                scale: 3,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PricePerDay_Currency",
                table: "Rental",
                type: "character varying(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RentalDays",
                table: "Rental",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Rental",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "BuyerId",
                table: "Orders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDelivered",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeliveryId",
                table: "Orders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "DeliveryPrice_Amount",
                table: "Orders",
                type: "numeric(18,3)",
                precision: 18,
                scale: 3,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryPrice_Currency",
                table: "Orders",
                type: "character varying(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "LinesTotal_Amount",
                table: "Orders",
                type: "numeric(18,3)",
                precision: 18,
                scale: 3,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "LinesTotal_Currency",
                table: "Orders",
                type: "character varying(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderGroupId",
                table: "Orders",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerSnapshot_DisplayName",
                table: "Orders",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "SellerSnapshot_SellerId",
                table: "Orders",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "SellerSnapshot_TIN",
                table: "Orders",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SellerSnapshot_Type",
                table: "Orders",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Seller_Type",
                table: "Orders",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "Seller_Id",
                table: "Offers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Seller_Type",
                table: "Offers",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Payments",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "Amount_Currency",
                table: "Payments",
                type: "character varying(3)",
                maxLength: 3,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount_Amount",
                table: "Payments",
                type: "numeric(18,3)",
                precision: 18,
                scale: 3,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ExternalTransactionId",
                table: "Payments",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Method",
                table: "Payments",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentOrders",
                table: "PaymentOrders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "OrderDeliveries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeliveryName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CarrierCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Channel = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Price_Amount = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    Price_Currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    BuyerFullName = table.Column<string>(type: "text", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: true),
                    HouseNumber = table.Column<string>(type: "text", nullable: true),
                    FlatNumber = table.Column<string>(type: "text", nullable: true),
                    City = table.Column<string>(type: "text", nullable: true),
                    PostalCity = table.Column<string>(type: "text", nullable: true),
                    PostalCode = table.Column<string>(type: "text", nullable: true),
                    Phone = table.Column<string>(type: "text", nullable: true),
                    PickupPointId = table.Column<string>(type: "text", nullable: true),
                    PickupPointName = table.Column<string>(type: "text", nullable: true),
                    PickupStreet = table.Column<string>(type: "text", nullable: true),
                    PickupCity = table.Column<string>(type: "text", nullable: true),
                    PickupLocalNumber = table.Column<string>(type: "text", nullable: true),
                    ParcelLockerId = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DateDeleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateEdited = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDeliveries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDeliveries_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderGroups",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BuyerId = table.Column<Guid>(type: "uuid", nullable: false),
                    TotalPrice_Amount = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    TotalPrice_Currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DateDeleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateEdited = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderGroups_AspNetUsers_BuyerId",
                        column: x => x.BuyerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderLines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemName = table.Column<string>(type: "text", nullable: false),
                    Thumbnail_ImagePath = table.Column<string>(type: "text", nullable: false),
                    Thumbnail_AltText = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DateDeleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    OrderLineType = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    PricePerDay_Amount = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: true),
                    PricePerDay_Currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    RentalDays = table.Column<int>(type: "integer", nullable: true),
                    PricePerItem_Amount = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: true),
                    PricePerItem_Currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateEdited = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderLines_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rental_BorrowerId",
                table: "Rental",
                column: "BorrowerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_BuyerId",
                table: "Orders",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryId",
                table: "Orders",
                column: "DeliveryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderGroupId",
                table: "Orders",
                column: "OrderGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDeliveries_Channel",
                table: "OrderDeliveries",
                column: "Channel");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDeliveries_OrderId",
                table: "OrderDeliveries",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderGroups_BuyerId",
                table: "OrderGroups",
                column: "BuyerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLines_OrderId",
                table: "OrderLines",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_BuyerId",
                table: "Orders",
                column: "BuyerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderDeliveries_DeliveryId",
                table: "Orders",
                column: "DeliveryId",
                principalTable: "OrderDeliveries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderGroups_OrderGroupId",
                table: "Orders",
                column: "OrderGroupId",
                principalTable: "OrderGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentOrders_OrderGroups_OrderGroupId",
                table: "PaymentOrders",
                column: "OrderGroupId",
                principalTable: "OrderGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentOrders_Payments_PaymentId",
                table: "PaymentOrders",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_AspNetUsers_BorrowerId",
                table: "Rental",
                column: "BorrowerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartOffers_Offers_OfferId",
                table: "CartOffers");

            migrationBuilder.DropForeignKey(
                name: "FK_OfferDeliveries_Offers_OfferId",
                table: "OfferDeliveries");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_BuyerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderDeliveries_DeliveryId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderGroups_OrderGroupId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentOrders_OrderGroups_OrderGroupId",
                table: "PaymentOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentOrders_Payments_PaymentId",
                table: "PaymentOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Rental_AspNetUsers_BorrowerId",
                table: "Rental");

            migrationBuilder.DropTable(
                name: "OrderDeliveries");

            migrationBuilder.DropTable(
                name: "OrderGroups");

            migrationBuilder.DropTable(
                name: "OrderLines");

            migrationBuilder.DropIndex(
                name: "IX_Rental_BorrowerId",
                table: "Rental");

            migrationBuilder.DropIndex(
                name: "IX_Orders_BuyerId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DeliveryId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderGroupId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentOrders",
                table: "PaymentOrders");

            migrationBuilder.DropColumn(
                name: "BorrowerId",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "Lender_Id",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "Lender_Type",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "PricePerDay_Amount",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "PricePerDay_Currency",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "RentalDays",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DateDelivered",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryPrice_Amount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryPrice_Currency",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "LinesTotal_Amount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "LinesTotal_Currency",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderGroupId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SellerSnapshot_DisplayName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SellerSnapshot_SellerId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SellerSnapshot_TIN",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "SellerSnapshot_Type",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Seller_Type",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Seller_Id",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "Seller_Type",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "Amount_Amount",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ExternalTransactionId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Method",
                table: "Payments");

            migrationBuilder.RenameTable(
                name: "Payments",
                newName: "Payment");

            migrationBuilder.RenameTable(
                name: "PaymentOrders",
                newName: "PaymentOrder");

            migrationBuilder.RenameColumn(
                name: "RentOrderLineId",
                table: "Rental",
                newName: "RentOrderItemId");

            migrationBuilder.RenameColumn(
                name: "Total_Currency",
                table: "Orders",
                newName: "TotalAmount_Currency");

            migrationBuilder.RenameColumn(
                name: "Total_Amount",
                table: "Orders",
                newName: "TotalAmount_Amount");

            migrationBuilder.RenameColumn(
                name: "SellerSnapshot_Address_Street",
                table: "Orders",
                newName: "ShippingAddress_Street");

            migrationBuilder.RenameColumn(
                name: "SellerSnapshot_Address_PostalCode",
                table: "Orders",
                newName: "ShippingAddress_PostalCode");

            migrationBuilder.RenameColumn(
                name: "SellerSnapshot_Address_PostalCity",
                table: "Orders",
                newName: "ShippingAddress_PostalCity");

            migrationBuilder.RenameColumn(
                name: "SellerSnapshot_Address_HouseNumber",
                table: "Orders",
                newName: "ShippingAddress_HouseNumber");

            migrationBuilder.RenameColumn(
                name: "SellerSnapshot_Address_FlatNumber",
                table: "Orders",
                newName: "ShippingAddress_FlatNumber");

            migrationBuilder.RenameColumn(
                name: "SellerSnapshot_Address_Country",
                table: "Orders",
                newName: "ShippingAddress_Country");

            migrationBuilder.RenameColumn(
                name: "SellerSnapshot_Address_City",
                table: "Orders",
                newName: "ShippingAddress_City");

            migrationBuilder.RenameColumn(
                name: "Seller_Id",
                table: "Orders",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "OfferId",
                table: "OfferDeliveries",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "OfferId",
                table: "CartOffers",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_CartOffers_OfferId",
                table: "CartOffers",
                newName: "IX_CartOffers_OrderId");

            migrationBuilder.RenameColumn(
                name: "Amount_Currency",
                table: "Payment",
                newName: "Currency");

            migrationBuilder.RenameColumn(
                name: "OrderGroupId",
                table: "PaymentOrder",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentOrders_PaymentId",
                table: "PaymentOrder",
                newName: "IX_PaymentOrder_PaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentOrders_OrderGroupId",
                table: "PaymentOrder",
                newName: "IX_PaymentOrder_OrderId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RentalEndDate",
                table: "Rental",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Payment",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "Currency",
                table: "Payment",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(3)",
                oldMaxLength: 3);

            migrationBuilder.AddColumn<long>(
                name: "AmountMinor",
                table: "Payment",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "StripeCheckoutSessionId",
                table: "Payment",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StripePaymentIntentId",
                table: "Payment",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "AmountMinor",
                table: "PaymentOrder",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payment",
                table: "Payment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentOrder",
                table: "PaymentOrder",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateDeleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DateEdited = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    OrderItem_TotalPrice = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    OrderItem_TotalPriceCurrency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    OrderItem_UnitPrice = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    OrderItem_UnitPriceCurrency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Offers_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Offers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RentOrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RentalEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RentalStartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentOrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RentOrderItems_OrderItems_Id",
                        column: x => x.Id,
                        principalTable: "OrderItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleOrderItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SaleOrderItems_OrderItems_Id",
                        column: x => x.Id,
                        principalTable: "OrderItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rental_RentOrderItemId",
                table: "Rental",
                column: "RentOrderItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartOffers_Offers_OrderId",
                table: "CartOffers",
                column: "OrderId",
                principalTable: "Offers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OfferDeliveries_Offers_OrderId",
                table: "OfferDeliveries",
                column: "OrderId",
                principalTable: "Offers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentOrder_Orders_OrderId",
                table: "PaymentOrder",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentOrder_Payment_PaymentId",
                table: "PaymentOrder",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_RentOrderItems_RentOrderItemId",
                table: "Rental",
                column: "RentOrderItemId",
                principalTable: "RentOrderItems",
                principalColumn: "Id");
        }
    }
}
