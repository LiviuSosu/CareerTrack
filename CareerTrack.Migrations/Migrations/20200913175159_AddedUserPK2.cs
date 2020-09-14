using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CareerTrack.Migrations
{
    public partial class AddedUserPK2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "UserId",
            //    table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<Guid>(
            //    name: "UserId",
            //    table: "AspNetUsers",
            //    type: "uniqueidentifier",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
