using Microsoft.EntityFrameworkCore.Migrations;

namespace Leifez.Core.PostgreSQL.Migrations
{
    public partial class ChangeTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Collections_DbCollectionId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_DbCollectionId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "DbCollectionId",
                table: "Tags");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DbCollectionId",
                table: "Tags",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tags_DbCollectionId",
                table: "Tags",
                column: "DbCollectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Collections_DbCollectionId",
                table: "Tags",
                column: "DbCollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
