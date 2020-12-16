using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CareerTrack.Migrations.Migrations
{
    public partial class ConfigureToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Expires",
                table: "UserTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Revoked",
                table: "UserTokens",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Expires",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "Revoked",
                table: "UserTokens");
        }
    }
}
