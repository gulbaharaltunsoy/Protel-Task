using Microsoft.EntityFrameworkCore.Migrations;

namespace Protel.Migrations
{
    public partial class addedtuskishname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TurkishName",
                table: "Currencies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TurkishName",
                table: "Currencies");
        }
    }
}
