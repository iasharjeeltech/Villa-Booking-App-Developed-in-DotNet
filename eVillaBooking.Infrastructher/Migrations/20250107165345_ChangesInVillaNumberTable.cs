using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eVillaBooking.Infrastructher.Migrations
{
    /// <inheritdoc />
    public partial class ChangesInVillaNumberTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VillaNumber",
                table: "VillaNumber");

            migrationBuilder.DeleteData(
                table: "VillaNumber",
                keyColumn: "VillaId",
                keyColumnType: "int",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "VillaNumber",
                keyColumn: "VillaId",
                keyColumnType: "int",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "VillaNumber",
                keyColumn: "VillaId",
                keyColumnType: "int",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "VillaId",
                table: "VillaNumber");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VillaNumber",
                table: "VillaNumber",
                column: "Villa_Number");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_VillaNumber",
                table: "VillaNumber");

            migrationBuilder.AddColumn<int>(
                name: "VillaId",
                table: "VillaNumber",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VillaNumber",
                table: "VillaNumber",
                column: "VillaId");

            migrationBuilder.InsertData(
                table: "VillaNumber",
                columns: new[] { "VillaId", "SpecialDetails", "Villa_Number" },
                values: new object[,]
                {
                    { 1, null, 101 },
                    { 2, null, 102 },
                    { 3, null, 103 }
                });
        }
    }
}
