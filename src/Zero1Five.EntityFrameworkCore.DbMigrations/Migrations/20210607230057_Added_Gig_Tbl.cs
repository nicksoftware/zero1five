using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Zero1Five.Migrations
{
    public partial class Added_Gig_Tbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "GigId",
                table: "AppProducts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "AppGigs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CoverImage = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uuid", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppGigs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppProducts_GigId",
                table: "AppProducts",
                column: "GigId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppProducts_AppGigs_GigId",
                table: "AppProducts",
                column: "GigId",
                principalTable: "AppGigs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppProducts_AppGigs_GigId",
                table: "AppProducts");

            migrationBuilder.DropTable(
                name: "AppGigs");

            migrationBuilder.DropIndex(
                name: "IX_AppProducts_GigId",
                table: "AppProducts");

            migrationBuilder.DropColumn(
                name: "GigId",
                table: "AppProducts");
        }
    }
}
