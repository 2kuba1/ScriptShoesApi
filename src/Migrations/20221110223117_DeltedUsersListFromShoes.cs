using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScriptShoesCQRS.Migrations
{
    public partial class DeltedUsersListFromShoes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Shoes_ShoesId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ShoesId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ShoesId",
                table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShoesId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ShoesId",
                table: "Users",
                column: "ShoesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Shoes_ShoesId",
                table: "Users",
                column: "ShoesId",
                principalTable: "Shoes",
                principalColumn: "Id");
        }
    }
}
