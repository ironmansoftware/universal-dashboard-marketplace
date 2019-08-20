using Microsoft.EntityFrameworkCore.Migrations;

namespace Marketplace.Migrations
{
    public partial class AddTypeModuleInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "ModuleInfo",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "ModuleInfo");
        }
    }
}
