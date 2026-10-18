using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeddingManager.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeWeddingDateToDateOnly : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "WeddingDate",
                table: "Weddings",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "WeddingDate",
                table: "Weddings",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
