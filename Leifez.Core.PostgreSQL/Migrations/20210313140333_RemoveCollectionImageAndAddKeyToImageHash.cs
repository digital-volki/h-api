using Microsoft.EntityFrameworkCore.Migrations;

namespace Leifez.Core.PostgreSQL.Migrations
{
    public partial class RemoveCollectionImageAndAddKeyToImageHash : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Collections");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Images_Hash",
                table: "Images",
                column: "Hash");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Images_Hash",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Collections",
                type: "text",
                nullable: true);
        }
    }
}
