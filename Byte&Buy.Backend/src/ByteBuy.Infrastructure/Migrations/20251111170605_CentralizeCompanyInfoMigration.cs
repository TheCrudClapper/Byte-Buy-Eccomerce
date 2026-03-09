using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteBuy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CentralizeCompanyInfoMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_CompanyInfo_CompanyInfoId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CompanyInfoId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CompanyInfoId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "NIP",
                table: "CompanyInfo",
                newName: "TIN");

            migrationBuilder.AddColumn<string>(
                name: "Slogan",
                table: "CompanyInfo",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Slogan",
                table: "CompanyInfo");

            migrationBuilder.RenameColumn(
                name: "TIN",
                table: "CompanyInfo",
                newName: "NIP");

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyInfoId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CompanyInfoId",
                table: "AspNetUsers",
                column: "CompanyInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_CompanyInfo_CompanyInfoId",
                table: "AspNetUsers",
                column: "CompanyInfoId",
                principalTable: "CompanyInfo",
                principalColumn: "SellerId");
        }
    }
}
