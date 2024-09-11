using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ms_recip.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRecipCalendarTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipCalendars",
                table: "RecipCalendars");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "RecipCalendars",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipCalendars",
                table: "RecipCalendars",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_RecipCalendars_UserId",
                table: "RecipCalendars",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RecipCalendars",
                table: "RecipCalendars");

            migrationBuilder.DropIndex(
                name: "IX_RecipCalendars_UserId",
                table: "RecipCalendars");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RecipCalendars");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RecipCalendars",
                table: "RecipCalendars",
                columns: new[] { "UserId", "RecipId", "Date" });
        }
    }
}
