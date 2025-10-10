using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class Removeddificulties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Walks_Difficulties_DifficultyId",
                table: "Walks");

            migrationBuilder.DropForeignKey(
                name: "FK_Walks_Regions_Regionid",
                table: "Walks");

            migrationBuilder.DropTable(
                name: "Difficulties");

            migrationBuilder.DropIndex(
                name: "IX_Walks_DifficultyId",
                table: "Walks");

            migrationBuilder.DropColumn(
                name: "DifficultyId",
                table: "Walks");

            migrationBuilder.RenameColumn(
                name: "Regionid",
                table: "Walks",
                newName: "RegionId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Walks",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_Walks_Regionid",
                table: "Walks",
                newName: "IX_Walks_RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Walks_Regions_RegionId",
                table: "Walks",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Walks_Regions_RegionId",
                table: "Walks");

            migrationBuilder.RenameColumn(
                name: "RegionId",
                table: "Walks",
                newName: "Regionid");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Walks",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_Walks_RegionId",
                table: "Walks",
                newName: "IX_Walks_Regionid");

            migrationBuilder.AddColumn<Guid>(
                name: "DifficultyId",
                table: "Walks",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Difficulties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Difficulties", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Walks_DifficultyId",
                table: "Walks",
                column: "DifficultyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Walks_Difficulties_DifficultyId",
                table: "Walks",
                column: "DifficultyId",
                principalTable: "Difficulties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Walks_Regions_Regionid",
                table: "Walks",
                column: "Regionid",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
