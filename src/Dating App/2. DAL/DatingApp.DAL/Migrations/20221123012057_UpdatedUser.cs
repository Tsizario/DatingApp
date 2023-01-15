using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DatingApp.DAL.Migrations
{
    public partial class UpdatedUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Country",
                table: "Users",
                newName: "Region");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Region",
                table: "Users",
                newName: "Country");
        }
    }
}
