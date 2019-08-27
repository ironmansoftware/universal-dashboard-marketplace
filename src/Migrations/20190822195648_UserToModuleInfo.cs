using Microsoft.EntityFrameworkCore.Migrations;

namespace Marketplace.Migrations
{
    public partial class UserToModuleInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubmittedById",
                table: "ModuleInfo",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ModuleInfo_SubmittedById",
                table: "ModuleInfo",
                column: "SubmittedById");

            migrationBuilder.AddForeignKey(
                name: "FK_ModuleInfo_AspNetUsers_SubmittedById",
                table: "ModuleInfo",
                column: "SubmittedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ModuleInfo_AspNetUsers_SubmittedById",
                table: "ModuleInfo");

            migrationBuilder.DropIndex(
                name: "IX_ModuleInfo_SubmittedById",
                table: "ModuleInfo");

            migrationBuilder.DropColumn(
                name: "SubmittedById",
                table: "ModuleInfo");
        }
    }
}
