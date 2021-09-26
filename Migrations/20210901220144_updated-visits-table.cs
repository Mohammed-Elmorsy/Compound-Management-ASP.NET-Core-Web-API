using Microsoft.EntityFrameworkCore.Migrations;

namespace VisitsTaskWebAPI_MohammedElmorsy.Migrations
{
    public partial class updatedvisitstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserFullName",
                table: "Visits",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserFullName",
                table: "Visits");
        }
    }
}
