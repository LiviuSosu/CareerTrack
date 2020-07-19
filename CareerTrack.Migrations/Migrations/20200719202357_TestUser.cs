using Microsoft.EntityFrameworkCore.Migrations;

namespace CareerTrack.Migrations
{
    public partial class TestUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Afroditul",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Afroditul",
                table: "AspNetUsers");
        }
    }
}
