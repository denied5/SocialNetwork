using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class PhotosApproved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Photos",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Photos");
        }
    }
}
