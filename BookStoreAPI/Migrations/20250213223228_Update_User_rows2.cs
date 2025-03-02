using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class Update_User_rows2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "15188dde-9045-4596-a331-f3db4e518b6e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dcdd7e05-8beb-40b8-8b11-bb2133c2d726");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b7aa9608-c3e6-4c95-9881-1a5e69afcd31", null, "Customer", "CUSTOMER" },
                    { "ced77c7b-ff18-48a8-a2d1-9551b6174816", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b7aa9608-c3e6-4c95-9881-1a5e69afcd31");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ced77c7b-ff18-48a8-a2d1-9551b6174816");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "15188dde-9045-4596-a331-f3db4e518b6e", null, "Customer", "CUSTOMER" },
                    { "dcdd7e05-8beb-40b8-8b11-bb2133c2d726", null, "Admin", "ADMIN" }
                });
        }
    }
}
