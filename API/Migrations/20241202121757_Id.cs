using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class Id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "42672b46-fe01-424d-995d-cba9f52187cf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f11a76a3-93d4-41a7-b7b2-f7090f25b641");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1031aed3-170e-4237-84a4-654bcd542f20", null, "Admin", "ADMIN" },
                    { "3399d39c-dca6-4153-9135-8deae757edc6", null, "Member", "MEMBER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1031aed3-170e-4237-84a4-654bcd542f20");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3399d39c-dca6-4153-9135-8deae757edc6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "42672b46-fe01-424d-995d-cba9f52187cf", null, "Member", "MEMBER" },
                    { "f11a76a3-93d4-41a7-b7b2-f7090f25b641", null, "Admin", "ADMIN" }
                });
        }
    }
}
