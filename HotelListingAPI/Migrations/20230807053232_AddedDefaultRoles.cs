using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelListingAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedDefaultRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5519164b-bfa6-4fe2-ad3f-b98bb7935716", "90579ed0-4bcf-4efd-a6d7-21122e44b73f", "Administrator", "ADMINISTRATOR" },
                    { "dc2d0adb-7bb0-47e3-ac56-9743fd54a25d", "aed88837-0d73-4502-8fc3-1f0836324f9f", "user", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5519164b-bfa6-4fe2-ad3f-b98bb7935716");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dc2d0adb-7bb0-47e3-ac56-9743fd54a25d");
        }
    }
}
