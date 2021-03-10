using Microsoft.EntityFrameworkCore.Migrations;

namespace Leifez.Core.PostgreSQL.Migrations
{
    public partial class DeleteQuantityByTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Tags");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Tags",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
