using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistance.Migrations
{
    /// <inheritdoc />
    public partial class mig_add_2233191 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Districts_CityWeathers_CityWeatherCityId",
                table: "Districts");

            migrationBuilder.DropIndex(
                name: "IX_Districts_CityWeatherCityId",
                table: "Districts");

            migrationBuilder.DropColumn(
                name: "CityWeatherCityId",
                table: "Districts");

            migrationBuilder.CreateIndex(
                name: "IX_Districts_CityId",
                table: "Districts",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_CityWeathers_CityId",
                table: "Districts",
                column: "CityId",
                principalTable: "CityWeathers",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Districts_CityWeathers_CityId",
                table: "Districts");

            migrationBuilder.DropIndex(
                name: "IX_Districts_CityId",
                table: "Districts");

            migrationBuilder.AddColumn<int>(
                name: "CityWeatherCityId",
                table: "Districts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Districts_CityWeatherCityId",
                table: "Districts",
                column: "CityWeatherCityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Districts_CityWeathers_CityWeatherCityId",
                table: "Districts",
                column: "CityWeatherCityId",
                principalTable: "CityWeathers",
                principalColumn: "CityId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
