using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendServer.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_contactdata",
                table: "contactdata");

            migrationBuilder.RenameTable(
                name: "contactdata",
                newName: "ContactData");

            migrationBuilder.RenameIndex(
                name: "IX_contactdata_Id",
                table: "ContactData",
                newName: "IX_ContactData_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContactData",
                table: "ContactData",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ContactData",
                table: "ContactData");

            migrationBuilder.RenameTable(
                name: "ContactData",
                newName: "contactdata");

            migrationBuilder.RenameIndex(
                name: "IX_ContactData_Id",
                table: "contactdata",
                newName: "IX_contactdata_Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_contactdata",
                table: "contactdata",
                column: "Id");
        }
    }
}
