using Microsoft.EntityFrameworkCore.Migrations;

namespace dal.Migrations
{
    public partial class FairbaseToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FairbaseToken",
                table: "AspNetUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FairbaseToken",
                table: "AspNetUsers");
        }
    }
}
