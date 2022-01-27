using Microsoft.EntityFrameworkCore.Migrations;

namespace TodoApi.Data.Migrations
{
    public partial class AddTodoItemForeignKeyToTodoCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TodoCategoryId",
                table: "TodoItem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TodoItem_TodoCategoryId",
                table: "TodoItem",
                column: "TodoCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoItem_TodoCategory_TodoCategoryId",
                table: "TodoItem",
                column: "TodoCategoryId",
                principalTable: "TodoCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoItem_TodoCategory_TodoCategoryId",
                table: "TodoItem");

            migrationBuilder.DropIndex(
                name: "IX_TodoItem_TodoCategoryId",
                table: "TodoItem");

            migrationBuilder.DropColumn(
                name: "TodoCategoryId",
                table: "TodoItem");
        }
    }
}
