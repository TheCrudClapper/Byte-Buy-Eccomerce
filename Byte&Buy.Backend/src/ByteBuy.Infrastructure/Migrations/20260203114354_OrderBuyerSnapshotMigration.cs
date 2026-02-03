using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteBuy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrderBuyerSnapshotMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "BuyerSnapshot_Address_City",
                table: "Orders",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BuyerSnapshot_Address_Country",
                table: "Orders",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BuyerSnapshot_Address_FlatNumber",
                table: "Orders",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuyerSnapshot_Address_HouseNumber",
                table: "Orders",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BuyerSnapshot_Address_PostalCity",
                table: "Orders",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BuyerSnapshot_Address_PostalCode",
                table: "Orders",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BuyerSnapshot_Address_Street",
                table: "Orders",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BuyerSnapshot_Email",
                table: "Orders",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BuyerSnapshot_FullName",
                table: "Orders",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BuyerSnapshot_PhoneNumber",
                table: "Orders",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BuyerSnapshot_Address_City",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BuyerSnapshot_Address_Country",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BuyerSnapshot_Address_FlatNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BuyerSnapshot_Address_HouseNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BuyerSnapshot_Address_PostalCity",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BuyerSnapshot_Address_PostalCode",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BuyerSnapshot_Address_Street",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BuyerSnapshot_Email",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BuyerSnapshot_FullName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BuyerSnapshot_PhoneNumber",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "HomeAddress_Street",
                table: "AspNetUsers",
                newName: "Address_Street");

            migrationBuilder.RenameColumn(
                name: "HomeAddress_PostalCode",
                table: "AspNetUsers",
                newName: "Address_PostalCode");

            migrationBuilder.RenameColumn(
                name: "HomeAddress_PostalCity",
                table: "AspNetUsers",
                newName: "Address_PostalCity");

            migrationBuilder.RenameColumn(
                name: "HomeAddress_HouseNumber",
                table: "AspNetUsers",
                newName: "Address_HouseNumber");

            migrationBuilder.RenameColumn(
                name: "HomeAddress_FlatNumber",
                table: "AspNetUsers",
                newName: "Address_FlatNumber");

            migrationBuilder.RenameColumn(
                name: "HomeAddress_Country",
                table: "AspNetUsers",
                newName: "Address_Country");

            migrationBuilder.RenameColumn(
                name: "HomeAddress_City",
                table: "AspNetUsers",
                newName: "Address_City");
        }
    }
}
