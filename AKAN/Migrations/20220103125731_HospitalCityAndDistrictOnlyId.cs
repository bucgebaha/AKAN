using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AKAN.Migrations
{
    public partial class HospitalCityAndDistrictOnlyId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hospitals_Cities_CityId",
                table: "Hospitals");

            migrationBuilder.DropForeignKey(
                name: "FK_Hospitals_Districts_DistrictId",
                table: "Hospitals");

            migrationBuilder.DropIndex(
                name: "IX_Hospitals_CityId",
                table: "Hospitals");

            migrationBuilder.DropIndex(
                name: "IX_Hospitals_DistrictId",
                table: "Hospitals");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_CityId",
                table: "Hospitals",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Hospitals_DistrictId",
                table: "Hospitals",
                column: "DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hospitals_Cities_CityId",
                table: "Hospitals",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Hospitals_Districts_DistrictId",
                table: "Hospitals",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
