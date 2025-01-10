using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eVillaBooking.Infrastructher.Migrations
{
    /// <inheritdoc />
    public partial class SeedVillaNumberTABLE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "VillaNumber",
                columns: new[] { "Villa_Number", "SpecialDetails", "Villa_Id" },
                values: new object[,]
                {
                    { 101, null, 1 },
                    { 102, null, 2 },
                    { 103, null, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "VillaNumber",
                keyColumn: "Villa_Number",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "VillaNumber",
                keyColumn: "Villa_Number",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "VillaNumber",
                keyColumn: "Villa_Number",
                keyValue: 103);
        }
    }
}
