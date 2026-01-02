using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteBuy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class HomeAndBillingAddressMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.RenameColumn(
                name: "Address_Street",
                table: "CompanyInfo",
                newName: "CompanyAddress_Street");

            migrationBuilder.RenameColumn(
                name: "Address_PostalCode",
                table: "CompanyInfo",
                newName: "CompanyAddress_PostalCode");

            migrationBuilder.RenameColumn(
                name: "Address_HouseNumber",
                table: "CompanyInfo",
                newName: "CompanyAddress_HouseNumber");

            migrationBuilder.RenameColumn(
                name: "Address_FlatNumber",
                table: "CompanyInfo",
                newName: "CompanyAddress_FlatNumber");

            migrationBuilder.RenameColumn(
                name: "Address_Country",
                table: "CompanyInfo",
                newName: "CompanyAddress_Country");

            migrationBuilder.RenameColumn(
                name: "Address_City",
                table: "CompanyInfo",
                newName: "CompanyAddress_City");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CompanyAddress_Street",
                table: "CompanyInfo",
                newName: "ShippingAddress_Street");

            migrationBuilder.RenameColumn(
                name: "CompanyAddress_PostalCode",
                table: "CompanyInfo",
                newName: "ShippingAddress_PostalCode");

            migrationBuilder.RenameColumn(
                name: "CompanyAddress_HouseNumber",
                table: "CompanyInfo",
                newName: "ShippingAddress_HouseNumber");

            migrationBuilder.RenameColumn(
                name: "CompanyAddress_FlatNumber",
                table: "CompanyInfo",
                newName: "ShippingAddress_FlatNumber");

            migrationBuilder.RenameColumn(
                name: "CompanyAddress_Country",
                table: "CompanyInfo",
                newName: "ShippingAddress_Country");

            migrationBuilder.RenameColumn(
                name: "CompanyAddress_City",
                table: "CompanyInfo",
                newName: "ShippingAddress_City");
        }
    }
}
