using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class PostsDateOfCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfCreation",
                table: "Posts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfCreation",
                table: "Posts");
        }
    }
}
