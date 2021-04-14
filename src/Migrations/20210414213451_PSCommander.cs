using Microsoft.EntityFrameworkCore.Migrations;

namespace Marketplace.Migrations
{
    public partial class PSCommander : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TotalPsCommanderModules",
                table: "Statistics",
                nullable: false,
                defaultValue: 0L);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPsCommanderModules",
                table: "Statistics");
        }
    }
}
