using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eVillaBooking.Infrastructher.Migrations
{
    /// <inheritdoc />
    public partial class AddFKInVillaNumberTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Villa_Id",
                table: "VillaNumber",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VillaNumber_Villa_Id",
                table: "VillaNumber",
                column: "Villa_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_VillaNumber_MyProperty_Villa_Id",
                table: "VillaNumber",
                column: "Villa_Id",
                principalTable: "MyProperty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VillaNumber_MyProperty_Villa_Id",
                table: "VillaNumber");

            migrationBuilder.DropIndex(
                name: "IX_VillaNumber_Villa_Id",
                table: "VillaNumber");

            migrationBuilder.DropColumn(
                name: "Villa_Id",
                table: "VillaNumber");
        }
    }
}
