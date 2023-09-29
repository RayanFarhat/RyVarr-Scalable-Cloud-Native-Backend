using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendServer.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProEndingDate",
                table: "AcountData",
                defaultValue: DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy hh:mm:ss"));
            migrationBuilder.AlterColumn<string>(
                name: "ProEndingDate",
                table: "AcountData",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
               oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "ProEndingDate",
                table: "AcountData",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
