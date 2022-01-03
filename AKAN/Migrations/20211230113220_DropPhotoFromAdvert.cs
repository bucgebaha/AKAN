using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AKAN.Migrations
{
    public partial class DropPhotoFromAdvert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Adverts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Adverts",
                type: "text",
                nullable: true);
        }
    }
}
