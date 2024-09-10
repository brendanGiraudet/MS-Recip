using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ms_recip.Migrations
{
    /// <inheritdoc />
    public partial class InitialDbCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Image = table.Column<string>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Profils",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profils", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recips",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Image = table.Column<string>(type: "TEXT", nullable: false),
                    Authorname = table.Column<string>(type: "TEXT", nullable: false),
                    PersonNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    CookingDuration = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recips", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ProfilId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Deleted = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Profils_ProfilId",
                        column: x => x.ProfilId,
                        principalTable: "Profils",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientQuantities",
                columns: table => new
                {
                    RecipId = table.Column<Guid>(type: "TEXT", nullable: false),
                    IngredientId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quantity = table.Column<decimal>(type: "TEXT", nullable: false),
                    MeasureUnit = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientQuantities", x => new { x.RecipId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_IngredientQuantities_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientQuantities_Recips_RecipId",
                        column: x => x.RecipId,
                        principalTable: "Recips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipCategories",
                columns: table => new
                {
                    RecipId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProfilModelId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipCategories", x => new { x.RecipId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_RecipCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipCategories_Profils_ProfilModelId",
                        column: x => x.ProfilModelId,
                        principalTable: "Profils",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RecipCategories_Recips_RecipId",
                        column: x => x.RecipId,
                        principalTable: "Recips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipSteps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Label = table.Column<string>(type: "TEXT", nullable: false),
                    RecipId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecipSteps_Recips_RecipId",
                        column: x => x.RecipId,
                        principalTable: "Recips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipCalendars",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RecipId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipCalendars", x => new { x.UserId, x.RecipId, x.Date });
                    table.ForeignKey(
                        name: "FK_RecipCalendars_Recips_RecipId",
                        column: x => x.RecipId,
                        principalTable: "Recips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipCalendars_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientQuantities_IngredientId",
                table: "IngredientQuantities",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipCalendars_RecipId",
                table: "RecipCalendars",
                column: "RecipId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipCategories_CategoryId",
                table: "RecipCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipCategories_ProfilModelId",
                table: "RecipCategories",
                column: "ProfilModelId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipSteps_RecipId",
                table: "RecipSteps",
                column: "RecipId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProfilId",
                table: "Users",
                column: "ProfilId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientQuantities");

            migrationBuilder.DropTable(
                name: "RecipCalendars");

            migrationBuilder.DropTable(
                name: "RecipCategories");

            migrationBuilder.DropTable(
                name: "RecipSteps");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Recips");

            migrationBuilder.DropTable(
                name: "Profils");
        }
    }
}
