using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AKAN.Migrations
{
    public partial class AddPhotoToAdvert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Adverts",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Adverts");
        }
    }
}
