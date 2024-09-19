using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ms_recip.Migrations
{
    /// <inheritdoc />
    public partial class addAuthorIdIntoRecips : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorId",
                table: "Recips",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Recips");
        }
    }
}
