using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zero1Five.Migrations
{
    public partial class Added_Category_To_Gig_Tbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "AppGigs",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppGigs_CategoryId",
                table: "AppGigs",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppGigs_AppCategories_CategoryId",
                table: "AppGigs",
                column: "CategoryId",
                principalTable: "AppCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppGigs_AppCategories_CategoryId",
                table: "AppGigs");

            migrationBuilder.DropIndex(
                name: "IX_AppGigs_CategoryId",
                table: "AppGigs");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "AppGigs");
        }
    }
}
