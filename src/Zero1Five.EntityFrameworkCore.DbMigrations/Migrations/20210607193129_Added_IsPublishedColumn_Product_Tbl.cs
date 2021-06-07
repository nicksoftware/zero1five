using Microsoft.EntityFrameworkCore.Migrations;

namespace Zero1Five.Migrations
{
    public partial class Added_IsPublishedColumn_Product_Tbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "AppProducts",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "AppProducts");
        }
    }
}
