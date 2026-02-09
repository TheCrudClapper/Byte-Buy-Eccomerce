using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteBuy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RentalAggregateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lender_Id",
                table: "Rental",
                newName: "Lender_SellerId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDate",
                table: "Rental",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ItemName",
                table: "Rental",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Lender_Address_City",
                table: "Rental",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Lender_Address_Country",
                table: "Rental",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Lender_Address_FlatNumber",
                table: "Rental",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lender_Address_HouseNumber",
                table: "Rental",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Lender_Address_PostalCity",
                table: "Rental",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Lender_Address_PostalCode",
                table: "Rental",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Lender_Address_Street",
                table: "Rental",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Lender_DisplayName",
                table: "Rental",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Lender_TIN",
                table: "Rental",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Rental",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Thumbnail_AltText",
                table: "Rental",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Thumbnail_ImagePath",
                table: "Rental",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "ItemName",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "Lender_Address_City",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "Lender_Address_Country",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "Lender_Address_FlatNumber",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "Lender_Address_HouseNumber",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "Lender_Address_PostalCity",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "Lender_Address_PostalCode",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "Lender_Address_Street",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "Lender_DisplayName",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "Lender_TIN",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "Thumbnail_AltText",
                table: "Rental");

            migrationBuilder.DropColumn(
                name: "Thumbnail_ImagePath",
                table: "Rental");

            migrationBuilder.RenameColumn(
                name: "Lender_SellerId",
                table: "Rental",
                newName: "Lender_Id");
        }
    }
}
