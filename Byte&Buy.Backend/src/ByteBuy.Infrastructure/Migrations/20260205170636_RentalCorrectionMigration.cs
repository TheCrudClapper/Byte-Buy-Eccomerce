using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteBuy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RentalCorrectionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rental_AspNetUsers_BorrowerId",
                table: "Rental");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rental",
                table: "Rental");

            migrationBuilder.RenameTable(
                name: "Rental",
                newName: "Rentals");

            migrationBuilder.RenameIndex(
                name: "IX_Rental_BorrowerId",
                table: "Rentals",
                newName: "IX_Rentals_BorrowerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rentals",
                table: "Rentals",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_AspNetUsers_BorrowerId",
                table: "Rentals",
                column: "BorrowerId",
                principalTable: "AspNetUsers",
                principalColumn: "SellerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_AspNetUsers_BorrowerId",
                table: "Rentals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rentals",
                table: "Rentals");

            migrationBuilder.RenameTable(
                name: "Rentals",
                newName: "Rental");

            migrationBuilder.RenameIndex(
                name: "IX_Rentals_BorrowerId",
                table: "Rental",
                newName: "IX_Rental_BorrowerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rental",
                table: "Rental",
                column: "SellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rental_AspNetUsers_BorrowerId",
                table: "Rental",
                column: "BorrowerId",
                principalTable: "AspNetUsers",
                principalColumn: "SellerId");
        }
    }
}
