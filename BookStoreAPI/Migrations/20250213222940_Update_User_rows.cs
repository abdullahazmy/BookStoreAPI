using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStoreAPI.Migrations
{
    /// <inheritdoc />
    public partial class Update_User_rows : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2a56dfd-5141-4415-88a9-1c8814b88141");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc676ec5-79f1-46ff-a6cb-c5ad278c4263");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "15188dde-9045-4596-a331-f3db4e518b6e", null, "Customer", "CUSTOMER" },
                    { "dcdd7e05-8beb-40b8-8b11-bb2133c2d726", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { "b2a56dfd-5141-4415-88a9-1c8814b88141", null, "Admin", "ADMIN" },
                    { "dc676ec5-79f1-46ff-a6cb-c5ad278c4263", null, "Customer", "CUSTOMER" }
                });
        }
    }
}
