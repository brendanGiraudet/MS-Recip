using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ms_recip.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RecipCategories_Profils_ProfilModelId",
                table: "RecipCategories");

            migrationBuilder.DropIndex(
                name: "IX_RecipCategories_ProfilModelId",
                table: "RecipCategories");

            migrationBuilder.DropColumn(
                name: "ProfilModelId",
                table: "RecipCategories");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CookingDuration",
                table: "Recips",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.CreateTable(
                name: "ProfilCategories",
                columns: table => new
                {
                    ProfilId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfilCategories", x => new { x.ProfilId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_ProfilCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfilCategories_Profils_ProfilId",
                        column: x => x.ProfilId,
                        principalTable: "Profils",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfilCategories_CategoryId",
                table: "ProfilCategories",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfilCategories");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CookingDuration",
                table: "Recips",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProfilModelId",
                table: "RecipCategories",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipCategories_ProfilModelId",
                table: "RecipCategories",
                column: "ProfilModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecipCategories_Profils_ProfilModelId",
                table: "RecipCategories",
                column: "ProfilModelId",
                principalTable: "Profils",
                principalColumn: "Id");
        }
    }
}
