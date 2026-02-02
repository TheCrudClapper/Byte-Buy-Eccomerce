using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ByteBuy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PaymentDetailsSoftDeleteMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateDeleted",
                table: "PaymentDetails",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "PaymentDetails",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateDeleted",
                table: "PaymentDetails");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "PaymentDetails");
        }
    }
}
