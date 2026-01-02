using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteBuy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OfferAddressSnapshotMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress_PostalCity",
                table: "Orders",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OwnerAddressSnapshot_City",
                table: "Offers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerAddressSnapshot_Country",
                table: "Offers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerAddressSnapshot_FlatNumber",
                table: "Offers",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerAddressSnapshot_HouseNumber",
                table: "Offers",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerAddressSnapshot_PostalCity",
                table: "Offers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerAddressSnapshot_PostalCode",
                table: "Offers",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerAddressSnapshot_Street",
                table: "Offers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompanyAddress_PostalCity",
                table: "CompanyInfo",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "HomeAddress_PostalCity",
                table: "AspNetUsers",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingAddress_PostalCity",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OwnerAddressSnapshot_City",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "OwnerAddressSnapshot_Country",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "OwnerAddressSnapshot_FlatNumber",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "OwnerAddressSnapshot_HouseNumber",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "OwnerAddressSnapshot_PostalCity",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "OwnerAddressSnapshot_PostalCode",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "OwnerAddressSnapshot_Street",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "CompanyAddress_PostalCity",
                table: "CompanyInfo");

            migrationBuilder.DropColumn(
                name: "HomeAddress_PostalCity",
                table: "AspNetUsers");
        }
    }
}
