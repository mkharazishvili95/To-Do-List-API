using Microsoft.EntityFrameworkCore.Migrations;

namespace To_Do_List_API.Migrations
{
    public partial class Final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ToDoLists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ToDoLists_UserId",
                table: "ToDoLists",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoLists_Users_UserId",
                table: "ToDoLists",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoLists_Users_UserId",
                table: "ToDoLists");

            migrationBuilder.DropIndex(
                name: "IX_ToDoLists_UserId",
                table: "ToDoLists");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ToDoLists");
        }
    }
}
