using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteBuy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeAsPartOfCompanyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyInfo");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TIN = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CompanyAddress_Street = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CompanyAddress_HouseNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CompanyAddress_PostalCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CompanyAddress_PostalCity = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CompanyAddress_City = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CompanyAddress_Country = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CompanyAddress_FlatNumber = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    Slogan = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateEdited = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Company_CompanyId",
                table: "AspNetUsers",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Company_CompanyId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CompanyId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "CompanyInfo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateEdited = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    Slogan = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    TIN = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CompanyAddress_City = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CompanyAddress_Country = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CompanyAddress_FlatNumber = table.Column<string>(type: "text", nullable: true),
                    CompanyAddress_HouseNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CompanyAddress_PostalCity = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CompanyAddress_PostalCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CompanyAddress_Street = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyInfo", x => x.Id);
                });
        }
    }
}
